using System;
using Player;
using UnityEngine;

namespace UI
{
    public class Billboard : MonoBehaviour
    {
        private Transform _playerTransform;
        
        private void Awake()
        {
            _playerTransform = FindObjectOfType<PlayerController>().transform;
        }

        private void Update()
        {
            if (_playerTransform == null) return;
            
            transform.LookAt(_playerTransform);
        }
    }
}