﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player")) {
            Debug.Log("I see you");
        }
    }
}