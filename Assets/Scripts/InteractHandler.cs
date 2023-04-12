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
    [Header("Each element represents if interaction with the player is required.")]
    [SerializeField]
    List<bool> advanceAutomatically;

    List<Interactable> interactables;
    int state;
    Inventory inv;

    void Start()
    {
        advanceAutomatically.Add(false); // Stops some bug from occurring I guess
        interactables = new List<Interactable>();
        inv = FindObjectOfType<Inventory>();
        state = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            interactables.Add(transform.GetChild(i).GetComponent<Interactable>());
            if (i > 0)
            {
                interactables[i].Disable();
            }
        }
    }

    void Update()
    {
        if (interactables[state].interacted || advanceAutomatically[state])
        {
            if (state == interactables.Count - 1)
            {
                if (interactables[state].type == Interactable.interactTypes.pickup) interactables[state].Disable();
                return;
            }

            if (inv.CheckItem(itemConditions[state])) inv.RemoveItem(itemConditions[state]);
            else
            {
                if (itemConditions[state] != Inventory.itemTypes.nothing) return;
            }

            interactables[state].Disable();
            state++;
            interactables[state].Enable();
            interactables[state].IndicationSound();
        }
    }
}