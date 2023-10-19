using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossfightPlayerHealthHUD : MonoBehaviour
{
    public PlayerHealthManager playerHealth;
    
    [SerializeField]
    private TMP_Text _text;
    
    // Update is called once per frame
    void Update()
    {
        if (playerHealth.lives > 3) { _text.text = "∞/3"; }
        else { _text.text = playerHealth.lives + "/3"; }
    }
}
