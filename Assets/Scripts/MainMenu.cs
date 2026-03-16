using UnityEngine;
using System.Collections;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject gameUI;

    public TextMeshProUGUI countdownText;

    void Start()
    {
        Time.timeScale = 0f; // Freeze game at start
    }

    public void PlayGame()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(true);

        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);

        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);

        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);

        countdownText.text = "GO";
        yield return new WaitForSecondsRealtime(0.5f);

        countdownText.gameObject.SetActive(false);

        Time.timeScale = 1f; // Start the game
    }

    public void QuitGame()

    {
        Application.Quit();
    }
}