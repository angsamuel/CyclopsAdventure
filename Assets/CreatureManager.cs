using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    // Start is called before the first frame update
    List<Creature> creatures;
    public static CreatureManager singleton{get; private set;}

    public void Awake(){
        if(singleton == null){
            singleton = this;
        }else{
            Destroy(this.gameObject);
            Debug.LogError("Error! Multiple Creature Managers in Scene!");
        }
        creatures = new List<Creature>();
    }

    public void RegisterCreature(Creature creature){
        creatures.Add(creature);
    }
    public List<Creature> GetCreatures(){
        return creatures;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
