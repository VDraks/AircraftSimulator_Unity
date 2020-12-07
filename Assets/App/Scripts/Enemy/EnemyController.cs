using App.Scripts.Aircraft;
using App.Scripts.Utils;
using UnityEngine;

namespace App.Scripts.Enemy {
    public class EnemyController : MonoBehaviour {

        [SerializeField]
        private AircraftController _aircraft = null;
        
        public PositionGenerator.PositionSettings PositionSettings;

        public Vector3 NextPoint;

        public float TargetDistanceToPoint = 5f;

        private bool _isTurning = false;

        private void Start() {
            SetNextPoint();
        }

        private void Update() {
            if (Vector3.Distance(transform.position, NextPoint) < TargetDistanceToPoint) {
                SetNextPoint();
            }

            var dot = Vector3.Dot(_aircraft.GetDirection(), TargetDirection());
            var directionAngle = Vector3.SignedAngle(_aircraft.GetDirection(), TargetDirection(), Vector3.up);

            Debug.Log("distance: " + Vector3.Distance(transform.position, NextPoint) + ", dot: " + dot +
                      ", directionAngle: " + directionAngle + ", angles: " + _aircraft.transform.rotation.eulerAngles);

            
            

            if (dot < 0.90f) {
                _isTurning = true;
            }
            if (dot > 0.95f) {
                _isTurning = false;
            }

            var zAngle = _aircraft.transform.rotation.eulerAngles.z;
            if (zAngle > 180) zAngle -= 360;

            if (_isTurning) {
                if (zAngle < 85) {
                    _aircraft.SetAngles(1, 0);
                } else if (zAngle > 95) {
                    _aircraft.SetAngles(-1, 0);  
                } else {
                    if (directionAngle > 0) {
                        _aircraft.SetAngles(0, 0.5f);
                    } else {
                        _aircraft.SetAngles(0, -0.5f);
                    }
                }
            } else {
                if (zAngle > 5) {
                    _aircraft.SetAngles(-1, 0);
                } else if (zAngle < -5) {
                    _aircraft.SetAngles(1, 0);  
                } else {
                    _aircraft.SetAngles(0, 0);
                }
            }
        }

        private Vector3 TargetDirection() {
            return (NextPoint - transform.position).normalized;
        }

        private void SetNextPoint() {
            NextPoint = PositionGenerator.GetRandomPosition(PositionSettings);
            NextPoint.y = transform.position.y;
        }
    }
}