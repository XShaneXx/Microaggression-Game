using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamExit : MonoBehaviour
{
    public GameObject dialoguePanel; // Reference to the dialogue panel
    private DreamExitDialogue dialogueScript;
    private bool isPlayerInRange; // Flag to check if player is within the collider

    // Start is called before the first frame update
    void Start()
    {
        isPlayerInRange = false;
        dialogueScript = dialoguePanel.GetComponent<DreamExitDialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0)))
        {
            dialoguePanel.SetActive(true); // Activate the dialogue panel
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
            dialogueScript.ResetDialogue();
        }
    }
}
