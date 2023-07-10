using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
	[SerializeField] [TextArea] private string[] dialogue;
	[SerializeField] private Response[] responses;
	public List<float> wasSaidAt;

	public string[] Dialogue => dialogue;

	public bool HasResponses =>  Responses != null && Responses.Length > 0;

	public Response[] Responses => responses;

	public bool hasBeenSaid = false;

	public bool saidInTime = false;

	public Sprite portrait;

	public Sprite detectivePortrait;

	public string Name;



	void OnEnable()
	{
		hasBeenSaid = false;
        saidInTime = false;
        wasSaidAt.Clear();
		wasSaidAt.Insert(0, 0);
	}
}
