using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCItself : MonoBehaviour
{
    public GameObject dialoguePanel; // Reference to the dialogue panel
    private Dialogue dialogueScript; // Reference to the Dialogue script
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public Color startColor = Color.white; // Initial color
    public Color targetColor = Color.yellow; // Target color when player is in range
    public float colorChangeSpeed = 2f; // Speed of color change

    private bool isPlayerInRange; // Flag to check if player is within the collider
    private bool isChangingColor = false; // Flag to control color change

    // Start is called before the first frame update
    void Start()
    {
        isPlayerInRange = false;
        dialogueScript = dialoguePanel.GetComponent<Dialogue>(); // Get the Dialogue script component
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
            ChangeColor(); // Call color change function
        }
    }

    // Change color logic when player is in range
    private void ChangeColor()
    {
        float t = Mathf.PingPong(Time.time * colorChangeSpeed, 1f);
        spriteRenderer.color = Color.Lerp(startColor, targetColor, t); // Interpolate between start and target colors
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Set the flag when player enters the collider
            StartChangingColor(); // Start changing color when player enters range
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Reset the flag when player exits the collider
            StopChangingColor(); // Stop color change and reset to start color
            dialoguePanel.SetActive(false); // Deactivate the dialogue panel when player leaves
            dialogueScript.ResetDialogue(); // Reset the dialogue to the first line
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
}
