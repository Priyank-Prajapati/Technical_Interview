using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public TextMeshProUGUI gameOverText;

    private bool gameHasEnded = false;
    private int enemyCount;

    void Start()
    {
        Time.timeScale = 1;
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void EnemyDefeated()
    {
        Debug.Log(enemyCount);
        enemyCount--;
        if (enemyCount <= 0)
        {
            GameOver(true);
        }
    }
    public void GameOver(bool playerWon)
    {
        if (gameHasEnded)
            return;
        gameHasEnded = true;

        if (playerWon)
        { 
            gameOverText.text = "YOU WIN!";
        }
        else
        {
            gameOverText.text = "GAME OVER!";
        }
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void RetryGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
