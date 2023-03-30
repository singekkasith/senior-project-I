using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushStructure : Interactable
{
    [SerializeField]
    private GameObject objHatchLock;
    [SerializeField]
    private float holdDuration = 2.5f;
    private Animator structureAnim;
    private GameObject pushSound;
    private AudioSource audioSource;
    public bool isPush;

    private void Start(){
        structureAnim = gameObject.GetComponent<Animator>();
        pushSound = GameObject.Find("MovingSound");
        audioSource = pushSound.GetComponent<AudioSource>();
    }

    void UpdateTask() {
        isPush = true;
        audioSource.Play();
        gameObject.layer = LayerMask.NameToLayer("Default");
        Destroy(gameObject, 2f);
    }

    public override float GetHoldDuration() {
        return  holdDuration;
    }

    public override string GetDescription() {
        return "Hold <color=green>[E]</color> Push the Structure.";
    }

    public override void Interact() {
        if (!isPush){
            Destroy(objHatchLock, 2f);
            Debug.Log("Hatch is Opened!");
            structureAnim.SetBool("isPush", true);
            UpdateTask();
        }

        //else
        //Pop up message about need some item
    }
}
