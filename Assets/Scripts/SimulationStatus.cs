using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationStatus : MonoBehaviour
{
    [SerializeField] int amountFiresInSim;

    private int firesTurnedOff = 0;
    public static event EventHandler<float> GameWon;

    private bool won = false;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        Fire.fireOff += TurnFireOff;
    }

    // Update is called once per frame
    void Update()
    {
        if(!won && WonGame())
        {
            GameWon?.Invoke(this, timer);
            //turn off fire alarm
            //turn off npc screaming
            //disable npc
            won = true;
        }
        else if(!won)
        {
            timer += Time.deltaTime;
        }
    }

    void TurnFireOff() => firesTurnedOff++;

    bool WonGame() => firesTurnedOff == amountFiresInSim;

    private void OnDestroy()
    {
        Fire.fireOff -= TurnFireOff;
    }

}
