using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace App.Scripts.Aircraft {
    public class WeaponController : MonoBehaviour {

        [SerializeField]
        private AircraftController _aircraft = null;
        
        [SerializeField]
        private Transform[] _weapons = null;
        
        [SerializeField]
        private Transform _bulletPrefab = null;
        
        [SerializeField]
        private float _bulletSpeed = 100f;
        [SerializeField]
        private float _bulletTimeout = 0.3f;
        
        private float _bulletTime = 0.0f;
        private bool _isFiring = false;
        
        public void Fire(InputAction.CallbackContext ctx) {
            _isFiring = ctx.performed;
        }

        private void Update() {
            CheckFire();
        }

        private void CheckFire() {
            _bulletTime -= Time.deltaTime;

            if (!_isFiring) return;
            if (_bulletTime > 0) return;
            _bulletTime = _bulletTimeout;
            
            foreach (var weapon in _weapons) {
                var bullet = Instantiate(_bulletPrefab, weapon.position, _aircraft.transform.rotation)
                    .GetComponent<Bullet>();
                bullet.Init(_bulletSpeed, _aircraft.GetDirection());
            }
        }
    }
}
