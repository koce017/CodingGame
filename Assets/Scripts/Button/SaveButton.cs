using UnityEngine;

public class SaveButton : MonoBehaviour
{
    public PigeonCodeEditor codeEditor;

    public void OnClick()
    {
        var inGameCodeEditor = codeEditor.GetComponent<InGameCodeEditor.CodeEditor>();
        codeEditor.Code = inGameCodeEditor.Text;
        codeEditor.gameObject.SetActive(false);
    }
}
