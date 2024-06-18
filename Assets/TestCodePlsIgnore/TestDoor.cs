using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDoor : MonoBehaviour
{

    [SerializeField] Sprite openSprite;
    [SerializeField] Sprite closeSprite;
    bool open = false;

    SpriteRenderer spriteRenderer;

    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start(){
        //Open();
    }

    public void Open(){
        spriteRenderer.sprite = openSprite;
        GetComponent<BoxCollider2D>().isTrigger = true;
        open = true;

    }

    public void Close(){
        spriteRenderer.sprite = closeSprite;
        GetComponent<BoxCollider2D>().isTrigger = false;
        open = false;
    }

    public void Toggle(){
        if(open){
            Close();
        }else{
            Open();
        }
    }
}
