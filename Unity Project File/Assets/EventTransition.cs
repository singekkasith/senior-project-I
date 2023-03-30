using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventTransition : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1f;
        CursorUnlock();
    }

    public void Restart()
    {
       SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        CursorLock();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void CursorUnlock()
    {
       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
    }

    private void CursorLock()
    {
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
    }
}
