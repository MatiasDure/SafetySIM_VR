using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class NPCBehaviour : MonoBehaviour
{
    [SerializeField]GameObject visual;
    [SerializeField] AudioSource screaming;
    private void Awake()
    {
        if (!visual) Debug.Log("Add the body object to the NPCBehaviour script!");
        else visual.SetActive(false);
        if(!screaming) Debug.Log("Add the body object to the NPCBehaviour script!");
    }
    void Start()
    {
        AlarmStatus.alarmPulled += Spawn;
        SimulationStatus.GameWon += WonGame;
    }

    void Spawn()
    {
        if (screaming) screaming.Play();
        visual.SetActive(true);
    }

    void WonGame(object sender, float timer)
    {
        if(screaming) screaming.Stop();
        visual.SetActive(false);
    }

    private void OnDestroy()
    {
        AlarmStatus.alarmPulled -= Spawn;
        SimulationStatus.GameWon -= WonGame;
    }
}