using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : MonoBehaviour

{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationSpeed;

    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _smothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        SetPlayerVelocity();
        RotateInDirectionOfInput();
    }

    private void SetPlayerVelocity()
    {
        _smothedMovementInput = Vector2.SmoothDamp(_smothedMovementInput,
            _movementInput,
            ref _movementInputSmoothVelocity,
            0.1f);


        _rigidbody.linearVelocity = _movementInput * _speed;
    }

    private void RotateInDirectionOfInput()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smothedMovementInput);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.MoveRotation(rotation);
    }
    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }
}