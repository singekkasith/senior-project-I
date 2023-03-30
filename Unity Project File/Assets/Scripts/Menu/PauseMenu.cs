using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject gameOverUI;

    private GameObject player;

    private GameObject otherUI;
    //private PlayerMovementFPS playerScript;

    void Start()
    {
        Time.timeScale = 1f;
        player = GameObject.Find("PlayerCapsule");
        otherUI = GameObject.Find("InGameUI");
        ResumeGame();
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape) && !playerScript.isGameOver)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        // if (playerScript.isGameOver){
        //     GameOver();
        // }
    }
    void PauseGame()
    {
       CursorUnlock();
       pauseMenuUI.SetActive(true);
       Time.timeScale = 0f;
       GameIsPaused = true;
       
    }

    public void ResumeGame()
    {
       CursorLock();
       pauseMenuUI.SetActive(false);
       Time.timeScale = 1f;
       GameIsPaused = false;
    }

    public void Restart()
    {
       CursorLock();
       Time.timeScale = 1f;
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void LoadMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Loading Menu...");
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    private void CursorLock()
    {
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
       player.SetActive(true);
       otherUI.SetActive(true);
    }

    private void CursorUnlock()
    {
       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
       player.SetActive(false);
       otherUI.SetActive(false);
    }

    public void GameOver()
    {
        CursorUnlock();
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("GameOver!");
    }
}
