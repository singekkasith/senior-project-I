using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternOil : Interactable
{
    [SerializeField] private bool isRespawnable = false;
    [SerializeField] private float respawnTime;

    [SerializeField]
    private AudioSource pickUpSource;


    public float litAmount = 100f;

    void UpdateSwitch() {
        pickUpSource.Play();
        GameObject.Find("LanternBar").GetComponent<LanternBar>().AddFuel(litAmount);
        Destroy(gameObject);
    }

    public override float GetHoldDuration() {
        return  3f;
    }

    public override string GetDescription() {

        return "Hold <color=green>[E]</color> to <color=yellow>Completely Refill</color> the Lantern Fuel.";
    }

    public override void Interact() {
        UpdateSwitch();
    }
}
