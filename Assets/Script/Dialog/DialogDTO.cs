using System;

/// <summary>
/// Data Transfer Object for dialogs received from React/Web frontend.
/// Maps to the JSON structure with camelCase fields.
/// </summary>
[Serializable]
public class DialogDTO
{
    public bool attempted;
    public int attempts;
    public int conversationId;
    public bool correct;
    public string correctAnswer;
    public string country;
    public string explanation;
    public string level;
    public bool locked;
    public int? nextConversationId;
    public string npcScript;          // Single string in DTO
    public string[] options;
    public string place;
    public string question;
    public string topic;

    /// <summary>
    /// Converts this DTO to the internal Dialog format used by DialogStorage.
    /// </summary>
    public Dialog ToDialog()
    {
        return new Dialog
        {
            id = conversationId,
            place = place ?? string.Empty,
            topic = topic ?? string.Empty,
            // Split npcScript by newline or treat as single-element array
            npc_script = string.IsNullOrEmpty(npcScript) 
                ? new string[0] 
                : npcScript.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries),
            question = question ?? string.Empty,
            next_dialog_id = nextConversationId ?? 0
        };
    }
}

