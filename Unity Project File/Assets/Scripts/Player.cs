using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private LayerMask pickableLayerMask;


    [SerializeField]
    private Transform playerCameraTransform;

    [SerializeField]
    private GameObject pickUpUI;

    [SerializeField]
    [Min(1)]
    private float hitRange = 3;

    [SerializeField]
    private Transform pickUpParent;

    [SerializeField]
    private GameObject inHandItem;

    [SerializeField]
    private InputActionReference interactionInput, dropInput, useInput;
    private RaycastHit hit;

    [SerializeField]
    private AudioSource pickUpSource;

    [SerializeField]
    private float itemdropForce = 10f;

    public StaminaBar staminaBar;
    private float maxStamina = 100f;
    private float currentStamina;

    private void Start(){
        interactionInput.action.performed += PickUp;
        dropInput.action.performed += Drop;
        useInput.action.performed += Use;

        currentStamina = maxStamina;
        staminaBar.SetMaxTime(maxStamina);
    }

    private void Use(InputAction.CallbackContext obj)
    {
        if (inHandItem != null)
        {
            IUsable usable = inHandItem.GetComponent<IUsable>();
            if (usable != null){
                usable.Use(this.gameObject);
            }
        }        
    }

    private void Drop(InputAction.CallbackContext obj)
    {
        if (inHandItem != null){
            pickUpSource.Play();
            inHandItem.transform.SetParent(null);
            inHandItem = null;
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null){
                rb.isKinematic = false;
                rb.AddForce(playerCameraTransform.forward * itemdropForce + playerCameraTransform.transform.up * 2, ForceMode.Impulse);
            }
        }
    }

    private void PickUp(InputAction.CallbackContext obj)
    {
        if (hit.collider != null && inHandItem == null){
            IPickable pickableItem = hit.collider.GetComponent<IPickable>();
            if (pickableItem != null)
            {
                pickUpSource.Play();
                inHandItem = pickableItem.PickUp();
                inHandItem.transform.SetParent(pickUpParent.transform, pickableItem.KeepWorldPosition);
            }

            // Debug.Log(hit.collider.name);
            // Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            // if (hit.collider.GetComponent<Item>()){
            //     Debug.Log("Holding an item");
            //     inHandItem = hit.collider.gameObject;
            //     inHandItem.transform.position = Vector3.zero;
            //     inHandItem.transform.rotation = Quaternion.identity;
            //     inHandItem.transform.SetParent(pickUpParent.transform, false);
            //     if (rb != null){
            //         rb.isKinematic = true;
            //     }
            //     return;
            // }

            // if (hit.collider.GetComponent<HeavyItem>())
            // {
            //     Debug.Log("Picking up an object");
            //     inHandItem = hit.collider.gameObject;
            //     inHandItem.transform.SetParent(pickUpParent.transform, true);
            //     if (rb != null){
            //         rb.isKinematic = true;
            //     }
            //     return;
            // }

        }

        
    }

    private void Update(){

        
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);

        if (hit.collider != null){
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            pickUpUI.SetActive(false);

        }

        if (inHandItem != null){
            return;
        }

        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange, pickableLayerMask)){
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            pickUpUI.SetActive(true);
        }

    }

    internal void AddHealth(int healthBoost)
    {
        Debug.Log($"Health boosted by {healthBoost}");
    }
}
