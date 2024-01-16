using UnityEngine;

namespace Player
{
    public interface IRecoilControlAngles
    {
        public float XAngle { get; }
        public float XAngleBeforeShooting { get; }
        public Vector3 RecoilAngles { get; }
        public void SetRecoilAngles(Vector3 recoilAngles);
    }
}