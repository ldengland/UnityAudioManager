using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();
    }

    void FixedUpdate()
    {
        // Tell the player motor to move using the input from the player.
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate() 
    {
        // Tell the player look to look using the input from the player.
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    } 

    public PlayerInput.OnFootActions OnFoot => onFoot;

    private void OnEnable() 
    {
        onFoot.Enable();
    }

    private void OnDisable() 
    {
        onFoot.Disable();
    }
}
