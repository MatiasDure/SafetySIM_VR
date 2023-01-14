using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmStatus : MonoBehaviour
{
    [SerializeField] AudioSource alarmSource;
    [SerializeField] Transform lever;
    [SerializeField] float minZ;
    [SerializeField] float maxZ;

    private bool active = false;
    private bool pulled = false;
    GameObject obj;
    float currentZValue;
    const float ALMOST_MIN = 0.05f;
    const float ALMOST_MAX = 89.8f;

    public static event Action alarmPulled;

    private void Awake()
    {
        if (!lever) Debug.Log("Add the hinge transform to the Fire alarm!");
        if(!alarmSource) Debug.Log("Add an audio source to the Fire alarm!");
    }

    private void Start()
    {
        WristMenu.notReadyToPull += CantPullYet;
        SimulationStatus.GameWon += WonGame;

    }

    // Update is called once per frame
    void Update()
    {
        currentZValue = lever.localEulerAngles.z;
        if (!active && AlarmActivated())
        {
            alarmSource.Play();
            active = true;
            if(!pulled)
            {
                pulled = true;
                alarmPulled?.Invoke();
            }
        }
        else if(active && AlarmDeactivated())
        {
            alarmSource.Stop();
            active = false;
        }
    }

    bool AlarmActivated() => ALMOST_MAX <= currentZValue;
    bool AlarmDeactivated() => ALMOST_MIN >= currentZValue;
    void CantPullYet() => pulled = false;
    void WonGame(object sender, float timer) => alarmSource.Stop();

    private void OnDestroy()
    {
        WristMenu.notReadyToPull -= CantPullYet;
        SimulationStatus.GameWon -= WonGame;
    }

}
