using UnityEngine;

public class NPCDialog : MonoBehaviour
{

    public DialogUI dialogUI;

    private bool playerInRange = false;
    private bool isTalking = false;

    [TextArea]
    public string dialogLine = "¾È³çÇÏ¼¼¿ä! ¿À´Ã ³¯¾¾°¡ ÁÁ³×¿ä.";
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isTalking)
            {
                dialogUI.Conversation(dialogLine);
                isTalking = true;
            }
            else
            {
                dialogUI.Hide();
                isTalking = false;
            }
        }


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
