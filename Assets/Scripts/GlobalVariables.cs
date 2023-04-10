using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    // Important layers
    [HideInInspector]
    public const int layerInteract = 3;

    [HideInInspector]
    public bool talking;
    [HideInInspector]
    public bool canTalk;

    void Start()
    {
        talking = false;
        canTalk = true;
    }
}