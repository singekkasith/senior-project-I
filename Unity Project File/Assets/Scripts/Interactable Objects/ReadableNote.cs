using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableNote : Interactable
{
    [SerializeField]
    private GameObject noteUI;

    void UpdateNote() {
        noteUI.SetActive(true);
    }
    public override float GetHoldDuration() {
        return  1f;
    }

    public override string GetDescription() {
        return "Press <color=green>[E]</color> to <color=green>READ</color> the note.";
    }

    public override void Interact() {
        UpdateNote();
    }
}
