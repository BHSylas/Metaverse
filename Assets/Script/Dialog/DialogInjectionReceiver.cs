using System;
using UnityEngine;

public class DialogInjectionReceiver : MonoBehaviour
{
    /*
     * This object name is used by the Web/React side:
     * unityInstance.SendMessage("DialogInjectionReceiverObject", "InjectDialogsJson", jsonString)
     */
    public const string ReceiverObjectName = "DialogInjectionReceiverObject";

    /* Toggle verbose logs for JSON injection flow tracing. */
    private const bool VerboseLogging = true;

    private void Awake()
    {
        // Keep this receiver name deterministic for JS SendMessage routing.
        if (gameObject.name != ReceiverObjectName)
        {
            gameObject.name = ReceiverObjectName;
        }
    }

    public void InjectDialogsJson(string json)
    {
        if (VerboseLogging)
        {
            Debug.Log($"[DialogInjectionReceiver] InjectDialogsJson called. payloadLength={(json ?? string.Empty).Length}");
        }

        try
        {
            Dialog[] dialogs = DialogJsonHelper.FromJson<Dialog>(json);

            if (dialogs == null)
            {
                Debug.LogWarning("[DialogInjectionReceiver] Parsed dialogs are null. Injection skipped.");
                return;
            }

            DialogStorage.Store(dialogs);

            if (VerboseLogging)
            {
                Debug.Log($"[DialogInjectionReceiver] Dialogs stored successfully. count={dialogs.Length}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[DialogInjectionReceiver] Failed to parse or store dialogs JSON. {ex.Message}\n{ex}");
        }
    }
}
