using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShelf : Interactable
{
    [SerializeField] private Animator switchFlipAnim;
    [SerializeField] private Animator moveShelfAnim;

    public bool isPressed = false;
    private objCheck objCheckScript;
    private AudioSource audioSource;

    void Start(){
        objCheckScript = GameObject.Find("ObjectiveCheck").GetComponent<objCheck>();
        audioSource = GameObject.Find("MovableShelf").GetComponent<AudioSource>();
    }

    void UpdateLight() {
        isPressed = true;
        objCheckScript.ShelfSwitchUpdate();
        gameObject.layer = LayerMask.NameToLayer("Default");
        switchFlipAnim.SetTrigger("Flip");
        moveShelfAnim.SetTrigger("Start");
        audioSource.Play();
    }

    public override float GetHoldDuration() {
        return  1f;
    }

    public override string GetDescription() {
        return "Press [E] to move <color=purple>Secret Shelf</color> contraption.";
    }

    public override void Interact() {
        UpdateLight();
    }
}
