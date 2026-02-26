using System;
using UnityEngine;

public class DialogInjectionReceiver : MonoBehaviour
{
    /*
     * This object name is used by the Web/React side:
     * unityInstance.SendMessage("DialogInjectionReceiverObject", "InjectDialogsJson", jsonString)
     */
    public const string ReceiverObjectName = "DialogInjectionReceiverObject";
    #if UNITY_WEBGL && !UNITY_EDITOR
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void OnLoadingSceneLoadedJS();
    #endif
    /* Toggle verbose logs for JSON injection flow tracing. */
    private const bool VerboseLogging = true;

    private void Awake()
    {
        // Keep this receiver name deterministic for JS SendMessage routing.
        if (gameObject.name != ReceiverObjectName)
        {
            gameObject.name = ReceiverObjectName;
        }
        #if UNITY_WEBGL && !UNITY_EDITOR
        OnLoadingSceneLoadedJS();
        Debug.Log("LoadingScene loaded, notified Web layer.");
        #endif
    }

    public void InjectDialogsJson(string json)
    {
        if (VerboseLogging)
        {
            Debug.Log($"[DialogInjectionReceiver] InjectDialogsJson called. payloadLength={(json ?? string.Empty).Length}");
        }

        try
        {
            // Parse JSON as DialogDTO array (from React/Web format)
            DialogDTO[] dtos = DialogJsonHelper.FromJson<DialogDTO>(json);

            if (dtos == null || dtos.Length == 0)
            {
                Debug.LogWarning("[DialogInjectionReceiver] Parsed dialog DTOs are null or empty. Injection skipped.");
                return;
            }

            // Convert DTOs to internal Dialog format
            Dialog[] dialogs = new Dialog[dtos.Length];
            for (int i = 0; i < dtos.Length; i++)
            {
                dialogs[i] = dtos[i].ToDialog();
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
