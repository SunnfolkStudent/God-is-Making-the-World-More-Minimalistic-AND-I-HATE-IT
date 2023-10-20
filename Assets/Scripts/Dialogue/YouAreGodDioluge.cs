using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;


public class YouAreGodDioluge : MonoBehaviour
{
    public GameObject dialoguePanel;
    public PlayableDirector playableDirector;
    
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
            
        if (timer > 1.5f && !timerDone)
        {
            timerDone = true;
            startDialuoge = true;
        }
        
        
        
        if (startDialuoge || Input.GetKeyDown(KeyCode.E) && canDiolouge == true)
        {
            
            startDialuoge = false;
            canDiolouge = true;
            
            playableDirector.Pause();
            
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
            else if (dialogueText.text != dialogue[index] && Input.GetKeyDown(KeyCode.E))
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
        dialoguePanel.SetActive(false);
        canDiolouge = false;
        playableDirector.Resume();
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
    
}