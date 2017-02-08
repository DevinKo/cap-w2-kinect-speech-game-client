using Assets.DataContracts;
using UnityEngine;

namespace Assets.Toolbox
{
    public abstract class AbstractAppDataManager : MonoBehaviour
    {
        public abstract void Save(BodySnapshot data);
        public abstract void Save(AudioSnapshot data);
    }
}