using System.Collections;
using System.Collections.Generic;
using Collections;
using UnityEngine;

namespace SpawnSystem
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private EnemiesCollection _collection;
        [SerializeField] private int _startSpawnEnemiesCount = 4;
        [SerializeField] private float _spawnDelay = 1.5f;

        private int _spawnPointsCount;
        private int _currentSpawnEnemiesCount;
        private int _enemiesOnSceneCount;

        private void Awake()
        {
            _spawnPointsCount = _spawnPoints.Count;
            _currentSpawnEnemiesCount = _startSpawnEnemiesCount;
            
            EventManager.OnEnemyDeath.AddListener(OnEnemyDeath);
            
            StartCoroutine(SpawnWave());
        }

        private IEnumerator SpawnWave()
        {
            if (_spawnPoints == null || _spawnPoints.Count == 0) yield break; 
            
            int needToSpawnCount = _currentSpawnEnemiesCount;

            while (needToSpawnCount != 0)
            {
                yield return new WaitForSeconds(_spawnDelay);
                
                List<Transform> remainingPoints = new List<Transform>(_spawnPoints);
                int spawnsCount = GetSpawnsCountInCycle(ref needToSpawnCount);

                for (int j = 0; j < spawnsCount; j++)
                {
                    Transform spawnPoint = GetNextSpawnPoint(remainingPoints);
                    Instantiate(_collection.NextEnemy, spawnPoint.position, spawnPoint.rotation);
                    _enemiesOnSceneCount += 1;
                }
            }

            _currentSpawnEnemiesCount += 1;
        }

        private Transform GetNextSpawnPoint(List<Transform> remainingPoints)
        {
            int randomIndex = Random.Range(0, remainingPoints.Count - 1);
            Transform nextPoint = remainingPoints[randomIndex];
            remainingPoints.Remove(nextPoint);
            return nextPoint;
        }

        private int GetSpawnsCountInCycle(ref int needToSpawnCount)
        {
            int spawnsCount = Mathf.Min(needToSpawnCount, _spawnPointsCount);
            needToSpawnCount -= spawnsCount;
            return spawnsCount;
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