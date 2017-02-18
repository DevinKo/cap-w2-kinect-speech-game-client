using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataStorage : MonoBehaviour {

    public class Session
    {
        public Session() { }

        private float identifyObjectTime;
        private float indicateSizeTime;

        public int currentTask;

        // setters, getters
        public void setIdentifyObjectTime(float time)
        {
            identifyObjectTime = time;
        }
        public float getIdentifyObjectTime()
        {
            return identifyObjectTime;
        }

        public void setIndicateSizeTime(float time)
        {
            indicateSizeTime = time;
        }
        public float getIndicateSizeTime()
        {
            return indicateSizeTime;
        }
    }
}
