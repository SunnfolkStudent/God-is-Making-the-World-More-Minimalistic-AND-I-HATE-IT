
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GodDialog : MonoBehaviour
{
    [Header("Dialogue")]
    public GameObject dialoguePanel;
    public GameObject GodInteractPromt;
    public GameObject God;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public string[] dialogue; 
    public string[] name;
        
    [Header("Player Input & Movement")]
    public InputManager _inputManager;
    public PlayerMovement _playerMovement;
    
    private int index = 0;

    [Header("Audio")]
    public AudioSource _audioSource;
    public AudioClip[] godClips;
    public AudioClip[] minimalistGodClips;
    public AudioClip[] joeClips;
    public AudioClip harp;

    [Header("Word Speed & Disable Movement")]
    public float wordSpeed;
    public bool playerIsClose;
    //public bool canGoToNextLine = true;

    [Header("Animation & Camera")]
    public PlayableDirector playableDirector;

    public GameObject dialogueCamPos;
    public Transform tFollowTarget;
    public CinemachineVirtualCamera vcam;
    
    private float timer;
    private bool timerDone;
    private bool timerOn = false;

    private CinemachineImpulseSource _impulseSource;
    
    public PlayableDirector crushed;
    void Start()
    {
        dialogueText.text = "";
        _audioSource.volume = 0.5f;
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            timer += Time.deltaTime;
            if (timer > 1.5f && !timerDone)
            {
                if (SceneManager.GetActiveScene().name.Equals("HeavenScene")) { SceneManager.LoadScene("LevelTwo"); }
                if (SceneManager.GetActiveScene().name.Equals("HeavenSceneTwo")) { SceneManager.LoadScene("LevelThree"); }
                if (SceneManager.GetActiveScene().name.Equals("HeavenSceneThree")) { SceneManager.LoadScene("LevelFour"); }
            }
        }
        
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
                    _audioSource.PlayOneShot(harp);
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
            else if (!dialogueText.text.Equals(dialogue[index]) && Input.GetKeyDown(KeyCode.E))
            {
                wordSpeed = 0;
                _audioSource.volume = 0.3f;
            } 
            else if (dialogueText.text == dialogue[index] && Input.GetKeyDown(KeyCode.E)) { NextLine(); } 
            
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
        wordSpeed = 0.6f;
        dialoguePanel.SetActive(false);
        _inputManager.canMove = true;
        _playerMovement.canMove = true;
        crushed.Play();
        timerOn = true;
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            if (letter.ToString() != " ") { PlaySpeechClip();}
            
            if (nameText.text.Equals("Minimalist God") && !char.IsPunctuation(letter)) { CameraShakeManager.instance.CameraShake(_impulseSource); }
            if (SceneManager.GetActiveScene().name.Equals("HeavenScene") && index == 4 && !char.IsPunctuation(letter)) { CameraShakeManager.instance.CameraShake(_impulseSource); }
            
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
            _audioSource.volume = 0.5f;
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
            //RemoveText();
            GodInteractPromt.SetActive(false);
        }
    }
}