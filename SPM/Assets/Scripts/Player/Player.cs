﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{


    //Attributes

    public float groundCheckDistance;
    public GameObject mainCamera;
    public LayerMask layerMask;
    public float skinWidth;
    [HideInInspector] public CapsuleCollider capsuleCollider;
    [HideInInspector] public PhysicsComponent physics;
    public GravityGun gravityGun;


    public GameObject respawnPoint;
    public float startHealth;
    private float health;


    protected override void Awake()
    {
        health = startHealth;
        physics = GetComponent<PhysicsComponent>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        base.Awake();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Damage(100);
        }
    
    }

    public void Damage(float damage)
    {
        health -= damage;
        Debug.Log("Player takes " + damage + " damage.");
        Debug.Log("Player has " + health + " health.");
        if (health <= 0)
        {
            Respawn();
        }
        else if (health <= 20)
        {
            //Blinka rött;
        }
    }

    public void Respawn()
    {
        Debug.Log("Player respawned");
        health = startHealth;
        transform.position = respawnPoint.transform.position;
    }

}
