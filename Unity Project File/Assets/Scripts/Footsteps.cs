using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private CharacterController playerScript;

    [SerializeField]
    private GameObject footstepsSounds;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<CharacterController>();
        audioSource = footstepsSounds.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerScript.isGrounded && playerScript.velocity.magnitude >= 4f && !audioSource.isPlaying)
        {
            audioSource.volume = Random.Range(0.1f, 0.2f);
            audioSource.pitch = Random.Range(1.3f, 1.5f);
            audioSource.Play(); 
        }

        if (playerScript.isGrounded && playerScript.velocity.magnitude >= 3f && playerScript.velocity.magnitude < 4f && !audioSource.isPlaying)
        {
            audioSource.volume = Random.Range(0.1f, 0.2f);
            audioSource.pitch = Random.Range(1f, 1.1f);
            audioSource.Play(); 
        }

        if (playerScript.velocity.magnitude == 0f){
            audioSource.Stop(); 
        }

    }
}
