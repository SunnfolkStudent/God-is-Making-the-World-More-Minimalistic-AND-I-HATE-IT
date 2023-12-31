
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

public class CutseneDialog : MonoBehaviour
{
    [Header("Dialogue")]
    public GameObject canvs;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public string[] dialogue; 
    public string[] name;
        
    //[Header("Player Input & Movement")]
    //public InputManager _inputManager;
    //public PlayerMovement _playerMovement;
    
    private int index = 0;

    [Header("Audio")]
    public AudioSource _audioSource;
    public AudioClip[] joeClips;

    [Header("Word Speed & Disable Movement")]
    public float wordSpeed;
    //public bool canGoToNextLine = true;

    [Header("Animation & Camera")]
    public PlayableDirector playableDirector;
    
    private float timer;
    private bool timerDone;
    private bool timerOn = false;

    public bool activated = true;

    void Start()
    {
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
        print("timerOn");
        timer += Time.deltaTime;
        if (timer > 11f) 
        {
            print("IwantTOleeve"); 
            SceneManager.LoadScene("LevelOne");
        }
        
        
        if (activated)
        {
            activated = false;
            if (!canvs.activeInHierarchy)
            {
                if (index == 0)
                {
                    //playableDirector.Stop();
                }
                

                canvs.SetActive(true); 
                
                dialogueText.text = ""; 
                nameText.text = name[index];
                _audioSource.volume = 0.5f;
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
        _audioSource.volume = 1f;
        canvs.SetActive(false);
        playableDirector.Resume();
        timerOn = true;
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
        if (nameText.text == "Joe") { _audioSource.PlayOneShot(joeClips[Random.Range(0, joeClips.Length)]); }
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