using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeCeneToEndCutSCene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("EndCutscene");
    }
    
}
