using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class creditscontroler : MonoBehaviour
{
    private float timer;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Count seconds
            
        if (timer > 15)
        { 
            SceneManager.LoadScene("Title");
        }
    }
}
