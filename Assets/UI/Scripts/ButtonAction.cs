using UnityEngine;

public class ButtonAction : MonoBehaviour {
    public void Play(bool platform)
    {
        // Call Game Manager to setup everything for a new game
        if (platform)
        {
            MainManager.Get.settings.gameMode = GameMode.Platform;
        }
        else
        {
            MainManager.Get.settings.gameMode = GameMode.Runner;
        }
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
