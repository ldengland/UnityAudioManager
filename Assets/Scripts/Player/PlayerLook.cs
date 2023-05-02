using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public Camera playerCamera;
    private float xRotaiton = 0f;

    [Header("Mouse Settings")]
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    // Start is called before the first frame update
    void Start()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Hide cursor
        Cursor.visible = false;
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // Calculate rotation, clamp it
        xRotaiton -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotaiton = Mathf.Clamp(xRotaiton, -80f, 80f);

        // Apply to camera
        playerCamera.transform.localRotation = Quaternion.Euler(xRotaiton, 0f, 0f);

        // Apply to player
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
