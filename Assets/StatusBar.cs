using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] float healthPercentage = 1;
    [SerializeField] Transform barFillTransform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        barFillTransform.localScale = new Vector3(healthPercentage,1,1);
    }
}
