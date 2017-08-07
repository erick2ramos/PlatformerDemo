using UnityEngine;
using UnityEngine.UI;

// Controls HUD view
public class HUDController : MonoBehaviour
{
    public Slider healthSlider;
    public Text scoreText;

    public void UpdateHealth(float amount)
    {
        healthSlider.value = amount;
    }

    public void UpdateScore(int amount)
    {
        scoreText.text = "" + amount;
    }
}
