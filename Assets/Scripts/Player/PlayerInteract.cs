using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    private Camera playerCamera;

    [SerializeField]
    private float interactDistance = 3f;

    [SerializeField]
    private LayerMask mask;

    private PlayerUI playerUI;

    private InputManager inputManager;
    
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponent<PlayerLook>().playerCamera;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Create a ray from the camera, shooting forward
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);

        // Check if the ray hits something
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, interactDistance, mask))
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {

                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();

                playerUI.UpdateText(interactable.promptMessage);
                if(inputManager.OnFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }

        }
        else
        {
            playerUI.UpdateText(string.Empty);
        }
    }
}
