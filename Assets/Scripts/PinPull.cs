using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinPull : MonoBehaviour
{
    [SerializeField] FireExtinguish extinguisherScript;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject particles;
    [SerializeField] Animator handlesAnim;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PullPin()
    {
        anim.SetBool("pulledPin", true);
        extinguisherScript.isEnabled = true;
        audioSource.enabled = true;
        handlesAnim.enabled = true;
        particles.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
            PullPin();
    }
}
