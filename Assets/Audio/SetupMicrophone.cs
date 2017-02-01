using UnityEngine;
using System.Collections;

public class SetupMicrophone : MonoBehaviour {

	public float vol;

	// Use this for initialization USING REGULAR MIC
	IEnumerator Start () {
        var audio = GetComponent<AudioSource>();
        if (Microphone.devices.Length == 0)
            yield break;
        audio.clip = Microphone.Start(null, true, 5, AudioSettings.outputSampleRate);
        while (Microphone.GetPosition(null) <= 0) {
            yield return 0;
        }
        audio.Play();

		//Debug.Log ("Volume: " + audio.volume);


		float[] samples = new float[1000];
		audio.clip.GetData(samples, 0);
		int i = 0;
		double volTotal = 0;
		while (i < samples.Length) 
		{
			print(samples[i]);
			volTotal += samples[i];
			++i;
		}

		double averageVol = volTotal / (double)i;
		print ("average: " + averageVol);
		print (audio.time.ToString());
    }
}


//KINECT START FUNCTION
//public Stream Start ()