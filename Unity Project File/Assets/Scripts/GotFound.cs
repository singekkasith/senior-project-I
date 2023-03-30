using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotFound : MonoBehaviour
{
    [SerializeField] private AudioSource foundSound;

    [SerializeField] private AudioSource heartbeatSound;


    private void OnTriggerEnter(Collider enemy){
        if (enemy.gameObject.CompareTag("Enemy")) {
            Debug.Log("Spooky SOund");
            foundSound.Play();
            heartbeatSound.Play();
        }
        
    }

}
