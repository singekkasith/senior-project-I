using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySFX : MonoBehaviour
{
    private EnemyAI enemyScript;
    private AudioSource audioSource;

    
    //[SerializeField] private AudioClip attackingSFX;

    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponent<EnemyAI>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyScript.agent.velocity.magnitude > 1f && !audioSource.isPlaying && Time.timeScale != 0f)
        {
            audioSource.volume = Random.Range(0.9f, 1f);
            audioSource.pitch = Random.Range(0.6f, 0.7f);
            audioSource.Play();
        }
        if (Time.timeScale == 0f){
            audioSource.Stop();
        }

    }
}
