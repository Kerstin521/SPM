﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent : MonoBehaviour
{
    //Attributes
    private Vector3 velocity;
    [SerializeField] private float airResistanceCoefficient;
    [SerializeField] private float staticFrictionCoefficient;
    [SerializeField] private float dynamicFrictionCoefficient;
    [SerializeField] private float gravity;

    //Methods
    private void Update()
    {
        ApplyGravity();
        ApplyAirResistance();
    }

    public void AddVelocity(Vector3 vel)
    {
        velocity += vel;
    }

    protected void DecreaseVelocity(Vector3 vel)
    {
        velocity -= vel;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    protected void SetVelocity(Vector2 vel)
    {
        velocity = vel;
    }

    public void ApplyGravity()
    {
        velocity += Vector3.down * gravity * Time.deltaTime;

    }
    protected void ApplyAirResistance()
    {
        velocity *= Mathf.Pow(airResistanceCoefficient, Time.deltaTime);

    }

    public void ApplyFriction(float normalForceMagnitude)
    {

        if (velocity.magnitude < CalculateStaticFriction(normalForceMagnitude))
        {


            velocity = Vector3.zero;
        }
        else
        {

            velocity += -velocity.normalized * CalculateDynamicFriction(normalForceMagnitude);

        }
    }

    protected float CalculateStaticFriction(float normalForceMagnitude)
    {
        return Functions.CalculateFriction(normalForceMagnitude, staticFrictionCoefficient);
    }

    protected float CalculateDynamicFriction(float normalForceMagnitude)
    {
        return Functions.CalculateFriction(normalForceMagnitude, dynamicFrictionCoefficient);
    }



    protected virtual void CheckCollision ()
    {

    }

}
