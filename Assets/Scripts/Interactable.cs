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

    [SerializeField]
    string infoText;
    [SerializeField]
    List<string> dialogueText;
    [SerializeField]
    Sprite dialogueImage;
    [SerializeField]
    Inventory.itemTypes pickupType;
    [SerializeField]
    AnimationClip talkingAnimation;

    [HideInInspector]
    public bool interacted;

    Inventory inv;
    Info info;
    Dialogue dialogue;
    GlobalVariables vars;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        vars = FindObjectOfType<GlobalVariables>();
        dialogue = FindObjectOfType<Dialogue>();
        inv = FindObjectOfType<Inventory>();
        info = FindObjectOfType<Info>();
        interacted = false;
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

    private void Update()
    {
        if (!vars.talking) anim.SetBool("Talking", false);
    }

    void InteractNothing()
    {
        Debug.Log("Nothing happened.");
    }
    void InteractPickup()
    {
        inv.AddItem(pickupType);
        info.Show("Picked up a " + pickupType + ".");
    }
    void InteractInfo()
    {
        info.Show(infoText);
    }
    void InteractDialogue()
    {
        if (!vars.canTalk) return;
        dialogue.StartConversation(dialogueText, dialogueImage);
        inv.AddItem(pickupType);
        anim.SetBool("Talking", true);
        switch (talkingAnimation.name)
        {
            case "EdTalk":
                anim.SetFloat("AnimID", 0);
                break;
            case "HannibalTalk":
                anim.SetFloat("AnimID", 0.25f);
                break;
        }
    }
}