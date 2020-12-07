using System;
using App.Scripts.Aircraft;
using App.Scripts.Enemy;
using UnityEngine;

namespace App.Scripts.Player {
    public class ScoreManager : MonoBehaviour {
        private int _score;

        [SerializeField]
        private EnemySpawner _enemySpawner = null;

        private void Start() {
            _enemySpawner.EnemySpawned += enemy => {
                enemy.GetComponent<AircraftController>().Died += () => {
                    _score++;
                };
            };
        }

        public int GetScore() {
            return _score;
        }
    }
}