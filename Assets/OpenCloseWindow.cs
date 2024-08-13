using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCloseWindow : MonoBehaviour
{
    public void OpenWindow(){
        GetComponent<Canvas>().enabled = true;
    }

    public void CloseWindow(){
        GetComponent<Canvas>().enabled = false;
    }
}
