using System;
using UnityEngine;
using UnityEngine.Playables;

public class MainMenuFadeOut : MonoBehaviour
{
    public SceneController _sceneController;
    
    private float timer;
    private bool timerOn = false;

    public PlayableDirector playebleDirector;

    private void Start()
    {
        _sceneController = GetComponent<SceneController>();
    }
    // Start is called before the first frame update

    private void Update()
    {
        if (timerOn = true)
        {
            timer += Time.deltaTime;
            if (timer > 1.5f) 
            {
                _sceneController.LoadSceneByName("IntroCutscene");
            }
        }
        
    }

    public void ButonPresed()
    {
        timerOn = true;
        playebleDirector.Play();
    }
    
}
