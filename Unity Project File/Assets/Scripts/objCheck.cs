using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class objCheck : MonoBehaviour
{
    public int bookLeft;
    public int plantDone;

    public int cubeDone;

    public int puddleLeft;

    public int tomeCollected = 0;

    public int objectiveDone = 0;

    public GameObject theThirdTome;

    public GameObject goToSleepText;

    public bool isObjectiveDone = false;

    public bool isTomeCollected = false;

    public TextMeshProUGUI bookCounterText;

    public TextMeshProUGUI treeProgressionText;

    public TextMeshProUGUI floorCounterText;

    public TextMeshProUGUI extraObjectiveText;

    public TextMeshProUGUI tomeObjectiveText;

    //private objCheck objCheckScript;
    //private ObjectiveTrigger objTreeScript;

    private Objectives objectiveScript;
    private CountDownTimer countDownTimerScript;

    [SerializeField] private int currentDay = 1;

    [SerializeField]
    private AudioSource completeSource;

    // Start is called before the first frame update
    void Start()
    {
        objectiveScript = GameObject.Find("Game Manager").GetComponent<Objectives>();
        countDownTimerScript = GameObject.Find("Canvas").GetComponent<CountDownTimer>();

        
        ObjectiveDifficulty();
        if (currentDay == 1){
            treeProgressionText.SetText("Tree Progression: " + "<color=orange>" + plantDone + "</color>" + " / 4");
        }
        else if (currentDay == 2){
            extraObjectiveText.SetText("Find the <color=orange>Strange Switch</color>:");
        }
    }

    public void ObjectiveDifficulty(){
        bookLeft = FindObjectsOfType<RearrangeBook>().Length;
        puddleLeft = FindObjectsOfType<CleanFloor>().Length;
         

        bookCounterText.SetText("Arrange Book: " + "<color=orange>" + bookLeft + "</color>" + " Left");
        floorCounterText.SetText("Clean Dirty Floor: " + "<color=orange>" + puddleLeft + "</color>" + " Left");
    }

    // Update is called once per frame
    public void BookUpdate()
    {
        bookLeft -= 1;
        bookCounterText.SetText("Arrange Book: " + "<color=orange>" + bookLeft + "</color>" + " Left");
        if (bookLeft <= 0) {
            bookCounterText.SetText("Arrange Book: <color=green>Complete!</color>");
            Debug.Log("Arrange Book: Complete!");
            objectiveDone += 1;
            ObjectiveCheck();
        } 
    }

    public void TreeUpdate()
    {
        plantDone += 1;
        treeProgressionText.SetText("Tree Progression: " + "<color=orange>" + plantDone + "</color>" + " / 4");
        if (plantDone >= 4) {
            treeProgressionText.SetText("Tree Progression: <color=green>Complete!</color>");
            objectiveDone += 1;
            ObjectiveCheck();
        } 
    }

    public void CubeUpdate()
    {
        cubeDone += 1;
        extraObjectiveText.SetText("Cube Structure: " + "<color=orange>" + cubeDone + "</color>" + " / 4");
        if (cubeDone >= 4 && theThirdTome != null) {
            extraObjectiveText.SetText("Cube Structure: <color=green>Complete!</color>");
            ObjectiveCheck();
            theThirdTome.SetActive(true);
        } 
    }

    public void CubeStart(){
        extraObjectiveText.SetText("Cube Structure: " + "<color=orange>" + cubeDone + "</color>" + " / 4");
    }

    public void PuddleUpdate()
    {
        puddleLeft -= 1;
        floorCounterText.SetText("Clean Dirty Floor: " + "<color=orange>" + puddleLeft + "</color>" + " Left");
        if (puddleLeft <= 0) {
            floorCounterText.SetText("Clean Dirty Floor: <color=green>Complete!</color>");
            Debug.Log("Clean Dirty Floor: Complete!");
            objectiveDone += 1;
            ObjectiveCheck();
        } 
    }

    public void ObjectiveCheck(){
        completeSource.Play();
        if ( objectiveDone >= 3 ){
            isObjectiveDone = true;
            countDownTimerScript.CountDownStop();
            goToSleepText.SetActive(true);
            //objectiveScript.currentDay += 1;
            //objectiveScript.DayCheck();
        }
    }

    public void TomeUpdate(){
        tomeCollected += 1;
        objectiveDone += 1;
        isTomeCollected = true;
        ObjectiveCheck();
        tomeObjectiveText.SetText("You Retrieved the <color=green>Forbidden Tome</color>!");
        
    }

    public void ShelfSwitchUpdate(){
        objectiveDone += 1;
        ObjectiveCheck();
        extraObjectiveText.SetText("Strange Switch: <color=green>Found!</color>");
    }
}
