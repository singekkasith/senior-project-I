using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainThunderSF : MonoBehaviour
{
    [SerializeField] private AudioSource rainSound;

    [SerializeField] private AudioSource thunderSound;

    private int coolDown;
    // Start is called before the first frame update
    void Start()
    {
        rainSound.Play();
        thunderSound.pitch = Random.Range(1f, 1.2f);
        coolDown = Random.Range(15, 30);
        thunderSoundPlay();
    }

    // Update is called once per frame
    private void thunderSoundPlay(){
        thunderSound.Play();
        thunderSound.pitch = Random.Range(1f, 1.2f);
        coolDown = Random.Range(15, 20);
        StartCoroutine(ThunderCoolDown(coolDown));
        Debug.Log("Thunder CoolDown:" + coolDown);
    }

    IEnumerator ThunderCoolDown(int cooldown){
        yield return new WaitForSeconds(cooldown);
        thunderSoundPlay();
    }
}
