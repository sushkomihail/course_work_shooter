using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;
using UnityEngine.Events;

namespace SpawnSystem
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private EnemiesCollection _collection;
        
        public static readonly UnityEvent<GameObject> OnEnemyDeath = new UnityEvent<GameObject>();

        private readonly List<EnemyController> _enemiesOnScene = new List<EnemyController>();

        private void Awake()
        {
            OnEnemyDeath.AddListener(RemoveDiedEnemyFromList);
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
                EnemyController enemyPrefab = _collection.NextEnemy;
                Vector3 spawnPosition = point.position + point.up * enemyPrefab.SpawnYOffset;
                EnemyController enemy = Instantiate(enemyPrefab, spawnPosition, point.rotation);
                _enemiesOnScene.Add(enemy);
            }
        }

        private void RemoveDiedEnemyFromList(GameObject diedEnemy)
        {
            if (diedEnemy.TryGetComponent(out EnemyController enemy))
            {
                _enemiesOnScene.Remove(enemy);
                Destroy(diedEnemy);
            }

            if (_enemiesOnScene.Count == 0)
            {
                StartCoroutine(SpawnWave());
            }
        }
    }
}