using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public enum interactTypes
    {
        nothing,
        pickup,
        info,
        dialogue
    }
    public interactTypes type;
    public string infoText;
    public List<string> dialogueText;
    public RawImage dialogueImage;
    public Inventory.itemTypes pickupType;

    [HideInInspector]
    public bool interacted;

    Inventory inv;

    void Start()
    {
        inv = FindObjectOfType<Inventory>();
        interacted = false;
    }

    void Update()
    {
        
    }

    public void Interact()
    {
        switch (type)
        {
            case interactTypes.nothing:
                InteractNothing();
                break;
            case interactTypes.pickup:
                InteractPickup();
                break;
            case interactTypes.info:
                InteractInfo();
                break;
            case interactTypes.dialogue:
                InteractDialogue();
                break;
        }
        interacted = true;
    }

    void InteractNothing()
    {
        Debug.Log("Nothing happened.");
    }
    void InteractPickup()
    {
        Debug.Log("Picked up a " + pickupType + ".");
        inv.AddItem(pickupType);
    }
    void InteractInfo()
    {
        Debug.Log("Received info: \"" + infoText + "\".");
    }
    void InteractDialogue()
    {
        Debug.Log("Initiated conversation.");
    }
}