using UnityEngine;


[CreateAssetMenu(fileName = "Dialogue_New", menuName = "Create new Dialogue Data")]
public class DialogueSO : ScriptableObject
{
    public string nameSpeaker;
    [TextArea]
    public string dialogue;
    public string nameAudioClipData;

    [Space]
    public DialogueSO nextDialogue;
}
