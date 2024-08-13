using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TimeController : MonoBehaviour
{

    public static TimeController singleton;
    public AudioMixer mixer;
    float timeFactor = 4f;
    float physicsTimeScale = 0.02f;
    public float audioFactor = 1.5f;
    float audioPitch = 1f;
    bool timeSlowed = false;
    [SerializeField] float maxTimeLimit = 5;
    [SerializeField] float currentTimeLimit = 5;


    void Awake(){
        singleton = this;
    }
    void Start()
    {
        ResumeTime();
    }

    void Update(){
        if(timeSlowed){
            currentTimeLimit -= Time.deltaTime;
        }
        if(currentTimeLimit <= 0 && timeSlowed){
            ResumeTime();
        }
        if(!timeSlowed){
            currentTimeLimit += Time.deltaTime;
            if(currentTimeLimit > maxTimeLimit){
                currentTimeLimit = maxTimeLimit;
            }
        }

    }

    public void ToggleTime(){
        if(timeSlowed){
            ResumeTime();

        }else{
            SlowTime();
        }
    }

    public void SlowTime(){
        if(currentTimeLimit <= 0){
            return;
        }
        timeSlowed = true;
        Time.timeScale /= timeFactor;
        Time.fixedDeltaTime = Time.timeScale * physicsTimeScale;

        audioPitch /= audioFactor;
        mixer.SetFloat("Pitch",audioPitch);

    }

    public void ResumeTime(){
        timeSlowed = false;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = physicsTimeScale;
        audioPitch = 1f;
        mixer.SetFloat("Pitch",audioPitch);
        currentTimeLimit = -2;
    }
}