public static class JSBridge
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void OnQuestionShownJS(int id);
#endif

    public static void OnQuestionShown(int id)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        OnQuestionShownJS(id);
#endif
    }
}