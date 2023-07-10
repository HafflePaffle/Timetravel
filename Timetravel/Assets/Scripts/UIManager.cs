using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas UI;
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] private ClueJournalManager journal;


    void Start()
    {
        UI.gameObject.SetActive(true);
    }

 
    void Update()
    {
        if(dialogueUI.IsOpen == true)
        {
            UI.gameObject.SetActive(false);
        }
        else
        {
            UI.gameObject.SetActive(true);
        }

        if (journal.journalActive == true)
        {
            UI.gameObject.SetActive(false);
        }
        else
        {
            UI.gameObject.SetActive(true);
        }
    }
}
