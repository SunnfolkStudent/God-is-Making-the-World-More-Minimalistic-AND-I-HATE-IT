using System.Collections;
using UnityEngine;
using TMPro;


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

    [Header("Audio")]
    public AudioClip[] joeClip;
    public AudioSource _audioSource;
    
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
        
        
        
        if (startDialuoge || Input.GetKeyDown(KeyCode.E) && canDiolouge == true)
        {
            startDialuoge = false;
            canDiolouge = true;
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
        canDiolouge = false;
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            if (letter.ToString() != " ") { PlaySpeechClip();}

            if (char.IsPunctuation(letter)) { yield return new WaitForSeconds(wordSpeed * 6); }
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    private void PlaySpeechClip()
    {
        _audioSource.PlayOneShot(joeClip[Random.Range(0, joeClip.Length)]);
        return;
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