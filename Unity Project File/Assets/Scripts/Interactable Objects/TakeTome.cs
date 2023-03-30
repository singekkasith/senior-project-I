using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeTome : Interactable
{
    private float holdDuration = 1f;

    private string audioName;

    public bool isLastDay = false;

    private bool isTaken = false;

    private objCheck objCheckScript;

    public GameObject door;

    [SerializeField] private GameObject tome;

    void Start() {
        if (!isLastDay){
            objCheckScript = GameObject.Find("ObjectiveCheck").GetComponent<objCheck>();
        }
    }
    public override float GetHoldDuration() {
        return  holdDuration;
    }

    void UpdateTask() {
        if (isLastDay){
            Destroy(tome);
            door.SetActive(true);
        }
        else {
            Destroy(tome);
            objCheckScript.TomeUpdate();
        }
    }

    public override string GetDescription() {
        if (!isTaken) return "Press <color=green>[E]</color> Take the <color=red>Forbidden Tome</color>.";
        return "";
    }

    public override void Interact() {
        isTaken = true;
        UpdateTask();
    }
}
