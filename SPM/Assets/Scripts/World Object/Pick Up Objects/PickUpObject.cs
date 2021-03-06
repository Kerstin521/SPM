﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    [SerializeField] protected int durability;
    protected bool active = false;
    [HideInInspector] public bool holding = false;
    private Transform player;
    private float pullForce;
    protected Rigidbody rb;
    [SerializeField] private float distanceToGrab = 0.1f;
    [SerializeField] protected float damage = 10f;
    [SerializeField] private float lowestVelocityToDoDamage = 5.0f;
    protected Transform pullPoint;
    protected bool thrown = false;

    private int geometry = 9;

    void Awake()
    {
        pullPoint = GameObject.Find("PullPoint Close").transform;
        player = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if(active && !holding) {
            //rb.useGravity = false;
            //if (!(Vector3.Distance(transform.position, pullPoint.position) < distanceToGrab))
            //{
            //    transform.position += (pullPoint.position - transform.position).normalized * pullForce * Time.deltaTime;
            //}
            transform.position += (pullPoint.position - transform.position).normalized * pullForce * Time.deltaTime;

            if (Vector3.Distance(transform.position, pullPoint.position) < distanceToGrab)
            {

                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                //rb.useGravity = false;
                transform.SetParent(player.GetComponentInChildren<GravityGun>().transform);

                holding = true;
            }
        }
    }

    public void Drop()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        transform.SetParent(null);
        active = false;
        holding = false;
        thrown = true;
        //rb.useGravity = true;
    }

    public void Pull (float pullForce)
    {
        this.pullForce = pullForce;
        active = true;
    }

    protected void LoseDurability()
    {
        durability--;
        if(durability <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter (Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") && rb.velocity.magnitude >= lowestVelocityToDoDamage) {
            EnemyBaseState enemyState = (EnemyBaseState)collision.collider.GetComponent<Enemy>().GetCurrentState();
            enemyState.Damage(rb.velocity.magnitude, rb.mass, damage);
            LoseDurability();
        }
    }
}
