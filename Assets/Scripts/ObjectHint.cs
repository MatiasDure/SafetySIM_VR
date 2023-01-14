using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHint : MonoBehaviour
{
    [SerializeField] GameObject hintPrefab;
    
    public void ActivateHint(bool activate)
    {
        hintPrefab.SetActive(activate);
    }
}
