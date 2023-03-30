using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanFloor : Interactable
{
    private float holdDuration = 6f;

    private string audioName;

    private objCheck objCheckScript;

    //public bool isArrange;

    void Start() {
        objCheckScript = GameObject.Find("ObjectiveCheck").GetComponent<objCheck>();
    }
    public override float GetHoldDuration() {
        return  holdDuration;
    }

    void UpdateTask() {
        Destroy(gameObject);
        objCheckScript.PuddleUpdate();
    }

    public override string GetDescription() {
        return "Hold <color=green>[E]</color> Clean the Floor.";
    }

    public override void Interact() {
        Debug.Log("Floor is Cleaned!");
        UpdateTask();
    }
}
