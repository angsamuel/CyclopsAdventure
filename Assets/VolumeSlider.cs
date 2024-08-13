using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] string volumeSetting = "MasterVolume";
    [SerializeField] TextMeshProUGUI percentageText;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider slider;

    void Start(){
        slider.value = PlayerPrefs.GetFloat(volumeSetting);
    }

    public void SetVolume(){

        float decVolume = Mathf.Log10(slider.value) * 20;
        if(slider.value == 0){
            decVolume = -80;
        }
        percentageText.text = ((int)(slider.value*100)).ToString() + "%";
        audioMixer.SetFloat(volumeSetting,decVolume);
        PlayerPrefs.SetFloat(volumeSetting,slider.value);
    }
}
