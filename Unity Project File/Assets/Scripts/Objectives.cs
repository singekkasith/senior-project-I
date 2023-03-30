using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Objectives : MonoBehaviour
{
    public int currentDay;
    [SerializeField] private GameObject[] cleanObjective;
    [SerializeField] private GameObject[] bookObjective;

    [SerializeField] private GameObject[] elecKeyObjective;

    [SerializeField] private GameObject[] knightSpawnPosition;

    private GameObject cleanSelect;
    private GameObject bookSelect;
    private GameObject elecKeySelect;

    private GameObject spawnSelect;

    [SerializeField] private GameObject knightStatueAI;

    private GameObject spawnAI;
    

    [SerializeField] private GameObject electricShutDown;

    
    //private GameObject growSelect;

    // Start is called before the first frame update
    void Start()
    {
        
        cleanSelect = cleanObjective[Random.Range(0, cleanObjective.Length)];
        bookSelect = bookObjective[Random.Range(0, bookObjective.Length)];
        

        if (elecKeyObjective.Length != 0){
            elecKeySelect = elecKeyObjective[Random.Range(0, elecKeyObjective.Length)];
            elecKeySelect.SetActive(true);
        }
        

        cleanSelect.SetActive(true);
        bookSelect.SetActive(true);
        
        currentDay -= 1;
        DayCheck();
        
    }

    void Update(){
        
    }

    public void DayCheck(){
        switch(currentDay){
                case 0:
                    Debug.Log("Day 1");
                    break;
                case 1:
                    Debug.Log("Day 2");
                    break;
                case 2:
                    Debug.Log("Day 3");
                    electricShutDown.SetActive(true);
                    break;
        } 
    }

    public void SpawnKnight(){
        spawnSelect = knightSpawnPosition[Random.Range(0, knightSpawnPosition.Length)];
        //spawn knight at spawn position
        spawnAI = Instantiate(knightStatueAI, spawnSelect.transform.position, Quaternion.identity);
        
    }

    public void DespawnKnight(){
        if (spawnAI != null){
            Destroy(spawnAI);
            Debug.Log("Destroy" + spawnAI);
        }
    }
}
