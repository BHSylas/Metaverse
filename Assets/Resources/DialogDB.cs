using UnityEngine;

public static class DialogDB
{
    public static DialogData LoadDialog(int npcId)
    {
        TextAsset json =
            Resources.Load<TextAsset>($"dialog_{npcId}");

        if (json == null)
        {
            Debug.LogError($"Dialog JSON not found: dialog_{npcId}");
            return null;
        }

        return JsonUtility.FromJson<DialogData>(json.text);
    }
}