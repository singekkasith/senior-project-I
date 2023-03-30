using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHitBox : MonoBehaviour
{
    //Dead Scene
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;

    private PauseMenu pauseMenuScript;
    [SerializeField] private AudioSource gameOverSound;
    private GameObject canvas;


    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        pauseMenuScript = canvas.GetComponent<PauseMenu>();
        transition = canvas.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider player){
        if (player.gameObject.CompareTag("Player")) {
            gameOverSound.Play();
            pauseMenuScript.GameOver();
            //LoadDeathScene();
        }
        
    }

    public void LoadDeathScene(){
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadScene(int levelIndex){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
