using UnityEngine;
using Assets.GameScripts;
using System;

namespace Assets.Toolbox
{

    public class Toolbox : MonoBehaviour
    {
        protected Toolbox() { } // guarantee this will be always a singleton only - can't use the constructor!

        // Should use some sort of dependency injection here.
        protected AbstractAppDataManager _appDataManager;
        protected AbstractDataServerClient _dataServerClient;
        protected BodySourceManager _bodySourceManager;
        protected BodySnapshotCollector _bodySnapshotCollector;
        protected VolumeSourceManager _volumeSourceManager;
        protected VolumeCollector _volumeCollector;
        protected DistanceCollector _distanceCollector;

        public AbstractAppDataManager AppDataManager
        {
            get
            {
                return _appDataManager;
            }
        }

        public AbstractDataServerClient DataServerProxy
        {
            get
            {
                return _dataServerClient;
            }
        }

        public BodySourceManager BodySourceManager
        {
            get
            {
                return _bodySourceManager;
            }
        }

        public BodySnapshotCollector BodySnapshotCollector
        {
            get
            {
                return _bodySnapshotCollector;
            }
        }

        public VolumeSourceManager VolumeSourceManager
        {
            get
            {
                return _volumeSourceManager;
            }
        }

        public VolumeCollector VolumeCollector
        {
            get
            {
                return _volumeCollector;
            }
        }

        public DistanceCollector DistanceCollector
        {
            get
            {
                return _distanceCollector;
            }
        }

        void Awake()
        {
            // initialization code here
            // Should use some sort of dependency injection here.
            //RegisterComponent<AppDataManager>();
            //RegisterComponent<DataServerProxy>();
            _appDataManager = gameObject.AddComponent<AppDataManager>();
            _dataServerClient = gameObject.AddComponent<DataServerProxy>();
            _bodySourceManager = gameObject.AddComponent<BodySourceManager>();
            _bodySnapshotCollector = gameObject.AddComponent<BodySnapshotCollector>();
            _volumeSourceManager = gameObject.AddComponent<VolumeSourceManager>();
            _volumeCollector = gameObject.AddComponent<VolumeCollector>();
            _distanceCollector = gameObject.AddComponent<DistanceCollector>();
        }

        // allow runtime registration of global objects
        //static public T RegisterComponent<T>() where T : Component
        //{
            //return Instance.GetOrAddComponent<T>();
            
        //}
    }
}