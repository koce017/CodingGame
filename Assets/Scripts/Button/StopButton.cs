using UnityEngine;

public class StopButton : MonoBehaviour
{
    private Executor executor;
    private PlayButton playButton;

    private void Awake()
    {
        executor = FindFirstObjectByType<Executor>();
        playButton = FindFirstObjectByType<PlayButton>(FindObjectsInactive.Include);
    }

    public void OnClick()
    {
        executor.Stop();
        gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
    }
}
