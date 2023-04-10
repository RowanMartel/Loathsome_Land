using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractHandler : MonoBehaviour
{
    [Header("# of interactables should be list length + 1.")]
    [Header("Each element represents the item needed to advance to the next object.")]
    [SerializeField]
    List<Inventory.itemTypes> itemConditions;

    List<Interactable> interactables;
    int state;
    Inventory inv;

    void Start()
    {
        interactables = new List<Interactable>();
        inv = FindObjectOfType<Inventory>();
        state = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            interactables.Add(transform.GetChild(i).GetComponent<Interactable>());
            if (i > 0)
            {
                interactables[i].GetComponent<SpriteRenderer>().enabled = false;
                interactables[i].GetComponent<BoxCollider2D>().enabled = false;
                interactables[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (interactables[state].interacted)
        {
            if (state == interactables.Count - 1)
            {
                if (interactables[state].type == Interactable.interactTypes.pickup) gameObject.SetActive(false);
                return;
            }

            if (inv.CheckItem(itemConditions[state])) inv.RemoveItem(itemConditions[state]);
            else
            {
                if (itemConditions[state] != Inventory.itemTypes.nothing) return;
            }

            interactables[state].GetComponent<SpriteRenderer>().enabled = false;
            interactables[state].GetComponent<BoxCollider2D>().enabled = false;
            interactables[state].transform.GetChild(0).gameObject.SetActive(false);
            state++;
            interactables[state].GetComponent<SpriteRenderer>().enabled = true;
            interactables[state].GetComponent<BoxCollider2D>().enabled = true;
            interactables[state].transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}