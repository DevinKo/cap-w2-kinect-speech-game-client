using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Windows.Kinect;

namespace Assets.Toolbox
{
    public class VolumeCollector : MonoBehaviour
    {
        private KinectSensor _Sensor;
        private AudioBeamFrameReader _Reader;

        /// <summary>
        /// Will be allocated a buffer to hold a single sub frame of audio data read from audio stream.
        /// </summary>
        private byte[] audioBuffer = null;

        /// <summary>
        /// Number of bytes in each Kinect audio stream sample (32-bit IEEE float).
        /// </summary>
        private const int BytesPerSample = sizeof(float);

        /// <summary>
        /// Minimum energy of audio to display (a negative number in dB value, where 0 dB is full scale)
        /// </summary>
        private const int MinEnergy = -90;

        /// <summary>
        /// The number of energy values kept track of by this audio manager
        /// </summary>
        private const int EnergyStreamLength = 100;

        /// <summary>
        /// Number of audio samples represented by each column of pixels in wave bitmap.
        /// </summary>
        private const int SamplesPerColumn = 1000;

        /// <summary>
        /// Sum of squares of audio samples being accumulated to compute the next energy value.
        /// </summary>
        private float accumulatedSquareSum;

        /// <summary>
        /// Number of audio samples accumulated so far to compute the next energy value.
        /// </summary>
        private int accumulatedSampleCount;

        /// <summary>
        /// Index of next element available in audio energy buffer.
        /// </summary>
        private int energyIndex;

        /// <summary>
        /// Buffer used to store audio stream energy data as we read audio.
        /// We store 25% more energy values than we strictly need for visualization to allow for a smoother
        /// stream animation effect, since rendering happens on a different schedule with respect to audio
        /// capture.
        /// </summary>
        private readonly LinkedList<float> energy = new LinkedList<float>();

        public float CurrentEnergy
        {
            get
            {
                return energy.First.Value;
            }
        }

        public IList<float> EnergyStream
        {
            get
            {
                return energy.ToList();
            }
        }

        public void Start()
        {
            _Sensor = KinectSensor.GetDefault();

            if (_Sensor != null)
            {
                //_Reader = _Sensor.AudioSource.OpenReader();

                // Get its audio source
                var audioSource = _Sensor.AudioSource;

                // Allocate 1024 bytes to hold a single audio sub frame. Duration sub frame 
                // is 16 msec, the sample rate is 16khz, which means 256 samples per sub frame. 
                // With 4 bytes per sample, that gives us 1024 bytes.
                audioBuffer = new byte[audioSource.SubFrameLengthInBytes];

                // We only support one audio beam and it points forward.
                //audioSource.AudioBeams[0].AudioBeamMode = AudioBeamMode.Manual;
                //audioSource.AudioBeams[0].BeamAngle = 0;

                if (!_Sensor.IsOpen)
                {
                    _Sensor.Open();
                }
            }
            //StartCoroutine("CollectAudio");
        }

        public void Update()
        {
            _Reader = _Sensor.AudioSource.OpenReader();
            
            var audioBeamFrames = _Reader.AcquireLatestBeamFrames();
            
            if (audioBeamFrames == null)
            {
                return;
            }

            if (audioBeamFrames[0] == null)
            {
                return;
            }

            // Only one audio beam is supported. Get the sub frame list for this beam
            var subFrameList = audioBeamFrames[0].SubFrames;
            
            // Loop over all sub frames, extract audio buffer and beam information
            foreach (AudioBeamSubFrame subFrame in subFrameList)
            {
                // Process audio buffer
                subFrame.CopyFrameDataToArray(this.audioBuffer);

                subFrame.Dispose();

                for (int i = 0; i < this.audioBuffer.Length; i += BytesPerSample)
                {
                    // Extract the 32-bit IEEE float sample from the byte array
                    float audioSample = BitConverter.ToSingle(this.audioBuffer, i);

                    this.accumulatedSquareSum += audioSample * audioSample;
                    ++this.accumulatedSampleCount;

                    if (this.accumulatedSampleCount < SamplesPerColumn)
                    {
                        continue;
                    }

                    float meanSquare = this.accumulatedSquareSum / SamplesPerColumn;

                    if (meanSquare > 1.0f)
                    {
                        // A loud audio source right next to the sensor may result in mean square values
                        // greater than 1.0. Cap it at 1.0f for display purposes.
                        meanSquare = 1.0f;
                    }

                    // Calculate energy in dB, in the range [MinEnergy, 0], where MinEnergy < 0
                    float energy = MinEnergy;

                    if (meanSquare > 0)
                    {
                        energy = (float)(10.0 * Math.Log10(meanSquare));
                    }
                    
                    // Normalize values to the range [0, 1] for display
                    this.energy.AddFirst((MinEnergy - energy) / MinEnergy);
                    //Debug.Log((MinEnergy - energy) / MinEnergy);
                    if (this.energy.Count >= EnergyStreamLength)
                    {
                        this.energy.RemoveLast();
                    }

                    this.accumulatedSquareSum = 0;
                    this.accumulatedSampleCount = 0;
                }
            }
            audioBeamFrames[0].Dispose();
        }

        public IEnumerator CollectAudio()
        {
            while (true) {
             
                yield return new WaitForSeconds(0.1f);
            }
            
        }

        void OnApplicationQuit()
        {
            if (_Reader != null)
            {
                _Reader.Dispose();
                _Reader = null;
            }

            if (_Sensor != null)
            {
                if (_Sensor.IsOpen)
                {
                    _Sensor.Close();
                }

                _Sensor = null;
            }
        }
    }
}
