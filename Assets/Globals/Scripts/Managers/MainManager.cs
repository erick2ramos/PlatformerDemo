using UnityEngine;

// Singleton class with pointers to other managers, centralizing reach
public class MainManager : MonoBehaviour
{
    static MainManager instance;
    public static MainManager Get { get { return instance; } }

    public GameManager gameManager;
    public SettingsManager settings;
    public LevelManager levelManager;
    public SpawnerFactory spawner;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
#if UNITY_ANDROID
            Screen.orientation = ScreenOrientation.Landscape;
#endif

            //loading pointers to other managers
            gameManager = GetComponent<GameManager>();
            settings = GetComponent<SettingsManager>();
            levelManager = GetComponent<LevelManager>();
            spawner = GetComponent<SpawnerFactory>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
