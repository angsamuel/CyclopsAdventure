using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class KillZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Creature>()!=null){
            SceneManager.LoadScene("MainMenu");
        }
    }
}
