using Assets.DataContracts;
using UnityEngine;

namespace Assets.Toolbox
{
    public abstract class AbstractDataServerClient : MonoBehaviour
    {
        public abstract bool Send(BodySnapshot[] data);
        public abstract void Send(GameSession session);
        public abstract void SendAsFile(BodySnapshotJsonFile message);
        public abstract void SendSession(Session session);
    }
}
