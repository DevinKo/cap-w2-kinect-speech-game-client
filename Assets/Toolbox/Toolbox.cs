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

        void Awake()
        {
            // initialization code here
            // Should use some sort of dependency injection here.
            //RegisterComponent<AppDataManager>();
            //RegisterComponent<DataServerProxy>();
            _appDataManager = gameObject.AddComponent<AppDataManager>();
            _dataServerClient = gameObject.AddComponent<DataServerProxy>();
            
        }

        // allow runtime registration of global objects
        //static public T RegisterComponent<T>() where T : Component
        //{
            //return Instance.GetOrAddComponent<T>();
            
        //}
    }
}