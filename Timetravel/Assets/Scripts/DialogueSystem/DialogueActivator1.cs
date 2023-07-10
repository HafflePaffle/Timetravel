using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator1 : MonoBehaviour, IInteractable
{
	[SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private DialogueObject dialogueObject2;
    [SerializeField] private DialogueObject dialogueObject3;
	public GameObject npcSprite;
	public DialogueUI dialogueUI;
	public Timer timer;
	public DialogueObject[] dialogues;
    public Sprite normalSprite;
    public Sprite interactableSprite;
    private SpriteRenderer spriteR;

	void Start()
	{
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }


    void Update()
	{
		foreach(DialogueObject dialogueObject in dialogues)
		{
			if(dialogueObject.hasBeenSaid && dialogueObject.saidInTime && dialogueObject.wasSaidAt[0] >= timer.levelTimer)
			{
				dialogueObject.saidInTime = false;
			}
		}
	}

    private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player") && other.TryGetComponent(out Player player))
		{
			player.Interactable = this;

            spriteR.sprite = interactableSprite;

        }
	}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            if (player.Interactable is DialogueActivator1 dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;

                spriteR.sprite = normalSprite;
            }
        }
    }


    public void Interact(Player player)
	{
		if(dialogueObject.hasBeenSaid == false)
		{

        player.DialogueUI.ShowDialogue(dialogueObject);

        }
        else if (dialogueObject.hasBeenSaid == true && dialogueObject.saidInTime == false)
        {
			player.DialogueUI.ShowDialogue(dialogueObject2);
        }
        else if (dialogueObject.hasBeenSaid == true && dialogueObject.saidInTime == true)
        {
            player.DialogueUI.ShowDialogue(dialogueObject3);

        }
	}
}
