using UnityEngine;

public class DialogLoader : MonoBehaviour
{
    /* Toggle verbose logs for default-dialog loading decisions. */
    private const bool VerboseLogging = true;

    void Start()
    {
        if (DialogStorage.IsInitialized)
        {
            if (VerboseLogging)
            {
                Debug.Log("[DialogLoader] Default load skipped because DialogStorage is already initialized.");
            }
            return;
        }

        TextAsset json = Resources.Load<TextAsset>("dialog_test");
        Dialog[] dialogs = DialogJsonHelper.FromJson<Dialog>(json.text);

        DialogStorage.Store(dialogs);

        if (VerboseLogging)
        {
            Debug.Log("[DialogLoader] Default dialog_test data loaded.");
        }
    }
}
