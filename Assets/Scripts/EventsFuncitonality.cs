using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventsFuncitonality : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] InputActionReference selector;

    private bool picked = false;

    private void Awake()
    {
        if (!canvas)
        {
            Debug.Log("You need to pass the canvas to the fire extinguisher!");
            canvas = gameObject.GetComponent<Canvas>();
        }
        if (!bulletPrefab) Debug.Log("You need to pass the bullet Prefab to the fire extinguisher!");
        //selector.action.started += PickUp;
        //selector.action.canceled += Drop;

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Shoot()
    {
        if (!picked) return;
        GameObject instance = Instantiate(bulletPrefab);
        instance.transform.position = this.gameObject.transform.forward + this.gameObject.transform.position;
        TestFunction testFunc = instance.GetComponent<TestFunction>();
        if (testFunc) testFunc.SetMoveTowards(this.gameObject.transform.forward);
    }

    public void ShowInfo()
    {
        if (picked) return;
        canvas.gameObject.SetActive(true);
    }

    public void HideInfo() => canvas.gameObject.SetActive(false);

    public void PickUp()//InputAction.CallbackContext context)
    {
        picked = true;
        HideInfo();
        Debug.Log("Gun got picked");
    }

    public void Drop()//InputAction.CallbackContext context)
    {
        picked = false;
        Debug.Log("Gun got dropped");
    }
}