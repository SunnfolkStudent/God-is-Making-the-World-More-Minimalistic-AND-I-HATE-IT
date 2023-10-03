using UnityEngine;
using UnityEngine.UI;

public class GodHealtBar : MonoBehaviour
{
    public GodMovment godHealth;
    public Image[] hearts;

    public void Update()
    {

        for (int i = 0; i < hearts.Length; i++)
        {

            hearts[i].color = i < godHealth.health ? new Color(1, 0, 0, 1) : new Color(1, 1, 1, 0.1f);
        }
    }
}