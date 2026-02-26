using System.Collections.Generic;
using UnityEngine;

public static class DialogStorage
{
    private static Dictionary<string, List<Dialog>> dict
        = new Dictionary<string, List<Dialog>>();

    /*
     * Priority policy:
     * - false: default loader can initialize from Resources/dialog_test.
     * - true : injected/default data already exists, so secondary loading should be skipped.
     */
    public static bool IsInitialized { get; private set; }

    /* Toggle verbose logs for storage operations. */
    private const bool VerboseLogging = true;

    public static void Store(Dialog[] dialogs)
    {
        dict.Clear();

        foreach (var d in dialogs)
        {
            if (!dict.ContainsKey(d.place))
                dict[d.place] = new List<Dialog>();

            dict[d.place].Add(d);
        }

        IsInitialized = true;

        if (VerboseLogging)
        {
            Debug.Log($"[DialogStorage] Store completed. places={dict.Keys.Count}, dialogs={dialogs.Length}");
        }
    }

    public static List<Dialog> GetByPlace(string place)
    {
        return dict.ContainsKey(place)
            ? dict[place]
            : new List<Dialog>();
    }
}
