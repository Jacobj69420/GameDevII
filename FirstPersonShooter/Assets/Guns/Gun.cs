using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using TMPro;

public class Gun : MonoBehaviour
{
    //Debug
    public TMP_Text debug_text;

    //Gun Variables
    public GunData gun_data;
    public Camera cam;
    protected Ray ray;

    //Ammo Variables
    protected int ammo_in_clip;

    //Shooting
    protected bool primary_fire_is_shooting = false;
    protected bool primary_fire_hold = false;
    protected float shoot_delay_timer = 0.0f;

    void Start()
    {
        //Set Variables
        ammo_in_clip = gun_data.ammo_per_clip;
    }

    void Update()
    {
        //Debug Text
        debug_text.text = "Ammo In Clip: " + ammo_in_clip.ToString();

        //Draw debuy ray to visualize where we are shooting from.
        //Debug.DrawRay(cam.transform.position, cam.transform.forward * 10000, Color.green);

        //Shooting
        PrimaryFire();

        //Subtract from shoot timer.
        if (shoot_delay_timer > 0) shoot_delay_timer -= Time.deltaTime;

    }

    public void GetPrimaryFireInput(InputAction.CallbackContext context)
    {
        print("here");

        //Checking for the initial button press.
        if (context.phase == InputActionPhase.Started)
        {
            primary_fire_is_shooting = true;
        }

        //Check if the gun is automatic.
        if (gun_data.automatic)
        {
            //Check if the hold was completed.
            if (context.interaction is HoldInteraction && context.phase == InputActionPhase.Performed)
            {
                primary_fire_hold = true;
            }
        }

        //Check if the button was released.
        if (context.phase == InputActionPhase.Canceled)
        {
            primary_fire_is_shooting = false;
            primary_fire_hold = false;
        }
    }

    public void GetSecondaryFireInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) SecondaryFire();
    }

    protected virtual void PrimaryFire()
    {

    }

    protected virtual void SecondaryFire()
    {

    }
}
