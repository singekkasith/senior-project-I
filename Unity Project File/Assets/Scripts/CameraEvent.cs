using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraEvent : MonoBehaviour
{
    public GameObject creditMenu;
    public GameObject mainMenu;

    public GameObject menuCam;

    public GameObject creditCamPos;
    public GameObject mainCamPos;
    
    private float waitTime = 2f;
    
    private float transitionTime = 1f;

    //transition
    [SerializeField] private Animator transition;

    [SerializeField] private float sceneTransitionTime = 1f;

    //Lights 
    private bool isOn;

    private int cooldownTime = 15;
    [SerializeField] private GameObject m_Light;

    //Gimmicks

    private bool isSpawn = false;
    [SerializeField] private GameObject knight;

    [SerializeField] private GameObject spawnLocation;

    void Start()
    {
        Time.timeScale = 1f;
        LightsOn();
    }
    
    public void PlayGame()
    {
        CursorLock();
        LoadStart();
    }


    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Credit()
    {
        CursorLock();
        StartCoroutine(TransitionToCredit());
        
    }

    public void Back()
    {
        CursorLock();
        StartCoroutine(TransitionToMainMenu());
    }

    public void StartCamPan()
    {
        //camAnim.Play("StartCam");
    }

    IEnumerator TransitionToCredit()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        menuCam.transform.position = creditCamPos.transform.position;
        menuCam.transform.rotation = creditCamPos.transform.rotation;
        transition.SetTrigger("Back");
        creditMenu.SetActive(true);
        CursorUnlock();
    }

    IEnumerator TransitionToMainMenu()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        menuCam.transform.position = mainCamPos.transform.position;
        menuCam.transform.rotation = mainCamPos.transform.rotation;
        mainMenu.SetActive(true);
        transition.SetTrigger("Back");
        CursorUnlock();
    }

    

    public void LoadStart(){
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadScene(int levelIndex){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(sceneTransitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    public void LightsOff(){
        isOn = false;
        m_Light.SetActive(false);
        if (!isSpawn){
        GameObject knightSpawn = Instantiate(knight, spawnLocation.transform.position, spawnLocation.transform.rotation);
        } 
        StartCoroutine(LightsOffCoolDown(cooldownTime));
    }

    public void LightsOn(){
        isOn = true;
        m_Light.SetActive(true);
        StartCoroutine(LightsOffCoolDown(cooldownTime));
        
    }

    IEnumerator LightsOffCoolDown(int cooldown){
        yield return new WaitForSeconds(cooldown);
        if (isOn){
            LightsOff();
        }
        else {
            LightsOn();
        }
        
    }

    private void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
