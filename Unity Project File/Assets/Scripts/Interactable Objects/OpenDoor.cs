using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Interactable
{
    [SerializeField] private GameObject transportLoc;
    
    [SerializeField]
    [Range(0,2)]
    // 0 = Normal , 1 = Objective Required, 2 = Text Door
    private int doorType;

    private GameObject player;

    private GameObject openSound;
    private AudioSource audioSource;
    private Vector3 tLoc;

    private bool isLock = false;
    private GameObject keyInHand;

    [SerializeField] private string itemRequired;


    private void Awake()
    {
        player = GameObject.Find("PlayerCapsule");
        tLoc = transportLoc.transform.position;

        DoorSound();
    }

    private void DoorSound(){

        if (doorType == 0){
            openSound = GameObject.Find("OpenSound");
        }

        else if (doorType > 0){
            openSound = GameObject.Find("LockSound");
        }
        audioSource = openSound.GetComponent<AudioSource>();
    }

    public override float GetHoldDuration() {
        return  1f;
    }

    void UpdateTask() {
        if (tLoc != null){
            player.transform.position = tLoc;
            player.SetActive(true);
            audioSource.Play();
        }
    }

    
    public override string GetDescription() {
        if (interactionType == InteractionType.Click && !isLock && doorType != 2)
            return "Press <color=green>[E]</color> to Open.";
        else if (interactionType == InteractionType.Click && isLock && doorType != 2)
            return "<color=purple>"+ itemRequired +"</color> Is Required to open the door";
        else if (doorType == 2)
            return "Door is locked by some <color=red>mysterious force<color=green>";
        else 
            return "Hold <color=green>[E]</color> to Open.";
    }

    public override void Interact() {
        switch(doorType){
                case 0:
                    Debug.Log("Door is Opened!");
                    player.SetActive(false);
                    UpdateTask();
                    break;
                case 1:
                    Debug.Log("Check for objective");
                    audioSource.Play();
                    keyInHand = GameObject.Find("PlayerCapsule/PlayerCameraRoot/Pickup Slot/" + itemRequired );
                    if (keyInHand != null){
                        isLock = false;
                        doorType = 0;
                        DoorSound();
                    }

                    else if (keyInHand == null){
                        isLock = true;
                    }
                    break;
                case 2:
                    Debug.Log("Door is Locked from the other side");
                    audioSource.Play();
                    break;
        } 
    }

    /*IEnumerator DelayTeleport(Vector3 tLoc){
        player
    }*/
}
