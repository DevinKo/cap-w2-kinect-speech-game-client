using Assets.DataContracts;
using UnityEngine;
using Windows.Kinect;

namespace Assets.Toolbox
{
    public abstract class AbstractAppDataManager : MonoBehaviour
    {
        public abstract void Save(BodySnapshot data);
        public abstract void Save(AudioSnapshot data);
        public abstract void Save(MaxReach maxReach, JointType joint);
        public abstract MaxReach GetMaxReach(JointType joint);
        public abstract GameSession GetSession();
        public abstract void Save(GameSession session);
        public abstract GameSettings GetGameSettings();
        public abstract void Save(GameSettings settings);
    }
}