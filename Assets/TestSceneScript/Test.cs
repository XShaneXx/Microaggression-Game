using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public Camera cameraToFollow; // Reference to the camera that will follow the player
    public Vector3 offset = new Vector3(0, 0, -10); // Default offset between the player and the camera
    public testPlayer playerMovementScript; // Reference to the player's movement script
    public GameObject dialogue; // Reference to the dialogue GameObject

    private Vector3 targetCameraPosition = new Vector3(4.3f, -5.4f, -10f); // Target camera position
    private bool isMovingCamera = false; // Flag to track if the camera is moving
    private bool cameraReachedTarget = false; // Flag to check if the camera reached the target position

    // Start is called before the first frame update
    void Start()
    {
        // Set initial camera position
        if (playerTransform != null && cameraToFollow != null)
        {
            cameraToFollow.transform.position = playerTransform.position + offset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingCamera)
        {
            MoveCameraToTarget();
        }
        else if (!cameraReachedTarget && playerTransform != null && cameraToFollow != null)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        // Follow the player with the specified offset
        cameraToFollow.transform.position = playerTransform.position + offset;
    }

    void MoveCameraToTarget()
    {
        // Smoothly move the camera to the target position
        cameraToFollow.transform.position = Vector3.Lerp(cameraToFollow.transform.position, targetCameraPosition, Time.deltaTime);

        // Stop moving the camera if it's close enough to the target position
        if (Vector3.Distance(cameraToFollow.transform.position, targetCameraPosition) < 0.1f)
        {
            cameraToFollow.transform.position = targetCameraPosition;
            isMovingCamera = false;
            cameraReachedTarget = true; // Mark that the camera has reached the target and should no longer follow the player

            // Keep player movement disabled
            playerMovementScript.speed = 0f; // Ensure player speed stays at zero
            playerMovementScript.animator.SetFloat("Speed", 0f); // Keep animator speed at zero

            // Activate the dialogue GameObject
            if (dialogue != null)
            {
                dialogue.SetActive(true);
            }
        }
    }

    // This method is called when the player enters the testTrigger
    public void OnPlayerEnterTrigger()
    {
        isMovingCamera = true; // Start moving the camera
        playerMovementScript.enabled = false; // Disable player movement
        playerMovementScript.speed = 0f; // Set player speed to zero
        playerMovementScript.animator.SetFloat("Speed", 0f); // Set animator speed to zero
    }
}
