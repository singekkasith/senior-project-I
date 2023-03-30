using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public enum InteractionType {
        Click,
        Hold,
    }

    float holdTime;
    
    public InteractionType interactionType;

    public abstract string GetDescription();
    public abstract void Interact();

    public void IncreaseHoldTime() => holdTime += Time.deltaTime; 

    public abstract float GetHoldDuration();
    
    public void PlaySound() => FindObjectOfType<AudioManager>().Play("Arranging");

    public void StopSound() => FindObjectOfType<AudioManager>().Stop("Arranging");
    public void ResetHoldTime() => holdTime = 0f;

    public float GetHoldTime() => holdTime;
}
