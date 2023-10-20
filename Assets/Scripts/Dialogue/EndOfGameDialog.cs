
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EndOfGameDialog : MonoBehaviour
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
    
    public int index = 0;

    [Header("Audio")]
    public AudioSource _audioSource;
    public AudioClip[] koeClips;

    [Header("Word Speed & Disable Movement")]
    public float wordSpeed;
    //public bool canGoToNextLine = true;

    [Header("Animation & Camera")]
    public PlayableDirector playableDirector;
    
    private float timer;
    private bool timerDone;
    private bool timerOn = false;

    private float timer2;
    private bool timerDone2;
    private bool timerOn2 = false;
    
    private float timer3;
    private bool timerDone3;
    private bool timerOn3 = false;
    
    
    public bool activated = true;

    void Start()
    {
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 18f) 
        {
            SceneManager.LoadScene("Credits");
        }

        if (timerOn2)
        {
             timer2 += Time.deltaTime;
             if (timer2 > 3f)
             {
                 timerDone2 = true;
                 timer2 = 0;
             }
        }

        if (timerOn3)
        {
            timer3 += Time.deltaTime;
            if (timer3 > 15f) 
            {
                dialogueText.text = "";
                index = 0;
                canvs.SetActive(false);
                playableDirector.Resume();
                timerOn = true;
            }
        }
        if (dialogueText.text == dialogue[index])
        {
            
            timerOn2 = true;
            if (timerDone2)
            {
                NextLine();
            }
        } 
        if (activated)
        {
            activated = false;
            if (!canvs.activeInHierarchy)
            {
                print("twest1");
                if (index == 0)
                {
                    //playableDirector.Stop();
                    print("test3");
                }
                

                canvs.SetActive(true); 
                
                dialogueText.text = ""; 
                nameText.text = name[index]; 
                StartCoroutine(Typing());
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
        timerOn3 = true;
        /*if (timer3 > 15f)
        {
             dialogueText.text = "";
             index = 0;
             canvs.SetActive(false);
             playableDirector.Resume();
             timerOn = true;
        }
        */
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
        _audioSource.volume = 0.5f;
        _audioSource.PlayOneShot(koeClips[Random.Range(0, koeClips.Length)]);
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