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
        //Dialogue Injection은 DialogInjectionReceiver의 initialize가 완료된 이후 실행되어야 하기 때문에, Web layer에서 이 시점에 주입을 시도하도록 안내하는 것이 안전합니다.
        // LoadingScene에서 일방적으로 OnLoadingSceneLoadedJS를 호출할 경우 DialogInjectionReceiver가 아직 initialize되지 않은 시점에 Web layer에서 주입을 시도할 수 있기 때문에,
        // DialogInjectionReceiver의 Awake에서 호출하도록 변경하였습니다.
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
