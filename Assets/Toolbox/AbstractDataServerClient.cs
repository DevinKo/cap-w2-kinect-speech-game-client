using Assets.DataContracts;
using UnityEngine;

namespace Assets.Toolbox
{
    public abstract class AbstractDataServerClient : MonoBehaviour
    {
        public abstract bool Send(BodySnapshot[] data);
        public abstract void SendAsFile(BodySnapshot[] snapshots);
    }
}
