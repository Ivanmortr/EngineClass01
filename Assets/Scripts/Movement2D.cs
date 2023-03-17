using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;
    [SerializeField] 
    private PlayerInput _playerInput;
    private IEnumerator _movementRoutine;
    private bool isMoving;
    private Vector2 _direction;
    [SerializeField]
    private UnityEvent onMovementStarted;
    [SerializeField]
    private UnityEvent onMovementStopped;
 
    public void SetDirection(InputAction.CallbackContext ctx) => _direction = ctx.ReadValue<Vector2>();
    protected void Start()
    {
        _playerInput.actions["Axis2D"].performed += _=>
        {
            if (_direction.magnitude == 0f || isMoving) return;
            isMoving = true;
            onMovementStarted.Invoke();
        };
         
        _playerInput.actions["Axis2D"].canceled += _=>
        {
            isMoving = false;
            onMovementStopped.Invoke();
        };
    }
 
    private void MoveBody() => transform.Translate(Vector3.right * _direction.x * _moveSpeed * Time.deltaTime);
 
    public void StartMoving()
    {
        _movementRoutine = MovementRoutine();
        StartCoroutine(_movementRoutine);
    }
 
    public void StopMoving()
    {
        StopCoroutine(_movementRoutine);
        _movementRoutine = null;
    }
 
    private IEnumerator MovementRoutine()
    {
        while (true)
        {
            MoveBody();
            yield return null;
        }
    }
}