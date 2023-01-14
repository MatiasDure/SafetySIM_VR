using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField]SkinnedMeshRenderer handMesh;
    Animator animator;
    private float gripTarget, triggerTarget, thumbTarget, leftGripTarget;
    private float gripCurrent, triggerCurrent, thumbCurrent, leftGripCurrent;
    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";
    private string animatorThumbParam = "Thumb";
    private string animatorLeftGripParam = "LeftGrip";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (!handMesh) GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
    }
    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }

    internal void SetThumb(float v)
    {
        thumbTarget = v;
    }

    internal void SetLeftGrip(float v)
    {
        leftGripTarget = v;
    }

    void AnimateHand()
    {
        if(gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }
        if(triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
        if (thumbCurrent != thumbTarget)
        {
            thumbCurrent = Mathf.MoveTowards(thumbCurrent, thumbTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorThumbParam, thumbCurrent);
        }
        if (leftGripCurrent != leftGripTarget)
        {
            leftGripCurrent = Mathf.MoveTowards(leftGripCurrent, leftGripTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorLeftGripParam, leftGripCurrent);
        }
    }

    public void ToggleVisibility()
    {
        handMesh.enabled = !handMesh.enabled;
    }

}
