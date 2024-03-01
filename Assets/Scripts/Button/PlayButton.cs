using UnityEngine;

public class PlayButton : MonoBehaviour
{
    private Executor executor;
    private StopButton stopButton;

    private void Awake()
    {
        executor = FindObjectOfType<Executor>();
        stopButton = FindObjectOfType<StopButton>(true);
    }

    public void OnClick()
    {
        executor.Run();
        gameObject.SetActive(false);
        stopButton.gameObject.SetActive(true);
    }
}
