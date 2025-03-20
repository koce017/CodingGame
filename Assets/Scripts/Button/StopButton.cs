using UnityEngine;

public class StopButton : MonoBehaviour
{
    private Executor executor;
    private PlayButton playButton;

    private void Awake()
    {
        executor = FindObjectOfType<Executor>();
        playButton = FindObjectOfType<PlayButton>(true);
    }

    public void OnClick()
    {
        executor.Stop();
        gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
    }
}
