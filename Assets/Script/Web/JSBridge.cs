using System.Runtime.InteropServices;
using UnityEngine;

public static class JSBridge
{
    /* Toggle verbose bridge logs for build/runtime debugging. */
    private const bool VerboseLogging = true;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void OnQuestionShownJS(int id);

    [DllImport("__Internal")]
    private static extern void OnCountrySelectedJS(string country);
#endif

    public static void OnQuestionShown(int id)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        OnQuestionShownJS(id);
#endif

        if (VerboseLogging)
        {
            Debug.Log($"[JSBridge] OnQuestionShown called with id={id}");
        }
    }

    public static void NotifyCountrySelected(string country)
    {
        if (VerboseLogging)
        {
            Debug.Log($"[JSBridge] Notifying selected country to Web: {country}");
        }

#if UNITY_WEBGL && !UNITY_EDITOR
        OnCountrySelectedJS(country ?? string.Empty);
#endif
    }
}
