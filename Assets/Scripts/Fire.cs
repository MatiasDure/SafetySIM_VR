using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    [SerializeField] float drainAmount;
    [SerializeField] float regenAmount;
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField] GameObject fireTarget;
    [SerializeField] FireExtinguish.Type extinguishNeeded;
    [SerializeField] Canvas wrongExtinguisher;

    private bool isHelperActive = false;
    private bool isFireOn = false;

    public static event Action fireStarted;
    public static event Action fireOff;

    Vector3 scaleDown; // Must use vector in order to scale down
    Vector3 scaleUp; // Regen
    Vector3 maxSize;

    private void Awake()
    {
        if (!fireTarget) Debug.Log("Provide a target in the firehelp script!");
        else
        {
            fireTarget = Instantiate(fireTarget);
            fireTarget.transform.parent = this.gameObject.transform.parent;
            fireTarget.transform.position = this.gameObject.transform.position;
            fireTarget.SetActive(false);
        }
    }

    void Start()
    {
        maxSize = new Vector3(1f, 1f, 1f); ;
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        sphereCollider.radius = 0.1f;

        //subscribe to fire event
        FireExtinguish.MissedFire += ActivateHelper;
        FireExtinguish.HitFire += DeactivateHelper;
    }

    void Update()
    {
        FireRegen();
    }
    public void ExtinguishFire(FireExtinguish.Type extinguishUsed)
    {
        if (extinguishUsed != extinguishNeeded)
        {
            wrongExtinguisher.gameObject.SetActive(true);

            return;
        }

        wrongExtinguisher.gameObject.SetActive(false);

        if (transform.localScale.x > 0.05f)
        {
            scaleDown = new Vector3(drainAmount, drainAmount, drainAmount) * Time.deltaTime;
            transform.localScale -= scaleDown;
            if (!sphereCollider) return;
            sphereCollider.radius -= drainAmount * Time.deltaTime * 0.4f;
        }
        else
        {
            //fired put out
            fireOff?.Invoke();
            this.gameObject.transform.parent.gameObject.SetActive(false);
            //Destroy(this.gameObject.transform.parent.gameObject);
        }
    }

    void FireRegen()
    {
        if (transform.localScale.x < maxSize.x)
        {
            scaleUp = new Vector3(regenAmount, regenAmount, regenAmount) * Time.deltaTime;
            transform.localScale += scaleUp;
            sphereCollider.radius += scaleUp.x * 0.6f;
        }
        else if(!isFireOn)
        {
            //when fire reaches its max size for the first time, trigger event 
            fireStarted?.Invoke();
            isFireOn = true;
        }
    }

    void ActivateHelper()
    {
        if (isHelperActive && !fireTarget) return;
        fireTarget.SetActive(true);
        isHelperActive = true;
    }

    void DeactivateHelper()
    {
        if (!isHelperActive && !fireTarget) return;
        isHelperActive = false;
        fireTarget.SetActive(false);
    }

    private void OnDestroy()
    {
        //unsubscribed to fire event
        FireExtinguish.MissedFire -= ActivateHelper;
        FireExtinguish.HitFire -= DeactivateHelper;
    }
}