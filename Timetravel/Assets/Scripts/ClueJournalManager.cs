using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClueJournalManager : MonoBehaviour
{
    [SerializeField] private DialogueObject dialogueOne;
    [SerializeField] private TextMeshProUGUI clueOne;
    public GameObject journal;
    [SerializeField] private Timer timer;
    [SerializeField] private PauseMenu pauseMenu;
    public bool journalActive = false;

    // Start is called before the first frame update
    void Start()
    {
        journal.SetActive(false);
        clueOne.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueOne.hasBeenSaid == true)
        {
            clueOne.enabled = true;
        }

        if(Input.GetKeyDown(KeyCode.Tab) )
        {
            if(journalActive == false)
            {
                journalActive = true;
                pauseMenu.Pause();
                journal.SetActive(true);
            }
            else
            {
                journalActive = false;
                pauseMenu.Resume();
                journal.SetActive(false);
            }
            
        }

    }
}
