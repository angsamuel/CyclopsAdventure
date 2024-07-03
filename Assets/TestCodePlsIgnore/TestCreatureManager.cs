using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCreatureManager : MonoBehaviour
{
    public static TestCreatureManager singleton{get; private set;}

    void Awake(){
        if(singleton != null){
            Destroy(this.gameObject);
        }
        singleton = this;
    }

    [SerializeField] List<Creature> creatures;
    public void RegisterCreature(Creature c){
        creatures.Add(c);
    }
    public void RemoveCreature(Creature c){
        if(creatures.Contains(c)){
            creatures.Remove(c);
        }
    }
}
