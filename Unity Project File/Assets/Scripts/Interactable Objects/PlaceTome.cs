using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceTome : Interactable
{
    public bool isPlace;

    private EndingManager endingScript;

    [SerializeField] private GameObject tome;

    [SerializeField] private AudioSource ding;

    private void Start() {
        isPlace = false;
        endingScript = GameObject.Find("Ending Manager").GetComponent<EndingManager>();
    }

    void UpdateSwitch() {
        gameObject.layer = LayerMask.NameToLayer("Default");
        tome.SetActive(true);
        ding.Play();
        endingScript.EndingUpdate();
    }

    public override float GetHoldDuration() {
        return  1f;
    }

    public override string GetDescription() {
        if (!isPlace) return "Press <color=green>[E]</color> to Place the <color=red>Forbidden Tome</color>.";
        return "";
    }

    public override void Interact() {
        if (!isPlace) {
            isPlace = true;
            UpdateSwitch();
        }
    }

    
}
