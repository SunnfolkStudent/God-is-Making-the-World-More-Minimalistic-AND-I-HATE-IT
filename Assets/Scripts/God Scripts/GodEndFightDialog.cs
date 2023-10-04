using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class GodEndFightDialog : MonoBehaviour
{
    public GameObject dialoguePanel;
    
    public InputCubeManager _inputCubeManager;
    public PlayerCubeMovement _playerCubeMovement;
    
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    
    public string[] dialogue;
    public string[] name;
    
    private int index = 0;

    public float wordSpeed;
    public bool godIsDead;
    
    private float timer;
    private bool timerDone;
    private bool timerOn;
    //public bool canGoToNextLine = true;


    void Start()
    {
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

        
        if (godIsDead)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                _inputCubeManager.canMove = false;
                _playerCubeMovement.canMove = false;
                
                dialoguePanel.SetActive(true);
                
                StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogue[index] && Input.GetKeyDown(KeyCode.E))
            {
                NextLine();
            } 
            
            if (dialogueText.text == dialogue[index])
            {
                //canGoToNextLine = true;
            }

        }
        /*if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
        {
            RemoveText();
        }*/
    }

    public void RemoveText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        _inputCubeManager.canMove = true;
        _playerCubeMovement.canMove = true;
        godIsDead = false;
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        //canGoToNextLine = false;
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            nameText.text = name[index];
            StartCoroutine(Typing());
        }
        else
        {
            RemoveText();
        }
        
    }
    
}