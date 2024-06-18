using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{


    public void Hurt(){
        GetComponent<SpriteRenderer>().color = new Color(0,1,1);
    }
}
