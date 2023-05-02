using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Template method pattern for interactable objects
public abstract class Interactable : MonoBehaviour
{
    // Add or remove InteractionEvent component to game object
    public bool useEvents;
    
    [SerializeField]
    // Message to display when player is looking at the interactable object
    public string promptMessage;

    // Called by the player when they interact with the object
    public void BaseInteract()
    {
        if(useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }

    // Override this method to handle the interaction
    protected virtual void Interact()
    {

    }
}
