using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenOptions : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle fullScreenToggle;
    Resolution[] resolutions;

    void Start(){
        resolutions = Screen.resolutions;
        if(PlayerPrefs.GetInt("chose a resolution") == 0){
            fullScreenToggle.isOn = true;
        }else{
            fullScreenToggle.isOn = PlayerPrefs.GetInt("fullscreen") == 1;
        }
        for(int i = 0; i<resolutions.Length; i++){
            string resString = resolutions[i].width.ToString() + "x" + resolutions[i].height.ToString();
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resString));
            if(PlayerPrefs.GetInt("chose a resolution") == 0){
                Debug.Log("Did not choose a resolution");
                if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
                    resolutionDropdown.value = i;
                }
            }

        }
        if(PlayerPrefs.GetInt("chose a resolution") == 1){
            resolutionDropdown.value = PlayerPrefs.GetInt("screen resolution");
            ConfirmResolution();
        }


    }

    public void ConfirmResolution(){
        Debug.Log("Changed Resolution");
        PlayerPrefs.SetInt("screen resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("chose a resolution", 1);
        Screen.SetResolution(resolutions[resolutionDropdown.value].width,resolutions[resolutionDropdown.value].height,fullScreenToggle.isOn);
        if(fullScreenToggle.isOn){
            PlayerPrefs.SetInt("fullscreen",1);
        }else{
            PlayerPrefs.SetInt("fullscreen",0);
        }
    }


}
