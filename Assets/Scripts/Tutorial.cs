using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource))]
public class Tutorial : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] UnityEngine.UI.Image infoImg;
    [SerializeField] Sprite[] slides;
    [SerializeField] UnityEngine.UI.Button skipButton;
    [SerializeField] AudioClip[] botSpeech;
    
    
    AudioSource botVoice;
    const int amountSlides = 5;
    int currentSlide = 1;

    private void Awake()
    {
        if (!infoText) GetComponentInChildren<TextMeshProUGUI>();
        if (!infoImg) GetComponentInChildren<UnityEngine.UI.Image>();
        if (!skipButton) Debug.Log("Pass in the skip button object to the tutorial script!");
        botVoice = GetComponent<AudioSource>();
    }

    private void Start()
    {
        BotTalkSound();
    }

    public void NextSlide(bool skip = false)
    {
        currentSlide++;
        if (skip || currentSlide == amountSlides+1)
        {
            SceneManager.LoadScene("TiaScene");
            return;
        }

        switch (currentSlide)
        {
            case 2:
                infoImg.sprite = slides[0];
                infoText.text = "Buttons and their functionalities";        
                break;
            case 3:
                infoImg.sprite = slides[1];
                infoText.text = "New hints are available when the icon appears on your LEFT wrist";
                break;
            case 4:
                infoImg.sprite = slides[2];
                infoText.text = "Restart and Exit icons";
                break;
            case 5:
                infoImg.sprite = slides[3];
                infoText.text = "Good Luck!";
                skipButton.gameObject.SetActive(false);
                break;
        }
        BotTalkSound();
    }

    private void BotTalkSound()
    {
        botVoice.Stop();
        int soundToPlay = currentSlide - 1;
        if(botSpeech.Length > soundToPlay) botVoice.PlayOneShot(botSpeech[soundToPlay]);
    }
}
