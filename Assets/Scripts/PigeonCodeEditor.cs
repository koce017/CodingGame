using UnityEngine;
using UnityEngine.SceneManagement;

public class PigeonCodeEditor : MonoBehaviour
{
    private FreeCam freeCam;
    private PlayButton playButton;
    private InGameCodeEditor.CodeEditor inGameCodeEditor;
    
    public string Code { get; set; }

    void Awake()
    {
        playButton = FindFirstObjectByType<PlayButton>();
        freeCam = Camera.main.GetComponent<FreeCam>();
        inGameCodeEditor = GetComponent<InGameCodeEditor.CodeEditor>();
        Code = Resources.Load<TextAsset>($"Codes/{SceneManager.GetActiveScene().name}").text;
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        freeCam.enabled = false;
        playButton.gameObject.SetActive(false);
        inGameCodeEditor.Text = Code;
        inGameCodeEditor.InputField.Select();
    }

    void OnDisable()
    {
        freeCam.enabled = true;
        playButton.gameObject.SetActive(true);
    }
}
