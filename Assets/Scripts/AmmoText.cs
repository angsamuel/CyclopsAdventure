using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoText : MonoBehaviour
{

    [SerializeField] Creature playerCreature;
    [SerializeField] TextMeshProUGUI counterText;
    // Update is called once per frame
    int currentAmmo;
    void Update()
    {
        counterText.text = playerCreature.GetTool().GetCurrentAmmo() + " / " + playerCreature.GetTool().GetMaxAmmo();
    }
}
