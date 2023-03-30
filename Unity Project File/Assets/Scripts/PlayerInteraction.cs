using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private LayerMask interactableLayer;
    public float interactionDistance;
    public TMPro.TextMeshProUGUI interactionText;
    public GameObject interactionHoldGO;
    public GameObject interactionGO;
    public UnityEngine.UI.Image interactionHoldProgress;
    Camera cam;
    public GameObject mainCamera;

    [SerializeField]
    private GameObject holdSFXSound;
    private AudioSource audioSource;

    [SerializeField]
    private GameObject lantern;

    [SerializeField]

    private LanternBar lanternBarScript;
    private bool isLanternOn;

    private RaycastHit hit;

    


    void Start()
    { 
        mainCamera = GameObject.Find("MainCamera");
        cam = mainCamera.GetComponent<Camera>();
        //lanternBarScript = GameObject.Find("LanternBar").GetComponent<LanternBar>();
        isLanternOn = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
       

        bool successfulHit = false;
        
        interactionHoldGO.SetActive(false);
        interactionGO.SetActive(false);

        if (hit.collider != null){
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);

        }

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer)) {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            
            

            if (interactable != null){
                hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
                HandleInteraction(interactable);
                interactionText.text = interactable.GetDescription();
                
                successfulHit = true;
                interactionHoldGO.SetActive(interactable.interactionType == Interactable.InteractionType.Hold);
                interactionGO.SetActive(interactable.interactionType == Interactable.InteractionType.Click);
            }
    
        }


        if (!successfulHit){
            interactionText.text = "";
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.F)){
            if (!isLanternOn){
                lantern.SetActive(true);
                isLanternOn = true;
                lanternBarScript.isLitting = true;
            }
            else if (isLanternOn) {
                lantern.SetActive(false);
                isLanternOn = false;
                lanternBarScript.isLitting = false;
            } 
        }

    }

    void HandleInteraction(Interactable interactable){
        KeyCode key = KeyCode.E;
        switch (interactable.interactionType) {
            case Interactable.InteractionType.Click:
                if (Input.GetKeyDown(key)) {
                    interactable.Interact();
                }
                break;
                
            case Interactable.InteractionType.Hold:
                CheckSFX(interactable.GetHoldDuration());
                if (Input.GetKey(key)) {
                    interactable.IncreaseHoldTime();
                    Debug.Log("Hold");
                    if (interactable.GetHoldTime() > interactable.GetHoldDuration()){
                        interactable.Interact();
                        interactable.ResetHoldTime();
                        audioSource.Stop();
                    }

                    
                    if (Input.GetKeyDown(key)){
                        audioSource.Play();
                    }
                    
                }


                else {
                    //Debug.Log("Reset");
                    interactable.ResetHoldTime();
                    audioSource.Stop();
                }                
                interactionHoldProgress.fillAmount = interactable.GetHoldTime() / interactable.GetHoldDuration();
                break;
        }
    }

    private void CheckSFX(float duration){
        if (duration  == 5f ){
            //Arrange Book
            holdSFXSound = GameObject.Find("ArrangeSound");
        }

        else if (duration  == 6f ){
            //playAnimation
            holdSFXSound = GameObject.Find("BroomSound");
        }


        else {
            //Moving Large Obj
            holdSFXSound = GameObject.Find("MovingSound");
        }
        audioSource = holdSFXSound.GetComponent<AudioSource>();

    }
    
}
