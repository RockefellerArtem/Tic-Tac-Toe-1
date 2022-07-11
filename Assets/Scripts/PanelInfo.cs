using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelInfo : MonoBehaviour
{
	[SerializeField] private TMP_Text _textStatus;


	public void SetStatus(string message,Color color)
	{
		_textStatus.color = color;
		_textStatus.text = message;
	}
}
