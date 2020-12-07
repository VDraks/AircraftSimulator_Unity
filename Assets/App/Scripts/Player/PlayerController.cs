using App.Scripts.Aircraft;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Scripts.Player {
    public class PlayerController : MonoBehaviour {

        [SerializeField]
        private AircraftController _aircraft;

        private Vector2 _moveInput = Vector2.zero;
        

        public void Move(InputAction.CallbackContext ctx) {
            _moveInput = ctx.ReadValue<Vector2>();

            var roll =  -_moveInput.x;
            var pitch = -_moveInput.y;
            _aircraft.SetAngles(roll, pitch);
        }
        
        public void Acceleration(InputAction.CallbackContext ctx) {
            _aircraft.SetAcceleration(ctx.performed);
        }
    }
}