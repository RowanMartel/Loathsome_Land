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
        if (type == interactTypes.dialogue) anim = GetComponent<Animator>();
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
    }

    private void Update()
    {
        if (!vars.talking && anim) anim.SetBool("Talking", false);
    }

    void InteractNothing()
    {
        if (!vars.canTalk) return;
        Debug.Log("Nothing happened.");
        interacted = true;
    }
    void InteractPickup()
    {
        if (!vars.canTalk) return;
        inv.AddItem(pickupType);
        info.Show("Picked up a " + pickupType + ".");
        interacted = true;
    }
    void InteractInfo()
    {
        if (!vars.canTalk) return;
        info.Show(infoText);
        interacted = true;
    }
    void InteractDialogue()
    {
        if (!vars.canTalk) return;
        dialogue.StartConversation(dialogueText, dialogueImage, this);
        inv.AddItem(pickupType);
        anim.SetBool("Talking", true);
        if (talkingAnimation) switch (talkingAnimation.name)
        {
            case "EdTalk":
                anim.SetFloat("AnimID", 0);
                break;
            case "HannibalTalk":
                anim.SetFloat("AnimID", 0.25f);
                break;
            case "HaroldHatTalk":
                anim.SetFloat("AnimID", 0.5f);
                break;
            case "HaroldTalk":
                anim.SetFloat("AnimID", 0.75f);
                break;
            case "SamTalk":
                anim.SetFloat("AnimID", 1);
                break;
        }
    }

    public void DialogueDone()
    {
        interacted = true;
    }
}