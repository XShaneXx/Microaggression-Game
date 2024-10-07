using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWithUnlockChoice : MonoBehaviour
{
    public GameObject dialoguePanel; // Reference to the dialogue panel
    private DialogueWithUnlockChoice dialogueScriptWithUC; // Reference to the Dialogue script
    private SpriteRenderer spriteRenderer;
    public Color startColor = Color.white;
    public Color targetColor = Color.green;
    public float colorChangeSpeed = 2f;

    private bool isPlayerInRange; // Flag to check if player is within the collider
    private bool isChangingColor = false; // Flag to check if the color is changing

    // Start is called before the first frame update
    void Start()
    {
        isPlayerInRange = false;
        dialogueScriptWithUC = dialoguePanel.GetComponent<DialogueWithUnlockChoice>(); // Get the Dialogue script component
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        spriteRenderer.color = startColor; // Set initial color
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0)))
        {
            dialoguePanel.SetActive(true); // Activate the dialogue panel
        }

        if (isChangingColor)
        {
            ChangeColor();
        }
    }

    // Trigger events to detect if the player enters or exits the range
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Set the flag when player enters the collider
            StartChangingColor(); // Start changing color
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Reset the flag when player exits the collider
            StopChangingColor(); // Stop changing color and reset
            dialoguePanel.SetActive(false); // Deactivate the dialogue panel when player leaves
            dialogueScriptWithUC.ResetDialogue(); // Reset the dialogue to the first line
        }
    }

    // Method to start changing the color
    private void StartChangingColor()
    {
        isChangingColor = true;
    }

    // Method to stop changing the color
    private void StopChangingColor()
    {
        isChangingColor = false;
        spriteRenderer.color = startColor; // Reset color when stopping
    }

    // Method to handle color change logic
    private void ChangeColor()
    {
        float t = Mathf.PingPong(Time.time * colorChangeSpeed, 1f);
        spriteRenderer.color = Color.Lerp(startColor, targetColor, t);
    }
}
