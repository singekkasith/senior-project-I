using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RearrangeBook : Interactable
{
    [SerializeField]
    private GameObject objBooks;
    private AudioSource audioSource;

    private float holdDuration = 5f;

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
        gameObject.layer = LayerMask.NameToLayer("Default");
        objCheckScript.BookUpdate();
    }

    public override string GetDescription() {
        return "Hold <color=green>[E]</color> Rearrage the <color=purple>books</color> back to shelf.";
        
    }

    public override void Interact() {
        //Do Change the Book to be arrange
        objBooks.SetActive(true);
        Debug.Log("Book is Arranged!");
        UpdateTask();
    }
}
