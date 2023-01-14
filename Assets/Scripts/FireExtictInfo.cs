using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireExtictInfo : MonoBehaviour
{
    [SerializeField] Canvas canvas;

    private void Awake()
    {
        if (!canvas)
        {
            Debug.Log("You need to pass the canvas to the fire extinguisher!");
            canvas = gameObject.GetComponent<Canvas>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowInfo(bool show) => canvas.gameObject.SetActive(show);
}
