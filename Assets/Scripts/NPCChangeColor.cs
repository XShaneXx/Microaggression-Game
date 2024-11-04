using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChangeColor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color startColor = Color.white;
    public Color targetColor = Color.green;
    public float colorChangeSpeed = 2f;

    private bool isChangingColor = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = startColor; // Set initial color
    }

    // Update is called once per frame
    void Update()
    {
        if (isChangingColor)
        {
            ChangeColor();
        }
    }

    // Method to handle color change logic
    private void ChangeColor()
    {
        float t = Mathf.PingPong(Time.time * colorChangeSpeed, 1f);
        spriteRenderer.color = Color.Lerp(startColor, targetColor, t);
    }

    // Trigger events to detect if the player enters or exits the range
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartChangingColor();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopChangingColor();
        }
    }

    // Method to start changing the color
    public void StartChangingColor()
    {
        isChangingColor = true;
    }

    // Method to stop changing the color
    public void StopChangingColor()
    {
        isChangingColor = false;
        spriteRenderer.color = startColor; // Reset color when stopping
    }
}
