using UnityEngine;

public class ButtonAction : MonoBehaviour {
    public void Play()
    {
        // Call Game Manager to setup everything for a new game
        MainManager.Get.gameManager.Play();
    }

    public void Retry()
    {
        // Call Game Manager to restart the level
        MainManager.Get.gameManager.Retry();
    }

    public void Quit()
    {
        MainManager.Get.gameManager.Quit();
    }
}
