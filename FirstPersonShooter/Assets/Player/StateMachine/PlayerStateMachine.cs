using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    //State Variables
    private PlayerBaseState current_state;
    public PlayerGroundState ground_state = new PlayerGroundState();
    public PlayerAirState air_state = new PlayerAirState();

    //Player Variables
    [HideInInspector] public CharacterController character_controller;
    [HideInInspector] public Vector2 move_input = Vector2.zero;
    [HideInInspector] public Vector3 player_velocity = Vector3.zero;

    [HideInInspector] public bool jump_button_pressed = false;

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        move_input = context.ReadValue<Vector2>();
    }

    public void GetJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) jump_button_pressed = true;
        if (context.phase == InputActionPhase.Canceled) jump_button_pressed = false;
    }

    void Start()
    {
      
        character_controller = GetComponent<CharacterController>();

      
        current_state = ground_state;

    
        current_state.EnterState(this);
    }

    void Update()
    {
        current_state.UpdateState(this);
    }


    public void SwitchState(PlayerBaseState cur_state, PlayerBaseState new_state)
    {
        cur_state.ExitState(this);
        current_state = new_state;
        current_state.EnterState(this);
    }

    public void Set_Velocity(float max_speed, float acceleration, float friction_multiplier)
    {
      
        player_velocity += (transform.right * move_input.x + transform.forward * move_input.y) * acceleration;
        Vector2 xz_velocity = new Vector2(player_velocity.x, player_velocity.z);
        xz_velocity = Vector2.ClampMagnitude(xz_velocity, max_speed);

      
        if (move_input == Vector2.zero)
        {
          
            xz_velocity *= friction_multiplier;
        }

        
        player_velocity = new Vector3(xz_velocity.x, player_velocity.y, xz_velocity.y);
    }

    public void Move()
    {
      
        character_controller.Move(player_velocity * Time.deltaTime);
    }

}
