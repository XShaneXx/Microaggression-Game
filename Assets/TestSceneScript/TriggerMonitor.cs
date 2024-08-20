using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMonitor : MonoBehaviour
{
    public Test testScript; // Reference to the Test script

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Call a method on the Test script when the player enters the trigger
            testScript.OnPlayerEnterTrigger();
        }
    }
}
