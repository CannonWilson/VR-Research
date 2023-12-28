using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunMotorOnBallEnter : MonoBehaviour
{
    // Reference to the script with the function you want to trigger
    public BluetoothTest btScript;

        private void OnCollisionEnter(Collision collision)
    {
        // Check if the entering collider has the tag "Ball"
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Trigger the function in the other script
            btScript.RunMotorForCollision();
        }
    }
}
