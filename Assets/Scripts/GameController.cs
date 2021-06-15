using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverScreen;

    public void Restart()
    {

        SceneManager.LoadScene("Ghost game-10");
        gameOverScreen.SetActive(false);
        Cursor.visible = false;

        ScoreManager.instance.ResetScore();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
