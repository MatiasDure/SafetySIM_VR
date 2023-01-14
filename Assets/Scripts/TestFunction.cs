using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFunction : MonoBehaviour
{
    float timer = 10;
    Vector3 moveTowards;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += moveTowards * 0.2f;
        if (timer <= 0) Destroy(this.gameObject);
        else timer -= Time.deltaTime;
    }

    public void SetMoveTowards(Vector3 towards)
    {
        moveTowards = towards;
        moveTowards.Normalize();
    }
}
