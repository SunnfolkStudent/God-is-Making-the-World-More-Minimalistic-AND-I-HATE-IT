
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GodDialog : MonoBehaviour
{
    public GameObject dialougePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialouge;
    private int index;

    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;

    private void Update()
    {
        if (playerIsClose)
        {
            if (dialougePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialougePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if (dialogueText.text == dialouge[index])
        {
            contButton.SetActive(true);
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialougePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialouge[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);
        if (index < dialouge.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}
