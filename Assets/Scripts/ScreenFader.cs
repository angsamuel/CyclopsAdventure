using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{

    [Header("Config")]

    [SerializeField] float fadeTime = 1;
    [SerializeField] Color fadeColor = Color.black;

    bool fadingOut = false;
    Image fadeImage;

    void Awake(){
        fadeImage = GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        FadeIn();
    }

    public void FadeIn(){

        StartCoroutine(FadeInRoutine());
        IEnumerator FadeInRoutine(){
            float timer = 0;
            fadeImage.color = fadeColor;
            while(timer < fadeTime){
                yield return null;
                timer+=Time.deltaTime;
                fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1f - (timer/fadeTime));
            }
            fadeImage.color = Color.clear;
        }
    }

    public void FadeOut(string newScene){
        if(fadingOut){
            return;
        }
        fadingOut = true;
        StartCoroutine(FadeOutRoutine());
        IEnumerator FadeOutRoutine(){
            float timer = 0;
            fadeImage.color = Color.clear;
            while(timer < fadeTime){
                yield return null;
                timer+=Time.deltaTime;
                fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, timer/fadeTime);
            }
            fadeImage.color = fadeColor;
            SceneManager.LoadScene(newScene);
        }
    }
}
