using UnityEngine;

namespace App.Scripts.Utils {
    public class DieByTime : MonoBehaviour {
        public float TimeSeconds;

        private void Start() {
            Destroy(gameObject, TimeSeconds);
        }
    }
}