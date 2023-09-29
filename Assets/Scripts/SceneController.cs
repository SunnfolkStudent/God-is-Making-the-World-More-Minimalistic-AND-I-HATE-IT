using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
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
