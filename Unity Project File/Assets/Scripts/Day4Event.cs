using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Day4Event : MonoBehaviour
{
    private GameObject player;

    private Animator textFade;


    [SerializeField] private TextMeshProUGUI notifyText;

 
    [Range(0,3)] //0 = start event, 1 = find key event
    [SerializeField] private int currentEvent;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerCapsule");
        textFade = notifyText.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider player){
        if (player.gameObject.CompareTag("Player")) {
            switch(currentEvent){
                case 0:
                    Event1();
                    break;
                case 1:
                    Event2();
                    break;
                
            } 
        }
        
    }
    private void Event1(){
        textFade.SetTrigger("Stop");
        notifyText.SetText("Today feels off....");
        textFade.SetTrigger("Start");
        Destroy(gameObject);
    }

    private void Event2(){
        textFade.SetTrigger("Stop");
        notifyText.SetText("Hmm.. a <color=red>Strange Hatch</color>. Maybe I should go check.");
        textFade.SetTrigger("Start");
        Destroy(gameObject);
    }
}
