using System;
using App.Scripts.Aircraft;
using App.Scripts.Player;
using App.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.Enemy {
    public class EnemySpawner : MonoBehaviour {
        private int _enemyCount = 0;

        [SerializeField]
        private EnemyController _enemyPrefab;
        [SerializeField]
        private PlayerController _player;

        public PositionGenerator.PositionSettings PositionSettings;
        public float MaxCount = 10;

        public event Action<EnemyController> EnemySpawned;

        private void Update() {
            Spawn();
        }

        private void Spawn() {
            if (_enemyCount >= MaxCount) return;

            var enemy = Instantiate(_enemyPrefab, PositionGenerator.GetRandomPosition(PositionSettings),
                GetRandomRotation());
            _enemyCount++;

            EnemySpawned?.Invoke(enemy);

            enemy.GetComponent<AircraftController>().Died += () => _enemyCount--;
        }

        private Quaternion GetRandomRotation() {
            return Quaternion.Euler(0, Random.Range(0, 360), 0);
        }
    }
}