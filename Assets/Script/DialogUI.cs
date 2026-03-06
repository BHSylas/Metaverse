using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{

    public GameObject panel;
    public TMP_Text dialogText;

    public Transform choiceRoot;
    public Button choiceButtonPrefab;

    public GameObject dialogBox;


    public bool IsOpen => panel.activeSelf;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panel.SetActive(false);
        dialogText.text = "";
    }

    public void Conversation(string line)
    {
        if (string.IsNullOrEmpty(line))
        {
            panel.SetActive(false);
            return;
        }

        ClearChoices();
        panel.SetActive(true);
        dialogText.text = line;
    }

    public void ShowConversationList(string[] titles, Action<string> onSelected)
    {
        panel.SetActive(true);
        //dialogText.text= "";

        ClearChoices();

        foreach(string title in titles)
        {
            Button btn = Instantiate(choiceButtonPrefab, choiceRoot);
            btn.GetComponentInChildren<TMP_Text>().text = title;

            btn.onClick.AddListener(() =>
            {
                ClearChoices();
                onSelected?.Invoke(title);
            });
        }
    }

    public void Hide()
    {
        ClearChoices();
        dialogText.text = "";
        panel.SetActive(false);
    }
    void ClearChoices()
    {
        for (int i = choiceRoot.childCount - 1; i >= 0; i--)
        {
            Destroy(choiceRoot.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
}
