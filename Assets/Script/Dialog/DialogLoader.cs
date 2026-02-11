using UnityEngine;

public class DialogLoader : MonoBehaviour
{
    void Start()
    {
        TextAsset json = Resources.Load<TextAsset>("dialog_test");
        Dialog[] dialogs = DialogJsonHelper.FromJson<Dialog>(json.text);

        DialogStorage.Store(dialogs);

        Debug.Log("Dialog 테스트 데이터 로드 완료");
    }
}