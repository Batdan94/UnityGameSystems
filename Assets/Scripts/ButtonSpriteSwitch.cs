using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class ButtonSpriteSwitch : MonoBehaviour {

    public Button button;
    public Sprite buttonImageOn;
    public Sprite buttonImageOff;
    public AudioSource audio;

    public void OnOffButton()
    {
        if (button.GetComponent<Image>().sprite == buttonImageOn)
        {
            button.GetComponent<Image>().sprite = buttonImageOff;
            AudioListener.volume = 0;
        }
        else
        {
            button.GetComponent<Image>().sprite = buttonImageOn;
            AudioListener.volume = 1;
        }
    }

    public void muteSound()
    {
        audio.mute = !audio.mute;
    }
}
