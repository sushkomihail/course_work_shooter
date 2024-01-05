using UnityEngine;

namespace Player
{
    public interface ICameraAngles
    {
        public float XAngle { get; }
        public float XAngleBeforeShooting { get; }
        public Vector3 RecoilAngles { get; }
        public void SetRecoilAngles(Vector3 recoilAngles);
    }
}