using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryFlag : MonoBehaviour
{
   SpriteRenderer spriteRenderer;
   [SerializeField] Color winColor;
   [SerializeField] Color defaultColor;
   void Awake(){
    spriteRenderer = GetComponent<SpriteRenderer>();
   }

   void OnTriggerEnter2D(Collider2D other)
   {
        if(!other.CompareTag("Creature")){
            return;
        }
        if(!other.GetComponent<Creature>().isPlayerCreature){
           return;
        }
        Win();
   }

   void Win(){
    spriteRenderer.color = winColor;
   }
}
