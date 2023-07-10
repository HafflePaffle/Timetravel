using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
	[SerializeField] private TMP_Text textLabel;
	[SerializeField] private GameObject DialogueBox;
	public GameObject playerSprite;
	private ResponseHandler responseHandler;
	private TypeWritterEffect typeWritterEffect;
	public bool IsOpen{get; private set;}
	[SerializeField] private Timer timer;
	public TMP_Text actor;
    public GameObject portrait;
	public GameObject detectivePortrait;
    private string currentSpeaker;
	private Sprite currentPortrait;


	void Start()
	{

		typeWritterEffect = GetComponent<TypeWritterEffect>();
		responseHandler = GetComponent<ResponseHandler>();
		portrait.SetActive(false);

        CloseDialogueBox();

    }

	public void ShowDialogue(DialogueObject dialogueObject)
	{
		playerSprite.SetActive(true);
        portrait.SetActive(true);
        IsOpen = true;
		DialogueBox.SetActive(true);
		StartCoroutine(StepThroughDialogue(dialogueObject));
		dialogueObject.hasBeenSaid = true;
		dialogueObject.saidInTime = true;
		dialogueObject.wasSaidAt.Clear();
		dialogueObject.wasSaidAt.Insert(0, timer.levelTimer);

		if(dialogueObject.Name != null)
		{
            actor.text = dialogueObject.Name;
        }
		
		if(dialogueObject.portrait != null)
		{
            portrait.transform.GetComponent<UnityEngine.UI.Image>().sprite = dialogueObject.portrait;
        }

        if (dialogueObject.detectivePortrait != null)
        {
            detectivePortrait.transform.GetComponent<UnityEngine.UI.Image>().sprite = dialogueObject.detectivePortrait;
        }


    }

	private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
	{

		for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
		{
			string dialogue = dialogueObject.Dialogue[i];
			yield return typeWritterEffect.Run(dialogue, textLabel);

			if(i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

			yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
		}

		if(dialogueObject.HasResponses)
		{
			responseHandler.ShowResponses(dialogueObject.Responses);
		}
		else
		{
		CloseDialogueBox();
		}


	}

	private void CloseDialogueBox()
	{
        playerSprite.SetActive(false);
        IsOpen = false;
		timer.updateTimer = true;
		DialogueBox.SetActive(false);
		textLabel.text = string.Empty;
        portrait.SetActive(false);
    }



}
