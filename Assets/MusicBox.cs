using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] float fadeDuration = 5;

    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }
        // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeMusicInRoutine());
    }
    IEnumerator FadeMusicInRoutine(){
        float originalVolume = audioSource.volume;
        audioSource.volume = 0;

        float fadeTimer = 0;
        while(fadeTimer < fadeDuration){
            yield return null;
            fadeTimer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0,originalVolume,fadeTimer/fadeDuration);
        }
        audioSource.volume = originalVolume;
    }

    IEnumerator FadeMusicOutRoutine(){
        float originalVolume = audioSource.volume;


        float fadeTimer = 0;
        while(fadeTimer < fadeDuration){
            yield return null;
            fadeTimer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(originalVolume,0,fadeTimer/fadeDuration);
        }
        audioSource.volume = 0;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
