using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    GlobalVariables vars;
    Interactable currentObj;

    void Start()
    {
        vars = FindObjectOfType<GlobalVariables>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space")) Interact();
    }

    void Interact()
    {
        if (!currentObj || vars.talking) return;
        currentObj.Interact();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        currentObj = collision.GetComponent<Interactable>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == GlobalVariables.layerInteract) currentObj = null;
    }
}