using System;
using UnityEngine;

namespace App.Scripts.Aircraft {
    public class CollisionDetector : MonoBehaviour {

        [SerializeField]
        private AircraftController _aircraft = null;
        
        private void OnCollisionEnter(Collision other) {
            Debug.Log("OnCollisionEnter: " + other.gameObject.name);
            
            var bullet = other.gameObject.GetComponent<Bullet>();
            if (bullet != null) {
                _aircraft.Die();
                Destroy(bullet.gameObject);
            }
        }

        private void OnTriggerEnter(Collider other) {
            Debug.Log("OnTriggerEnter: " + other.gameObject.name);
        }
    }
}