using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject cameraPrefab;

    [HideInInspector]
    public PlayerController player;
    [HideInInspector]
    public CameraFollow cameraController;

    private void Start()
    {
        GameObject go = Instantiate(playerPrefab);
        player = go.GetComponent<PlayerController>();
        go = Instantiate(cameraPrefab);
        cameraController = go.GetComponent<CameraFollow>();
        UIManager.Get.ShowMainMenu();
        Init();
    }

    public void Init()
    {
        // Player
        player.Init();
        // Camera
        cameraController.Init();
        cameraController.ChangeState(CameraFollow.CameraState.MainMenu);
        // Spawner
        MainManager.Get.spawner.Init();
        // Level
        MainManager.Get.levelManager.Init();
        MainManager.Get.levelManager.GenerateLevel();
        // UI
        UIManager.Get.hudObject.UpdateHealth(player.currentHitpoints / (float)player.maxHitpoints);
        UIManager.Get.hudObject.UpdateScore(0);
    }

    public void Play()
    {
        InputManager.Init();

        // Start player 
        player.Init();
        // Camera
        cameraController.ChangeState(CameraFollow.CameraState.Transition);
        // UI
        UIManager.Get.hudObject.UpdateHealth(player.currentHitpoints / (float)player.maxHitpoints);
        UIManager.Get.hudObject.UpdateScore(0);
        UIManager.Get.ShowHUD();
        
        // Activate enemies
        // Control to player
        Invoke("GivePlayerControl", 1.25f);
    }

    void GivePlayerControl()
    {
        player.machine.ChangeState("PlayerWalkState");
        MainManager.Get.spawner.ActivateSpawnedEnemies();
    }

    public void Win()
    {
        // Remove control from player
        player.machine.ChangeState("PlayerIdleState");
        player.inmunity = true;
        player.hit = false;

        // Disable AI and bullets
        MainManager.Get.spawner.DeactivateSpawnedEnemies();

        // UI score
        // show UI
        UIManager.Get.ShowGoalMenu(player.collectablesAmount * player.currentHitpoints);
    }

    public void Retry()
    {
        UIManager.Get.Hide();
        // Unload Level
        MainManager.Get.levelManager.UnloadLevel();
        MainManager.Get.spawner.Clear();

        // Create new level
        MainManager.Get.levelManager.GenerateLevel();

        // Default positions
        Vector3 playerStart = playerPrefab.transform.position;
        Vector3 cameraStart = cameraPrefab.transform.position;
        // Reset Player
        player.transform.position = playerStart;

        // Reset Camera
        cameraController.Init();
        cameraController.MoveCameraTo(cameraStart);

        UIManager.Get.ShowMainMenu();
    }

    public void GameOver()
    {
        // Remove control from player
        player.machine.ChangeState("PlayerIdleState");
        player.inmunity = true;
        player.hit = false;

        // Disable AI and bullets
        MainManager.Get.spawner.DeactivateSpawnedEnemies();

        // UI
        UIManager.Get.ShowRetryMenu();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
