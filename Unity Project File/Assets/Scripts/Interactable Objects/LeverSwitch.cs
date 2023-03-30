using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSwitch : Interactable
{
    public bool isOn;

    private MazeGateManager mazeScript;

    [SerializeField] private Animator switchFlipAnim;

    private void Start() {
        isOn = false;
        mazeScript = GameObject.Find("GateManager").GetComponent<MazeGateManager>();
    }

    void UpdateSwitch() {
        switchFlipAnim.SetTrigger("Flip");
        gameObject.layer = LayerMask.NameToLayer("Default");
        mazeScript.MazeUpdate();
    }

    public override float GetHoldDuration() {
        return  1f;
    }

    public override string GetDescription() {
        if (!isOn) return "Press <color=green>[E]</color> to switch the lever.";
        return "The switch is active";
    }

    public override void Interact() {
        if (!isOn) {
            isOn = true;
            UpdateSwitch();
        }
    }
}
