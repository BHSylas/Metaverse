using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCDialogNoMove : MonoBehaviour
{
    public DialogUI dialogUI;

    [Header("Unity Default Dialog")]
    [TextArea]
    public string[] defaultDialogs;

    [Header("Dialog Place")]
    public string currentPlace;

    [Header("Return Scene")]
    public string returnSceneName;

    void Update()
    {
        if (!InputBinder.isInputEnabled)
            return;

        if (dialogUI.IsOpen) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Talk();
        }
    }
    void Talk()
    {
        var list = DialogStorage.GetByPlace(MapState.CurrentPlace);

        if (list.Count == 0)
        {
            Debug.LogWarning("해당 place에 Dialog 없음: " + MapState.CurrentPlace);
            return;
        }

        var topics = list.Select(d => d.topic).ToList();

        // ⭐ 마지막에 돌아가기 추가
        topics.Add("돌아가기");

        // ⭐ 선택지 화면에서는 대사박스 숨김
        dialogUI.dialogBox.SetActive(false);

        dialogUI.ShowConversationList(topics.ToArray(), (selectedTopic) =>
        {
            if (selectedTopic == "돌아가기")
            {
                ReturnScene();
                return;
            }

            var d = list.First(x => x.topic == selectedTopic);
            StartCoroutine(PlayDialog(d));
        });
    }

    IEnumerator PlayDialog(Dialog d)
    {
        // ⭐ 대사 시작 → 대사박스 표시
        dialogUI.dialogBox.SetActive(true);

        yield return PlayLines(defaultDialogs);
        yield return PlayLines(d.npc_script);

        if (!string.IsNullOrEmpty(d.question))
        {
            dialogUI.Hide();              // 대화창 닫기
            JSBridge.OnQuestionShown(d.id);  // 웹에 문제 표시 요청
            yield break;
        }

        dialogUI.Hide();
    }


    IEnumerator PlayLines(string[] lines)
    {
        if (lines == null || lines.Length == 0)
            yield break;

        foreach (var line in lines)
        {
            dialogUI.Conversation(line);

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        }
    }

    void ReturnScene()
    {
        if (!string.IsNullOrEmpty(returnSceneName))
        {
            dialogUI.Hide();

            // 다음 씬 저장
            MapLoadingController.nextScene = returnSceneName;

            // 로딩씬으로 이동
            SceneManager.LoadScene("MapLoadingScene");
        }
        else
        {
            dialogUI.Hide();
        }
    }
}