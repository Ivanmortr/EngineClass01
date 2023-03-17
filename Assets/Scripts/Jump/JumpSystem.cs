using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Jump
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class JumpSystem : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rb2D;

        [SerializeField] 
        private PlayerInput _playerInput;
        [SerializeField, Range(0f, 15f)] 
        private float _jumpForce = 5f;

        private IAirController _airController;
        [SerializeReference]
        private IGroundingChecker _groundingChecker;
    
        //Events
        [SerializeField]
        private UnityEvent Jumped;
        [SerializeField] 
        private UnityEvent<float> Fell;
        [SerializeField] 
        private UnityEvent JumpStopped;
    
        // Routine for stay on air
        private IEnumerator jumpRoutine;

        public JumpSystem()
        {
            Inject(new AirController());
            Inject(new GroundingChecker());
        }

        public void Inject(IAirController airController) => _airController = airController;
        public void Inject(IGroundingChecker groundingChecker) => _groundingChecker = groundingChecker;

        private void Awake()
        {
            jumpRoutine = JumpRoutine();
        
            _playerInput.actions["jump"].performed += _=>
            {
                _airController.IsGrounding = _groundingChecker.GroundingByRaycast(transform.position);
                if (_airController.Jumping && !_airController.IsGrounding) return;
                Jumped.Invoke();
            };
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _groundingChecker.RayColor;
            Gizmos.DrawRay(transform.position + _groundingChecker.GroundRayPosition, Vector2.down * _groundingChecker.GroundRayDistance);
        }

        public void StartJumping()
        {
            AddImpulseVertical();
            _airController.Jumping = true;
            StartCoroutine(jumpRoutine);
        }

        public void StopJumping()
        {
            StopCoroutine(jumpRoutine);
            _airController.Jumping = false;
            _airController.IsFalling = false;
        }

        public void CheckGrounding(float velY)
        {
            _airController.IsGrounding = _groundingChecker.GroundingByRaycast(transform.position);
            if (_airController.IsGrounding && velY == 0f)
            {
                JumpStopped.Invoke();
            }
        }

        public void CheckFalling(float velY) => _airController.IsFalling = !_airController.IsGrounding && velY < 0f;

        private IEnumerator JumpRoutine()
        {
            while (true)
            {
                Fell.Invoke(_rb2D.velocity.y);
                yield return null;
            }
        }

        private void AddImpulseVertical() => _rb2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
