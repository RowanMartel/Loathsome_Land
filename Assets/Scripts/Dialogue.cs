using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    Image panel;
    SpriteRenderer speaker;
    TMP_Text dialogueTmp;
    Queue<string> dialogueText;
    GlobalVariables vars;
    float timer;

    void Start()
    {
        vars = FindObjectOfType<GlobalVariables>();
        panel = GetComponentInChildren<Image>();
        speaker = GetComponentInChildren<SpriteRenderer>();
        dialogueTmp = GetComponentInChildren<TMP_Text>();
        dialogueText = new Queue<string>();
        Hide();
    }

    void Hide()
    {
        vars.canTalk = false;
        timer = 0;
        speaker.enabled = false;
        vars.talking = false;
        dialogueText.Clear();
        panel.enabled = false;
        dialogueTmp.text = "";
    }

    private void Update()
    {
        if (vars.talking && Input.GetKeyDown("space")) NextLine();

        if (!vars.talking) timer += Time.deltaTime;
        if (timer >= 0.1f) vars.canTalk = true;
    }

    public void StartConversation(List<string> dialogueList, Sprite speaker)
    {
        if (!vars.canTalk) return;
        vars.talking = true;
        dialogueText.Clear();
        panel.enabled = true;
        this.speaker.enabled = true;
        this.speaker.sprite = speaker;
        foreach (string line in dialogueList)
            dialogueText.Enqueue(line);
        NextLine();
    }

    void NextLine()
    {
        if (dialogueText.Count == 0)
        {
            Hide();
            return;
        }
        dialogueTmp.text = dialogueText.Dequeue();
    }
}