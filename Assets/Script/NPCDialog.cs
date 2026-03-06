using System.Collections;
using UnityEngine;
using System.Linq;

public class NPCDialog : MonoBehaviour
{
    public DialogUI dialogUI;
    [Tooltip("DialogStorage에서 가져올 때 사용할 place. Country와 다릅니다. 예: 'AIRPORT', 'CITY' 등")]
    public string currentPlace; // DialogStorage에서 가져올 때 사용할 place. Country와 다릅니다. 예: 'AIRPORT', 'CITY' 등

    private bool playerInRange = false;

    bool waitingForQuizClose = false;




    void Update()
    {
        if(!InputBinder.isInputEnabled) 
            return;
        if (dialogUI.IsOpen)
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
        var list = DialogStorage.GetByPlace(MapState.CurrentPlace);
        if (list.Count == 0)
        {
            Debug.LogWarning("해당 place에 Dialog 없음: " + currentPlace);
            return;
        }

        string[] topics = list.Select(d => d.topic).ToArray();

        // ⭐ 대사가 없으면 DialogBox 숨김
        if (string.IsNullOrWhiteSpace(dialogUI.dialogText.text))
        {
            dialogUI.dialogBox.SetActive(false);
        }
        else
        {
            dialogUI.dialogBox.SetActive(true);
        }

        dialogUI.ShowConversationList(topics, (selectedTopic) =>
        {
            var d = list.First(x => x.topic == selectedTopic);
            StartCoroutine(PlayDialog(d));
        });
    }
    IEnumerator PlayDialog(Dialog d)
    {
        // 대화 시작 시 대사박스 다시 켜기
        dialogUI.dialogBox.SetActive(true);

        foreach (var line in d.npc_script)
        {
            // 빈 대사는 스킵
            if (string.IsNullOrEmpty(line))
                continue;

            dialogUI.dialogBox.SetActive(true);
            dialogUI.Conversation(line);

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        }

        if (!string.IsNullOrEmpty(d.question))
        {
            dialogUI.dialogBox.SetActive(true);
            dialogUI.Conversation(d.question);
            JSBridge.OnQuestionShown(d.id);

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));

            dialogUI.Hide();

            yield break;
        }

        dialogUI.Hide();
    }
}
