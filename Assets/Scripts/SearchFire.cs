using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchFire : MonoBehaviour
{
    public static event Action FireFound;
    public static event Action PlanFound;
    bool fireFound = false;
    bool planFound = false;

    private void Start()
    {
        WristMenu.notReadyToLook += CantFintYet;
        WristMenu.notReadyToCheck += CantCheckYet;
    }

    private void FixedUpdate()
    {
        if (planFound) return;
        RaycastHit hit;
        Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);

        if(Physics.Raycast(ray,out hit))
        {
            string tag = hit.transform.gameObject.tag;
            if(tag == "FireTrigger" && !fireFound)
            {
                fireFound = true;
                FireFound?.Invoke();
            }else if(tag == "FloorPlan")
            {
                planFound = true;
                PlanFound?.Invoke();
            }
        }
    }
    private void CantFintYet() => fireFound = false;
    private void CantCheckYet() => planFound = false;

    private void OnDestroy()
    {
        WristMenu.notReadyToLook -= CantFintYet;
        WristMenu.notReadyToCheck -= CantCheckYet;
    }

}
