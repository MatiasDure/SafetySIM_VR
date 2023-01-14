using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FireExtinguish : MonoBehaviour
{
    [SerializeField] float raycastLength;
    [SerializeField] Type type;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource colAudio;

    GameObject currentFire;

    public static event Action MissedFire;
    public static event Action HitFire;

    public bool isEnabled = false;

    public enum Type
    {
        CO2,
        POWDER,
        WATER, 
        FOAM
    };

    private Dictionary<GameObject, Fire> fires;

    bool canRaycast = false;

    private void Awake()
    {
        if(!animator) animator = GetComponent<Animator>();
    }

    private void Start()
    {
        fires = new Dictionary<GameObject, Fire>();
    }

    void Update()
    {
        if(canRaycast && isEnabled) ShootRaycast();
    }

    void ShootRaycast()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, raycastLength))
        {
            if (hitInfo.collider.CompareTag("FireTrigger"))
            {
                currentFire = hitInfo.collider.gameObject;
                if (fires.ContainsKey(currentFire)) fires[currentFire].ExtinguishFire(type);
                else
                {
                    fires.Add(currentFire, currentFire.transform.parent.GetComponentInChildren<Fire>());
                    fires[currentFire].ExtinguishFire(type);
                }
                HitFire?.Invoke();
            }
            else MissedFire?.Invoke();
        }
        else MissedFire?.Invoke();
    }

    public void ActivateRaycast() // Called by trigger
    {
        canRaycast = true;
        if (!animator) return;
        animator.SetBool("isFiring", true);
    }

    public void DeactivateRaycast() // Called by trigger
    {
        canRaycast = false;
        if (!animator) return;
        animator.SetBool("isFiring", false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Hand") && !collision.gameObject.CompareTag("ExtinguisherContainer"))
        {
            colAudio.Play();
        }
    }
}
