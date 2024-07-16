using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cylib
{
    public static List<float> GetDistribution(int distSeed, int distLen){
        Random.InitState(distSeed);
        List<float> dist = new List<float>();
        for(int i = 0; i<distLen; i++){
            dist.Add(0);
        }

        int offset = Random.Range(0,distLen);
        
        float total = ((float)distLen) / 2;
        
        for(int i = 0; i<distLen; i++){
            float randNum = Random.Range(Mathf.Max(0,total-((distLen-1.0f)-i)), Mathf.Min(total,1f));
            total -= randNum;
            dist[(i + offset)%distLen] = randNum;
        }
        return dist;
    }
}
