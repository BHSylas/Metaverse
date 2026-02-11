using System;

[Serializable]
public class Dialog
{
    public int id;
    public string place;         
    public string topic;          
    public string[] npc_script;   
    public string question;     
    public int next_dialog_id;   
}