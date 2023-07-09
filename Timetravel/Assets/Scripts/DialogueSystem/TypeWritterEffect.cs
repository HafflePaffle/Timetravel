using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWritterEffect : MonoBehaviour
{
	[SerializeField]private float typeWritterSpeed = 5;

	public Coroutine Run(string textToType, TMP_Text textLabel)
	{
		return StartCoroutine(TypeText(textToType, textLabel));
	}

	private IEnumerator TypeText(string textToType, TMP_Text textLabel)
	{
		textLabel.text = string.Empty;

		float t = 0;
		int CharIndex = 0;

		while (CharIndex < textToType.Length)
		{
			t += Time.deltaTime * typeWritterSpeed;
			CharIndex = Mathf.FloorToInt(t);
			CharIndex = Mathf.Clamp(CharIndex, 0, textToType.Length);

			textLabel.text = textToType.Substring(0, CharIndex);

			yield return null;
		}

		textLabel.text = textToType;

	}

		
}
