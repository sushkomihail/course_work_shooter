using System.Collections;
using System.Collections.Generic;
using Collections;
using Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace SpawnSystem
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private EnemiesCollection _collection;

        private int _enemiesOnSceneCount;

        private void Awake()
        {
            EventManager.OnEnemyDeath.AddListener(OnEnemyDeath);
        }

        private void Start()
        {
            StartCoroutine(SpawnWave());
        }

        private IEnumerator SpawnWave()
        {
            yield return new WaitForSeconds(1);
            
            foreach (Transform point in _spawnPoints)
            {
                EnemyController enemy = Instantiate(_collection.NextEnemy, point.position, point.rotation);
                _enemiesOnSceneCount += 1;
            }
        }

        private void OnEnemyDeath()
        {
            _enemiesOnSceneCount -= 1;
            
            if (_enemiesOnSceneCount == 0)
            {
                StartCoroutine(SpawnWave());
            }
        }
    }
}