using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Day3Event : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private AudioSource ding;

    [SerializeField] GameObject cubeTrigger;

    private Animator textFade;

    private objCheck objCheckScript;


    [SerializeField] private TextMeshProUGUI notifyText;

 
    [Range(0,3)] //0 = start event, 1 = find key event
    [SerializeField] private int currentEvent;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerCapsule");
        objCheckScript = GameObject.Find("ObjectiveCheck").GetComponent<objCheck>();
        textFade = notifyText.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider player){
        if (player.gameObject.CompareTag("Player")) {
            switch(currentEvent){
                case 0:
                    BarrelEvent();
                    break;
                case 1:
                    StructureEvent();
                    break;
                case 2:
                    TomeEvent();
                    break;
                
            } 
        }
        
    }

    private void BarrelEvent(){
        notifyText.SetText("That <color=yellow>Barrel</color>! I could use it to <color=purple>throw</color> at the <color=red>statue</color>, if it come back again.");
        textFade.SetTrigger("Start");
        ding.Play();
        Destroy(gameObject);
    }

    private void TomeEvent(){
        textFade.SetTrigger("Stop");
        notifyText.SetText("The <color=purple>Padestal</color>! It is empty... Maybe some <color=yellow>Contraption</color> could return the <color=red>Forbidden Tome</color> back.");
        textFade.SetTrigger("Start");
        ding.Play();
        Destroy(gameObject);
    }

    private void StructureEvent(){
        textFade.SetTrigger("Stop");
        notifyText.SetText("That <color=blue>Structure</color> looks like some kind of <color=yellow>Contraption</color>! I could find something to <color=purple>throw</color> at it.");
        textFade.SetTrigger("Start");
        objCheckScript.CubeStart();
        ding.Play();
        Destroy(cubeTrigger);
    }
    
}
