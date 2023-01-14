using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ContinuousMoveProviderBase))]
public class Movement : MonoBehaviour
{
    [SerializeField] InputActionReference runAction;
    [SerializeField] ContinuousMoveProviderBase moveSystem;
    [SerializeField] float runSpeed;
    [SerializeField] float walkSpeed;

    private void Awake()
    {
        if (!runAction) Debug.Log("Add the run action binding to the movement script in xr origin!");
        if(!moveSystem) moveSystem = gameObject.GetComponent<ContinuousMoveProviderBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!runAction) return;
        if (Running()) moveSystem.moveSpeed = runSpeed;
        else moveSystem.moveSpeed = walkSpeed;
    }

    private bool Running() => runAction.action.ReadValue<float>() == 1;
}
