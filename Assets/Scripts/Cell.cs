using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
	[SerializeField] private PhotonView _pv;
	[SerializeField] private Image _imageIcon;
	[SerializeField] private Sprite _O;
	[SerializeField] private Sprite _X;
	[SerializeField] private int Id;
	public Player playerBusy;

	private void Awake()
	{
		_pv.ViewID = Id;
	}
	[PunRPC]
	private void SetIcon(string test)
	{
		_imageIcon.color = Color.white;
		_imageIcon.sprite = GameManager.Instance.CurrentPlayer.GetSprite();
		playerBusy = GameManager.Instance.CurrentPlayer;
		GameManager.Instance.CheckCells();
	}

	public void SetColor()
	{
		_imageIcon.color = Color.red;
	}

	public void OnClick()
	{
		if (GameManager.Instance.CurrentPlayer._photonView.IsMine && GameManager.Instance._isGame)
		{
			_pv.RPC("SetIcon", RpcTarget.All, "test");
			GameManager.Instance.SwitchPlayer();
		}
		
	}
}
