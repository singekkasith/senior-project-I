using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantEvent : MonoBehaviour
{
    private GameObject player;

    private Animator textFade;

    [SerializeField] private GameObject eventObject;

    [SerializeField] private AudioSource ding;

    [SerializeField] private TextMeshProUGUI notificationText;
 



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerCapsule");
        textFade = notificationText.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider player){
        if (player.gameObject.CompareTag("Player")) {
            //textFade.SetTrigger("Stop");
            notificationText.SetText("A Fertilizer !? Maybe I can <color=purple>throw</color> it at the <color=orange>planter</color> in front of the entrace door.");
            textFade.SetTrigger("Start");
            ding.Play();
            Destroy(gameObject);
        }
        
    }

}
