using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelStartDiolouge : MonoBehaviour
{
    public GameObject dialoguePanel;
    
    public InputManager _inputManager;
    public PlayerMovement _playerMovement;
    
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    
    public string[] dialogue;
    public string[] name;
    
    private int index = 0;

    public float wordSpeed;
    
    private float timer;
    private bool timerDone;
    private bool timerOn = false;

    private bool startDialuoge = false;
    private bool canDiolouge = false;
    
    //public bool canGoToNextLine = true;


    void Start()
    {
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Count seconds
            
        if (timer > 2 && !timerDone)
        {
            timerDone = true;
            startDialuoge = true;
        }
        
        
        
        if (startDialuoge || Input.GetKeyDown(KeyCode.E) && startDialuoge == false)
        {
            startDialuoge = false;
            if (!dialoguePanel.activeInHierarchy)
            {
                _inputManager.canMove = false;
                _playerMovement.canMove = false;
                
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
        _inputManager.canMove = true;
        _playerMovement.canMove = true;
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