using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    public GateCounter gateCounterUI;
    public Image gameOverScreen;
    public Text gameOverText;
    public Image timerImage;

    public void Start()
    {
        gameOverScreen.gameObject.SetActive(false);
        timerImage.fillAmount = 1;
    }

    public void UpdateTimer(float value, int maxValue)
    {
        timerImage.fillAmount = Mathf.InverseLerp(0, maxValue, value);
    }

    public void UpdateGateCounter(int value, int maxGatesCount)
    {
        gateCounterUI.UpdateUI(value, maxGatesCount);
    }

    public void ShowGameOverScreenPassed()
    {
        gameOverScreen.gameObject.SetActive(true);
        gameOverText.text = "Passed";
        gameOverText.color = Color.green;
    }

    public void ShowGameOverScreenDeath()
    {
        gameOverScreen.gameObject.SetActive(true);
        gameOverText.text = "YOU DIED";
        gameOverText.color = Color.red;
    }

    public void ShowGameOverScreenFail()
    {
        gameOverScreen.gameObject.SetActive(true);
        gameOverText.text = "FAILED";
        gameOverText.color = Color.red;
    }

    public void OnRestartClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
