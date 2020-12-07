using System.Collections;
using UnityEngine;

namespace App.Scripts.Aircraft {
    public class Bullet : MonoBehaviour {
        
        private float _speed;
        private Vector3 _direction;
        private void Start() {
            StartCoroutine(Die());
        }

        public void Init(float speed, Vector3 direction) {
            _speed = speed;
            _direction = direction;
        }

        private void Update() {
            transform.localPosition += _direction * _speed * Time.deltaTime;
        }

        private IEnumerator Die() {
            yield return new WaitForSeconds(3);
            
            Destroy(gameObject);
        }
    }
}