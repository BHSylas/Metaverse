using TMPro;
using UnityEngine;
using static UnityEditor.Rendering.MaterialUpgrader;

public class DialogUI : MonoBehaviour
{

    public GameObject panel;
    public TMP_Text dialogText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panel.SetActive(false);
        
    }

    public void Conversation(string line)
    {
        panel.SetActive(true);
        dialogText.text = line;
    }

    public void ShowConversationList(string[] titles)
    {
        Debug.Log("ShowConversationList CALLED");

        panel.SetActive(true);

    }

    public void Hide()
    {
               panel.SetActive(false);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
