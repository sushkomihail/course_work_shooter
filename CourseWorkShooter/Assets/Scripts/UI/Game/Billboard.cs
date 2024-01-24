using Player;
using UnityEngine;

namespace UI.Game
{
    public class Billboard : MonoBehaviour
    {
        private Transform _cameraHolder;
        
        private void Awake()
        {
            _cameraHolder = FindObjectOfType<PlayerController>()?.Camera.CameraHolder;
        }

        private void Update()
        {
            if (_cameraHolder == null) return;
            
            transform.rotation = Quaternion.LookRotation(_cameraHolder.forward);
        }
    }
}