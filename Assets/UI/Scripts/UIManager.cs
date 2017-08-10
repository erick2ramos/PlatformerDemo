using UnityEngine;
using UnityEngine.UI;

// UI Facade singleton for opening menus and dialog boxes
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Get { get { return instance; } }

    // Pointers to ui elements
    Animator animator;
    public GameObject mainMenu;
    public GameObject retryMenu;
    public GameObject goalMenu;
    public Text scoreText;
    public HUDController hudObject;
    public GameObject touchInputUI;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            animator = GetComponent<Animator>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Hide()
    {
        animator.SetBool("ShowMainMenu", false);
        animator.SetBool("ShowRetryMenu", false);
        animator.SetBool("ShowGoalMenu", false);
        animator.SetBool("ShowHud", false);
    }

    public void ShowHUD()
    {
        Hide();
        animator.SetBool("ShowHud", true);
#if UNITY_STANDALONE_WIN
        touchInputUI.SetActive(false);
#endif
#if UNITY_ANDROID
        if (MainManager.Get.settings.gameMode == GameMode.Runner)
        {
            touchInputUI.SetActive(false);
        } else
        {
            touchInputUI.SetActive(true);
        }
#endif
    }

    public void ShowMainMenu()
    {
        Hide();
        animator.SetBool("ShowMainMenu", true);
    }

    public void ShowRetryMenu()
    {
        Hide();
        animator.SetBool("ShowRetryMenu", true);

    }

    public void ShowGoalMenu(int finalScore)
    {
        Hide();
        scoreText.text = "" + finalScore;
        animator.SetBool("ShowGoalMenu", true);
    }
}
