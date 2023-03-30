using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public int tomePlaced = 0;

    [SerializeField] private Animator transition;

    

    public void EndingUpdate()
    {
        tomePlaced += 1;
        if (tomePlaced == 4) {
            Debug.Log("Ending Scene");
            LoadNextDay();
        } 
    }

    public void LoadNextDay(){
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadScene(int levelIndex){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
    }
}
