using System;
using UnityEngine;

namespace App.Scripts.Aircraft {
    public class AircraftController : MonoBehaviour {
        
        [SerializeField]
        private Transform _head = null;

        public float DefaultSpeed = 30.0f;
        public float AccelerationSpeed = 50.0f;
        public float AngleSpeed = 5.0f;

        public event Action Died;
        
        private float _speed;
        private Transform _thisTransform;
        private Vector3 _rotation = Vector3.zero;
        
        [SerializeField]
        private GameObject _dieEffectPrefab = null;
        
        private void Awake() {
            _thisTransform = transform;
            _speed = DefaultSpeed;
        }

        public void Die() {
            Died?.Invoke();

            Instantiate(_dieEffectPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
        private void Update() {
            _thisTransform.localPosition += GetDirection() * _speed * Time.deltaTime;
            _thisTransform.localRotation *= Quaternion.Euler(_rotation * Time.deltaTime);

            // _thisTransform.localRotation = Damp(_thisTransform.localRotation,
            //     _thisTransform.localRotation * Quaternion.Euler(new Vector3(-pitch, 0, -roll) * AngleSpeed),
            //     5f,
            //     Time.deltaTime);
        }
        
        public Vector3 GetDirection() {
            return (_head.position - _thisTransform.position).normalized;
        }
        
        private Quaternion Damp(Quaternion a, Quaternion b, float lambda, float dt) {
            return Quaternion.Slerp(a, b, 1 - Mathf.Exp(-lambda * dt));
        }

        public void SetAngles(float roll, float pitch) {
            _rotation = new Vector3(pitch, 0, roll) * AngleSpeed;
        }

        public void SetAcceleration(bool enable) {
            _speed = enable ? AccelerationSpeed : DefaultSpeed;
        }
    }
}