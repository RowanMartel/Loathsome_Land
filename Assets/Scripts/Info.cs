using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Info : MonoBehaviour
{
    const float showTime = 2;

    Image panel;
    TMP_Text infoText;
    float timer;
    bool active;

    void Start()
    {
        panel = GetComponentInChildren<Image>();
        infoText = GetComponentInChildren<TMP_Text>();
        Hide();
    }

    void Update()
    {
        if (active) timer += Time.deltaTime;
        if (timer >= showTime) Hide();
    }

    void Hide()
    {
        panel.enabled = false;
        infoText.text = "";
        timer = 0;
        active = false;
    }

    public void Show(string info)
    {
        timer = 0;
        panel.enabled = true;
        infoText.text = info;
        active = true;
    }
}