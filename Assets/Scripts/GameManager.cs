using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverText;

    private bool gameOver = false;

    void Start()
    {
        gameOverText.SetActive(false); // hide at start
    }

    public void TriggerGameOver()
    {
        if (gameOver)
            return;

        gameOver = true;

        gameOverText.SetActive(true);
    }

    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}