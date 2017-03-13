using Assets.DataContracts;
using UnityEngine;

namespace Assets.Toolbox
{
    public abstract class AbstractDataServerClient : MonoBehaviour
    {
        public abstract void Send(GameSession session);
    }
}
