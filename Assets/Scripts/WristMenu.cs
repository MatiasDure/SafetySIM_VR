using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas), typeof(AudioSource))]
public class WristMenu : MonoBehaviour
{
    [SerializeField] InputActionReference menuButton;
    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI wristText;
    [SerializeField] Image background;
    [SerializeField] AudioSource newMessageSound;
    [SerializeField] GameObject hintObject;
    [SerializeField] TextMeshProUGUI winningTime;
    [SerializeField] Image winningImg;

    private static WristMenu _wristMenuSingleton;
    public static WristMenu WristMenuSingleton { get => _wristMenuSingleton; }

    private int hintsShowed = 0;

    public static event Action notReadyToLook;
    public static event Action notReadyToPull;
    public static event Action notReadyToCheck;

    private void Awake()
    {
        //creating singleton
        if (_wristMenuSingleton != null && _wristMenuSingleton != this)
        {
            Destroy(this.gameObject);
        }
        else _wristMenuSingleton = this;

        if (!newMessageSound) newMessageSound = GetComponent<AudioSource>();
        if (!menuButton) Debug.Log("Pass the reference of the menu input action to the wrist menu!");
        if (!wristText) Debug.Log("Pass the text object to the wrist menu script!");
        else
        {
            menuButton.action.Enable();
            menuButton.action.performed += ToggleMenu;
        }
        if (!canvas) canvas = GetComponent<Canvas>();
        if (!hintObject) Debug.Log("Pass the hint object to the wrist menu script!");
        if (!winningImg) Debug.Log("Pass the winnig img object to the wrist menu script!");
        if (!winningTime) Debug.Log("Pass the text object for the winning time to the wrist menu script!"); ;
    }

    private void Start()
    {
        Fire.fireStarted += FireStartedHint;
        SearchFire.FireFound += FireFoundHint;
        AlarmStatus.alarmPulled += AlarmTriggeredHint;
        SimulationStatus.GameWon += WonGame;
        SearchFire.PlanFound += CheckedFloorPlanHint;
        wristText.text = "";
        winningTime.gameObject.SetActive(false);
        winningImg.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        menuButton.action.performed -= ToggleMenu;
        Fire.fireStarted -= FireStartedHint;
        SearchFire.FireFound -= FireFoundHint;
        AlarmStatus.alarmPulled -= AlarmTriggeredHint;
        SimulationStatus.GameWon -= WonGame;
        SearchFire.PlanFound -= CheckedFloorPlanHint;
    }

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        canvas.enabled = !canvas.enabled;
        if (!hintObject) return;
        hintObject.SetActive(false);
    }

    private void FireStartedHint()
    {
        wristText.text = "Something is burning! Find the source of the smell";
        NewHint();
        hintsShowed = 1;
    }

    private void FireFoundHint()
    {
        if (hintsShowed < 1)
        {
            notReadyToLook?.Invoke();
            return;
        }
        wristText.text = "Trigger the fire alarm by pulling the lever and call 112";
        NewHint();
        hintsShowed = 2;
    }

    private void AlarmTriggeredHint()
    {
        if (hintsShowed < 2)
        {
            notReadyToPull?.Invoke();
            return;
        }
        wristText.text = "Check the floorplan for extinguishers' locations";
        NewHint();
        hintsShowed = 3;
    }

    private void CheckedFloorPlanHint()
    {
        if (hintsShowed < 3)
        {
            notReadyToCheck?.Invoke();
            return;
        }
        wristText.text = "Look for the correct fire extinguisher type to control the fire";
        NewHint();
    }

    private void WonGame(object sender, float timeWon)
    {
        NewHint();
        wristText.text = "";
        background.gameObject.SetActive(false);
        wristText.gameObject.SetActive(false);
        winningTime.gameObject.SetActive(true);
        winningImg.gameObject.SetActive(true);
        winningTime.text = "" + Math.Round(timeWon, 2);
    }

    public void NewHint()
    {
        //Activate icon to show that there is a new hint
        newMessageSound.Play();
        if (!hintObject) return;
        hintObject.SetActive(true);
    }
}
