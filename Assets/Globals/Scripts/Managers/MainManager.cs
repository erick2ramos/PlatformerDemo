using UnityEngine;

// Singleton class with pointers to other managers, centralizing reach
public class MainManager : MonoBehaviour
{
    static MainManager instance;
    public static MainManager Get { get { return instance; } }

    public GameManager gameManager;
    public SettingsManager settings;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            //loading pointers to other managers
            gameManager = GetComponent<GameManager>();
            settings = GetComponent<SettingsManager>();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
