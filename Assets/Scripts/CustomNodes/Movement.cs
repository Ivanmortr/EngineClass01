using System;
using Unity.VisualScripting;
using UnityEngine;

namespace CustomNodes
{
    public enum TypeMovement
    {
        HORIZONTAL,
        VERTICAL,
        FREE
    };
        
    public class Movement: Unit
    {
        [DoNotSerialize]
        private ControlInput _inputTrigger;

        [DoNotSerialize]
        private ControlOutput _outputTrigger;

        [DoNotSerialize] 
        private ValueInput _typeMomentInput;
        private TypeMovement _typeMovment;
        [DoNotSerialize] 
        private ValueInput _moveSpeedInput;
        private float _moveSpeed;
        [DoNotSerialize] 
        private ValueInput _rigidbody2DInput;
        private Rigidbody2D _rigidbody2D;
        [DoNotSerialize] 
        private ValueInput _directionInput;
        private Vector2 _direction;
        
        protected override void Definition()
        {
            _inputTrigger = ControlInput("", ExecuteFlow);

            _typeMomentInput = ValueInput<TypeMovement>("Type Movement", TypeMovement.HORIZONTAL);
            _moveSpeedInput = ValueInput<float>("Move Speed", 0f);
            _rigidbody2DInput = ValueInput<Rigidbody2D>("Rigidbody2D");
            _directionInput = ValueInput<Vector2>("Direction", Vector2.zero);
            
            _outputTrigger = ControlOutput("");
        }

        private ControlOutput ExecuteFlow(Flow flow)
        {
            _typeMovment = flow.GetValue<TypeMovement>(_typeMomentInput);
            _moveSpeed = flow.GetValue<float>(_moveSpeedInput);
            _rigidbody2D = flow.GetValue<Rigidbody2D>(_rigidbody2DInput);
            _direction = flow.GetValue<Vector2>(_directionInput);

            switch (_typeMovment)
            {
                case TypeMovement.HORIZONTAL:
                    _rigidbody2D.MovePosition(_rigidbody2D.position + _direction.normalized * Vector2.right * _moveSpeed * Time.fixedDeltaTime);
                    break;
                case TypeMovement.VERTICAL:
                    _rigidbody2D.MovePosition(_rigidbody2D.position + _direction.normalized * Vector2.up * _moveSpeed * Time.fixedDeltaTime);
                    break;
                case TypeMovement.FREE:
                    _rigidbody2D.MovePosition(_rigidbody2D.position + _direction.normalized * _moveSpeed * Time.fixedDeltaTime);
                    break;
                default:
                    _rigidbody2D.MovePosition(_rigidbody2D.position + _direction.normalized * Vector2.right * _moveSpeed * Time.fixedDeltaTime);
                    break;
            }

            return _outputTrigger;
        }
    }
}