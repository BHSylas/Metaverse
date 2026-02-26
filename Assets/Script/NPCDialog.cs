using System.Collections;
using UnityEngine;
using System.Linq;

public class NPCDialog : MonoBehaviour
{
    public DialogUI dialogUI;
    [Tooltip("DialogStorage에서 가져올 때 사용할 place. Country와 다릅니다. 예: 'AIRPORT', 'CITY' 등")]
    public string currentPlace; // DialogStorage에서 가져올 때 사용할 place. Country와 다릅니다. 예: 'AIRPORT', 'CITY' 등

    private bool playerInRange = false;
    void Update()
    {
        if(!InputBinder.isInputEnabled) 
            return;
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Talk();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }


    public void Talk()
    {
        var list = DialogStorage.GetByPlace(currentPlace);

        if (list.Count == 0)
        {
            Debug.LogWarning("해당 place에 Dialog 없음: " + currentPlace);
            return;
        }

        string[] topics = list.Select(d => d.topic).ToArray();

        dialogUI.ShowConversationList(topics, (selectedTopic) =>
        {
            var d = list.First(x => x.topic == selectedTopic);
            StartCoroutine(PlayDialog(d));
        });
    }

    IEnumerator PlayDialog(Dialog d)
    {
        foreach (var line in d.npc_script)
        {
            dialogUI.Conversation(line);

            // 스페이스 누를 때까지 대기
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            // 스페이스에서 손을 뗄 때까지 대기
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space));
        }

        if (!string.IsNullOrEmpty(d.question))
        {
            dialogUI.Conversation(d.question);
            JSBridge.OnQuestionShown(d.id);
        }

        dialogUI.Hide();
    }
}
