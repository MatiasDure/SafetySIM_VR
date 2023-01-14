//using Palmmedia.ReportGenerator.Core.Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneInteraction : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numberToCall;
    [SerializeField] AudioClip emergencyCall;
    [SerializeField] AudioClip[] dialing;
    [SerializeField] AudioClip callDropped;
    [SerializeField] AudioSource source;

    private string numbersPressed = "";
    private int maxLengthNum = 6;
    private bool calling = false;

    private void OnTriggerEnter(Collider other)
    {
        string otherTag = other.gameObject.tag;

        int num;
        if (int.TryParse(otherTag, out num) && numbersPressed.Length < maxLengthNum)
        {
            numbersPressed += num;
            UpdateText();
            source.PlayOneShot(dialing[num]);
        }
        else if (otherTag == "Delete") EraseNums();
        else if (otherTag == "Call") Call();
    }

    private void Update()
    {
        if (calling && !source.isPlaying)
        {
            UpdateText();
            calling = false;
        }
    }

    private void UpdateText() => numberToCall.text = numbersPressed;
    private void EraseNums()
    {
        numbersPressed = "";
        numberToCall.text = numbersPressed;
    }
    private void Call()
    {
        if (!source || !emergencyCall || calling) return;
        
        if (numbersPressed == "112") StartCall(emergencyCall);
        else StartCall(callDropped);

        calling = true;
    }

    private void StartCall(AudioClip clipToPlay)
    {
        numberToCall.text = "Calling " + numbersPressed;
        source.PlayOneShot(clipToPlay);
    }

}
