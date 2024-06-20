using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestSceneFader : MonoBehaviour
{

    [Header("Config")]
    public Color fadeColor = Color.black;
    public float fadeTime = 1;
    public Image screenCoverImage;


    void Start(){
        FadeIn(); //fade from black on start

    }

    //fade in over time
    void FadeIn(){
        screenCoverImage.enabled = true;
        screenCoverImage.color = fadeColor;


        StartCoroutine(FadeInRoutine());
        IEnumerator FadeInRoutine(){
            float timer = 0;
            while(timer < fadeTime){
                yield return null;
                timer+=Time.deltaTime;
                Color clearFadeColor = fadeColor;
                clearFadeColor.a = 0;

                screenCoverImage.color = Color.Lerp(fadeColor,clearFadeColor,timer/fadeTime);
            }
            screenCoverImage.enabled = false; //now we can press buttons again! yay!
            yield return null;
        }
    }

    //transition to new scene after fading to black
    public void FadeOut(string sceneName){
        Color clearFadeColor = fadeColor;
        clearFadeColor.a = 0;

        screenCoverImage.enabled = true;
        screenCoverImage.color = clearFadeColor;
        StartCoroutine(FadeOutRoutine());
        IEnumerator FadeOutRoutine(){
            float timer = 0;
            while(timer < fadeTime){
                yield return null;
                timer+=Time.deltaTime;
                screenCoverImage.color= Color.Lerp(clearFadeColor,fadeColor,timer/fadeTime);
            }
            screenCoverImage.color = fadeColor; //ensure we fade to black all the way
            SceneManager.LoadScene(sceneName);
            yield return null;

        }
    }

}
