using UnityEngine;
using System;

namespace Assets.Toolbox
{

    public class Toolbox : MonoBehaviour
    {
        protected Toolbox() { } // guarantee this will be always a singleton only - can't use the constructor!

        // Should use some sort of dependency injection here.
        protected AbstractAppDataManager _appDataManager;
        protected AbstractDataServerClient _dataServerClient;
        protected IBodySourceManager _bodySourceManager;
        protected BodySnapshotCollector _bodySnapshotCollector;
        protected VolumeSourceManager _volumeSourceManager;
        protected VolumeCollector _volumeCollector;
        protected DistanceCollector _distanceCollector;
        protected Distance2SnapshotCollector _distance2Collector;
        protected GameEventHub _gameEventHub;
        protected AppAuth _appAuth;

        public AppAuth AppAuth
        {
            get
            {
                return _appAuth;
            }
        }

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

        public IBodySourceManager BodySourceManager
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

        public Distance2SnapshotCollector Distance2Collector
        {
            get
            {
                return _distance2Collector;
            }
        }

        public GameEventHub EventHub
        {
            get
            {
                return _gameEventHub;
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

            if (GameManager.instance._testing)
            {
                _bodySourceManager = gameObject.AddComponent<FakeBodySourceManager>();
            }
            else
            {
                _bodySourceManager = gameObject.AddComponent<BodySourceManager>();
            }

            _bodySnapshotCollector = gameObject.AddComponent<BodySnapshotCollector>();
            _volumeSourceManager = gameObject.AddComponent<VolumeSourceManager>();
            _volumeCollector = gameObject.AddComponent<VolumeCollector>();
            _distanceCollector = gameObject.AddComponent<DistanceCollector>();
            _distance2Collector = gameObject.AddComponent<Distance2SnapshotCollector>();
            _gameEventHub = new GameEventHub();
            _appAuth = gameObject.AddComponent<AppAuth>();
        }

        // allow runtime registration of global objects
        //static public T RegisterComponent<T>() where T : Component
        //{
            //return Instance.GetOrAddComponent<T>();
            
        //}
    }
}