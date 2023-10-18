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
        _text.text = playerHealth.lives + "/3";
    }
}
