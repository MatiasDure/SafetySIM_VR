using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        bool cool = other.gameObject.tag == "1";
        if(cool)
        {
            Debug.Log("We clicked 1!");
        }
    }

}
