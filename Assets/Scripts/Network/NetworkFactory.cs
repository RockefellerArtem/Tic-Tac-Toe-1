using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkFactory
{
	public Player Create(string namePrefab)
	{
		GameObject playerGameObject = PhotonNetwork.Instantiate(namePrefab, Vector3.zero, Quaternion.identity);
		Player player = playerGameObject.GetComponent<Player>();
		return player;
	}
}
