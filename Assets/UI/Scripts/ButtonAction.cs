using UnityEngine;

public class ButtonAction : MonoBehaviour {
    public void Play()
    {
        // Call Game Manager to setup everything for a new game
    }

    public void Retry()
    {
        // Call Game Manager to restart the level
    }

    public void Quit()
    {
        Application.Quit();
    }
}
