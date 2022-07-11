using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Player : MonoBehaviour
{
	public PhotonView _photonView;
	public Sprite _iconSpite;

	private void Start()
	{
		SetNickName("Player " + Random.Range(1000, 9999));
		GameManager.Instance.AddPlayer(this);
	}

	public void SetSprite(Sprite sprite)
	{
		_iconSpite = sprite;
	}

	public Sprite GetSprite()
	{
		return _iconSpite;
	}
	public string GetNickName()
	{
		return _photonView.Owner.NickName;
	}
	public void SetNickName(string nickName)
	{
		_photonView.Owner.NickName = nickName;
	}
}
