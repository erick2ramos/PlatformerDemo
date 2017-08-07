using UnityEngine;
using UnityEngine.UI;

// UI Facade singleton for opening menus and dialog boxes
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public UIManager Get { get { return instance; } }

    // Pointers to ui elements
    Animator animator;
    public GameObject mainMenu;
    public GameObject retryMenu;
    public GameObject goalMenu;
    public Text scoreText;
    public HUDController hudObject;

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
        animator.SetBool("ShowGoalMenu", true);

    }
}
