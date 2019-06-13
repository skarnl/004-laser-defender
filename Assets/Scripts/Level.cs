using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void LoadStartMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/0. Start Menu");
    }

    public void LoadGameScene() {
        SceneManager.LoadScene("Scenes/1. Game");
        var gameSession = FindObjectOfType<GameSession>();
        
        if (gameSession) {
            gameSession.Reset();
        }
    }

    public void LoadGameOver() {
        var gameSpeedController = FindObjectOfType<GameSpeedController>();
        gameSpeedController.SlowDownGame();

        StartCoroutine(LoadGameOverAfterDelay());
    }

    private IEnumerator LoadGameOverAfterDelay() {
        yield return new WaitForSecondsRealtime(3.2f);

        Time.timeScale = 1;
        
        SceneManager.LoadScene("Scenes/2. Game Over");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
