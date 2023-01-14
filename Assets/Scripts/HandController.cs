using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class HandController : MonoBehaviour
{
    [SerializeField] InputActionReference PrimarySecondaryButtonsTouched;
    [SerializeField] Hand hand;
    [SerializeField] bool leftHand;
    ActionBasedController controller;

    private void Awake()
    {
        controller = GetComponent<ActionBasedController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(leftHand) hand.SetLeftGrip(controller.selectAction.action.ReadValue<float>());
        else hand.SetGrip(controller.selectAction.action.ReadValue<float>());
        hand.SetTrigger(controller.activateAction.action.ReadValue<float>());
        hand.SetThumb(PrimarySecondaryButtonsTouched.action.ReadValue<float>());
    }
}
