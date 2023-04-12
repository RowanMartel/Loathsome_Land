using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    public enum interactTypes
    {
        nothing,
        pickup,
        info,
        dialogue,
        goal
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
    [SerializeField]
    AudioClip indicationSound;
    [SerializeField]
    AudioClip interactSound;

    [HideInInspector]
    public bool interacted;

    Inventory inv;
    Info info;
    Dialogue dialogue;
    GlobalVariables vars;
    Animator anim;
    AudioSource audioSource;

    void Start()
    {
        if (type == interactTypes.dialogue) anim = GetComponent<Animator>();
        vars = FindObjectOfType<GlobalVariables>();
        dialogue = FindObjectOfType<Dialogue>();
        inv = FindObjectOfType<Inventory>();
        info = FindObjectOfType<Info>();
        audioSource = GetComponent<AudioSource>();
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
            case interactTypes.goal:
                InteractGoal();
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
        if (interactSound) audioSource.PlayOneShot(interactSound);
        interacted = true;
    }
    void InteractInfo()
    {
        if (!vars.canTalk) return;
        info.Show(infoText);
        if (interactSound) audioSource.PlayOneShot(interactSound);
        interacted = true;
    }
    void InteractDialogue()
    {
        if (!vars.canTalk) return;
        dialogue.StartConversation(dialogueText, dialogueImage, this);
        anim.SetBool("Talking", true);
        switch (talkingAnimation.name)
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
            default:
                anim.SetFloat("AnimID", 1.25f);
                break;
        }
        if (interactSound) audioSource.PlayOneShot(interactSound);
    }
    void InteractGoal()
    {
        SceneManager.LoadScene(1);
    }

    public void DialogueDone()
    {
        inv.AddItem(pickupType);
        interacted = true;
    }

    public void IndicationSound()
    {
        if (indicationSound) audioSource.PlayOneShot(indicationSound);
    }

    public void Disable()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void Enable()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}