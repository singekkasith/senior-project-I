using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sleep : Interactable
{
    [SerializeField] private Animator transition;

    [SerializeField] private float transitionTime = 1f;

    [SerializeField] private bool isDay4 = false;


    private objCheck objCheckScript;

    void Start(){
        if (!isDay4){
            objCheckScript = GameObject.Find("ObjectiveCheck").GetComponent<objCheck>();
        }
        
    }

    void UpdateTask() {
        LoadNextDay();
    }
    public override float GetHoldDuration() {
        return  1f;
    }

    public override string GetDescription() {
        if (!isDay4){
            if (objCheckScript.isObjectiveDone && objCheckScript.isTomeCollected)
                return "Press <color=green>[E]</color> to Sleep.";
            else if (objCheckScript.isObjectiveDone && !objCheckScript.isTomeCollected)
                return "I need to find the <color=red>Forbidden Tome</color>!";
            else 
                return "There's no time for rest, I should finish all the <color=red>Tasks</color> first.";
        
        }
        else {
            return "Press <color=green>[E]</color> to Enter the <color=red>Strange Hatch</color>.";
        }
            
    }

    public override void Interact() {
        if (!isDay4){
            if (objCheckScript.isObjectiveDone && objCheckScript.isTomeCollected){
            UpdateTask();
            }
        }
        else {
            UpdateTask();
        }
        
    }

    public void LoadNextDay(){
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadScene(int levelIndex){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
