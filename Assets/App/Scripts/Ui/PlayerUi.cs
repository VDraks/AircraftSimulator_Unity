using System.Collections.Generic;
using App.Scripts.Enemy;
using App.Scripts.Player;
using TMPro;
using UnityEngine;

namespace App.Scripts.Ui {
    public class PlayerUi : MonoBehaviour {

        [SerializeField]
        private DistanceUi _distanceUiPrefab = null;
        
        [SerializeField]
        private RectTransform _textContainer = null;

        [SerializeField]
        private ScoreManager _scoreManager = null;
        
        [SerializeField]
        private TextMeshProUGUI _textScore = null;
        
        [SerializeField]
        private PlayerController _player = null;

        private readonly List<DistanceUi> _distanceList = new List<DistanceUi>();
        
        private void Update() {
            foreach (var text in _distanceList) {
                text.gameObject.SetActive(false);
            }

            var enemies = FindObjectsOfType<EnemyController>();

            for (int i = 0; i < enemies.Length; i++) {
                ShowEnemy(enemies[i], i);
            }

            _textScore.text = "Score: " + _scoreManager.GetScore();
        }

        private void ShowEnemy(EnemyController enemy, int index) {
            var screenPoint = Camera.main.WorldToScreenPoint(enemy.transform.position);

            while (_distanceList.Count <= index) {
                _distanceList.Add(Instantiate(_distanceUiPrefab, _textContainer));
            }

            var distanceUi = _distanceList[index];
            distanceUi.gameObject.SetActive(true);

            var distance = Vector3.Distance(_player.transform.position, enemy.transform.position);

            distanceUi.SetData(screenPoint, distance);
        }
    }
}