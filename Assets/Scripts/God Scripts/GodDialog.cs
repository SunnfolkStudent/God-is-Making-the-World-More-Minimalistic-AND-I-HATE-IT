
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using UnityEngine.Playables;

public class GodDialog : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject GodInteractPromt;
    public GameObject God;

    public InputManager _inputManager;
    public PlayerMovement _playerMovement;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    public string[] dialogue;
    public string[] name;

    private int index = 0;

    public AudioSource _audioSource;
    public AudioClip[] godClips;
    public AudioClip[] minimalistGodClips;
    public AudioClip[] joeClips;

    public float wordSpeed;
    public bool playerIsClose;
    //public bool canGoToNextLine = true;

    public PlayableDirector playableDirector;

    public GameObject dialogueCamPos;
    public Transform tFollowTarget;
    public CinemachineVirtualCamera vcam;
    
    
    void Start()
    {
        dialogueText.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            
            tFollowTarget = dialogueCamPos.transform; 
            vcam.LookAt = tFollowTarget; 
            vcam.Follow = tFollowTarget;
            vcam.m_Lens.OrthographicSize = 5f;
            
            if (!dialoguePanel.activeInHierarchy)
            {
                if (index == 0)
                {
                    God.SetActive(true); 
                    playableDirector.Play(); 
                }
                
                _inputManager.canMove = false; 
                _playerMovement.canMove = false; 
                dialoguePanel.SetActive(true); 
                GodInteractPromt.SetActive(false);
                
                dialogueText.text = ""; 
                nameText.text = name[index]; 
                StartCoroutine(Typing());
                
            }
            else if (dialogueText.text == dialogue[index])
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            GodInteractPromt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            RemoveText();
            GodInteractPromt.SetActive(false);
        }
    }
}