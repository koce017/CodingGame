using UnityEngine;

public class PlayButton : MonoBehaviour
{
    private Executor executor;
    private StopButton stopButton;

    private void Awake()
    {
        executor = FindFirstObjectByType<Executor>();
        stopButton = FindFirstObjectByType<StopButton>(FindObjectsInactive.Include);
    }

    public void OnClick()
    {
        executor.Run();
        gameObject.SetActive(false);
        stopButton.gameObject.SetActive(true);
    }
}
