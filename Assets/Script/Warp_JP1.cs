using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    public DialogUI dialogUI;
    public string nextSceneName;

    // ⭐ 포탈이 설정할 place
    public string place;

    public KeyCode interactKey = KeyCode.E;

    [TextArea]
    public string dialogText = "이동하시겠습니까?";

    bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            OpenDialog();
        }
    }

    void OpenDialog()
    {
        string[] options =
        {
            "이동한다",
            "더 둘러본다"
        };

        dialogUI.dialogText.text = dialogText;

        dialogUI.ShowConversationList(options, (selected) =>
        {
            if (selected == "이동한다")
            {
                dialogUI.Hide();

                // ⭐ place 설정
                MapState.CurrentPlace = place;

                // ⭐ 다음 씬 저장
                MapLoadingController.nextScene = nextSceneName;

                // ⭐ 로딩씬 이동
                SceneManager.LoadScene("MapLoadingScene");
            }
            else
            {
                dialogUI.Hide();
            }
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = false;
    }
}