using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject dialoguePanel; // Reference to the dialogue panel
    public int itemID; // Unique ID for each item
    private bool isPlayerInRange; // Flag to check if player is within the collider
    private Dialogue dialogueScript; // Reference to the DialogueWithUnlockChoice script

    // Start is called before the first frame update
    void Start()
    {
        isPlayerInRange = false;
        dialogueScript = dialoguePanel.GetComponent<Dialogue>(); // Get the DialogueWithUnlockChoice script component
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.J))
        {
            dialoguePanel.SetActive(true); // Activate the dialogue panel
            StorylineState.hasCheckedDEIPoster = true;
            DialogueWithUnlockChoice.RegisterItemInteraction(itemID); // Register that this item has been interacted with
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Set the flag when player enters the collider
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Reset the flag when player exits the collider
            dialoguePanel.SetActive(false); // Deactivate the dialogue panel when player leaves
            dialogueScript.ResetDialogue(); // Reset the dialogue to the first line
        }
    }
}
