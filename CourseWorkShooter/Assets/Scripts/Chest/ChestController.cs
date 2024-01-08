using System;
using HealthSystem;
using UnityEngine;

namespace Chest
{
    public class ChestController : MonoBehaviour
    {
        [SerializeField] private ChestShaker _shaker;
        [SerializeField] private ChestHealth _health;

        private void Awake()
        {
            _health.Initialize(_shaker);
        }

        private void Update()
        {
            _shaker.Shake();
        }
    }
}