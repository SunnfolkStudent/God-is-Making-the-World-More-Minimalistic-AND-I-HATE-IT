
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static string lastSceneWeWereOn;
    private string _previousScene;

    private void Awake()
    {
        _previousScene = lastSceneWeWereOn;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "DeathScene") return;
        lastSceneWeWereOn = SceneManager.GetActiveScene().name;
    }

    public void LoadPreviousScene()
    {
        
        SceneManager.LoadScene(_previousScene);
    }

    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
        return;
    }

    public void QuitGame()
    {
        Application.Quit();
        return;
    }

    public void LoadSceneByNumber()
    {

        return;
    }
}
