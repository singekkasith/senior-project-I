using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerOffEvent : MonoBehaviour
{
    private GameObject player;

    private Animator textFade;

    [SerializeField] private AudioSource ding;

    [SerializeField] private GameObject electricKeyTextGO;

    [SerializeField] private TextMeshProUGUI electricKeyText;
    [SerializeField] private GameObject electricSwitch;
    private LightSwitch lightSwitchScript;

    [SerializeField] private TextMeshProUGUI powerOffWarningText;

    [SerializeField] private GameObject switchText;

    [SerializeField] private GameObject hiddenSwitch;
 
    [Range(0,4)] //0 = start event, 1 = find key event
    [SerializeField] private int currentEvent;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerCapsule");
        lightSwitchScript = electricSwitch.GetComponent<LightSwitch>();
        textFade = powerOffWarningText.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider player){
        if (player.gameObject.CompareTag("Player")) {
            switch(currentEvent){
                case 0:
                    ElecEvent1();
                    break;
                case 1:
                    ElecEvent2();
                    break;
                case 2:
                    ElecEvent3();
                    break;
                case 3:
                    TomeEvent();
                    break;
                case 4:
                    KeyEvent();
                    break;
            } 
        }
        
    }

    private void ElecEvent1(){
        ding.Play();
        electricKeyTextGO.SetActive(true);
        lightSwitchScript.isDay2 = true;
        electricSwitch.SetActive(true);
        powerOffWarningText.SetText("<color=red>Something</color> is coming for me! I need to turn <color=orange>power switch</color> back on in the <color=purple>electric room</color>.");
        textFade.SetTrigger("Start");
        Destroy(gameObject);
    }
    private void ElecEvent2(){
        textFade.SetTrigger("Stop");
        powerOffWarningText.SetText("The door is locked! I must find the <color=purple>Electric Room Key</color>.");
        textFade.SetTrigger("Start");
        ding.Play();
        Destroy(gameObject);
    }

    private void ElecEvent3(){
        textFade.SetTrigger("Stop");
        powerOffWarningText.SetText("that <color=red>thing</color> is gone... I have to complete the tasks quickly before that happen again.");
        textFade.SetTrigger("Start");
        ding.Play();
        Destroy(gameObject);
    }

    private void TomeEvent(){
        textFade.SetTrigger("Stop");
        powerOffWarningText.SetText("That bookshelf looks moveable... Perhaps I could find some <color=yellow>switch</color> that move it up.");
        switchText.SetActive(true);
        hiddenSwitch.SetActive(true);
        textFade.SetTrigger("Start");
        ding.Play();
        Destroy(gameObject, 1f);
        
    }

    private void KeyEvent(){
        electricKeyText.SetText("Find the <color=purple>Electric Room Key</color>: <color=green>Complete!</color>");
        ding.Play();
        Destroy(gameObject, 1f);
    }
}
