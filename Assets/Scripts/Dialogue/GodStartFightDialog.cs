using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

public class GodStartFightDialog : MonoBehaviour
{
    public GameObject dialoguePanel;
    
    public InputCubeManager _inputCubeManager;
    public PlayerCubeMovement _playerCubeMovement;
    public GodMovment _godMovment;
    public PlayerShoot _playerShoot;
    
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    
    public string[] dialogue;
    public string[] name;
    
    private int index = 0;

    public float wordSpeed;
    public bool godHasDesended;

    public PlayableDirector playableDirector;
    //public bool canGoToNextLine = true;

    [Header("Audio")]
    public AudioSource _audioSource;
    public AudioClip[] godClips;
    public AudioClip[] minimalistGodClips;
    public AudioClip[] joeClips;
    public AudioSource backgroundMusic;
    
    private float timer;
    private bool timerDone;
    
    private float timerTwo;
    private bool timerDoneTwo;
    private bool timerTwoOn;

    private bool firstActivasion = true;
    void Start()
    {
        dialogueText.text = "";
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Count seconds

        if (timer > 6 && !timerDone)
        {
            timerDone = true;
            
            godHasDesended = true;
        }
        
        if (timerTwoOn)
        {
            timerTwo += Time.deltaTime;
            if (timerTwo > 5 && !timerDoneTwo)
            {
                timerDoneTwo = true;

                _godMovment.GodIsAlive = true;
                _playerShoot.canAttack = true;
                backgroundMusic.Play();
            }
        }
        
        
        if ((godHasDesended && Input.GetKeyDown(KeyCode.E)) || godHasDesended && firstActivasion)
        {
            print("pressed E");
            firstActivasion = false;
            if (!dialoguePanel.activeInHierarchy)
            {
                print("typing");
                _inputCubeManager.canMove = false;
                _playerCubeMovement.canMove = false;
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
               
            }
            else if (dialogueText.text != dialogue[index] || _inputCubeManager.interactPressed)
            {
                wordSpeed = 0; 
                _audioSource.volume = 0.3f;
                print("skip");
            } 
            else if  (dialogueText.text == dialogue[index] && Input.GetKeyDown(KeyCode.E))
            {
                NextLine(); 
                print("next line");
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
        playableDirector.Play();
        godHasDesended = false;
        _godMovment.canChekIfGodIsDead = true;
        timerTwoOn = true;
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            if (letter.ToString() != " ") { PlaySpeechClip();}

            if (char.IsPunctuation(letter)) { yield return new WaitForSeconds(wordSpeed * 6); }
            else { yield return new WaitForSeconds(wordSpeed); }
        }
    }

    private void PlaySpeechClip()
    {
        if (nameText.text == "God") { _audioSource.PlayOneShot(godClips[Random.Range(0, godClips.Length)]); }
        if (nameText.text == "Minimalist God") { _audioSource.PlayOneShot(minimalistGodClips[Random.Range(0, minimalistGodClips.Length)]); }
        else if (nameText.text == "Maximalist Joe") { _audioSource.PlayOneShot(joeClips[Random.Range(0, joeClips.Length)]); }
        return;
    }

    public void NextLine()
    {
        //canGoToNextLine = false;
        if (index < dialogue.Length - 1)
        {
            wordSpeed = 0.06f;
            _audioSource.volume = 1f;
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