using System.Linq;
using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    [Header("References")]
    public DialogUI dialogUI;

    [Header("Dialog Data")]
    public int npcId = 1;

    private bool playerInRange = false;
    private bool isTalking = false;

    private DialogChoice[] choices;
    private string[] dialogLines;
    private int currentIndex = 0;

    void Start()
    {
        DialogData data = DialogDB.LoadDialog(npcId);

        if (data == null)
        {
            Debug.LogError("DialogData is null");
            return;
        }

        choices = data.choices;
    }

    void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isTalking)
            {
                ShowChoices();
            }
            else
            {
                NextLine();
            }
                
        }
            
    }

    void ShowChoices()
    {
        isTalking = true;
        
        string[] titles = choices.Select(c => c.title).ToArray();
        dialogUI.ShowConversationList(titles, OnChoiceSelected);
    }

    void OnChoiceSelected(string title)
    {
        DialogChoice choice = System.Array.Find(choices, c => c.title == title);

        dialogLines = choice.lines;
        currentIndex = 0;

        if(dialogLines.Length == 0)
        {
            EndConversation();
            return;
        }

        dialogUI.Conversation(dialogLines[currentIndex]);
    }

    //void StartConversation()
    //{
    //    if(dialogLines == null || dialogLines.Length == 0)
    //    {
    //        Debug.LogWarning("대사 없음");
    //        return;
    //    }

    //    currentIndex = 0;
    //    isTalking = true;

    //    dialogUI.Conversation(dialogLines[currentIndex]);
    //}

    void NextLine()
    {
        currentIndex++;
        if(currentIndex < dialogLines.Length)
        {
            dialogUI.Conversation(dialogLines[currentIndex]);
        }
        else
        {
            EndConversation();
        }
    }    

    void EndConversation()
    {
        isTalking = false;
        dialogUI.Hide();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        isTalking = false;
        dialogUI.Hide();
    }
}
