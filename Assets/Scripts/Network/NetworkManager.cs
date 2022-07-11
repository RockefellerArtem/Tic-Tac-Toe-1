using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
	[SerializeField] private GameObject _playerPrefab;
	[SerializeField] private GameManager _gameManager;

	private NetworkFactory _factory;

	private void Awake()
	{
		_factory = new NetworkFactory();
		var player = _factory.Create(_playerPrefab.name);
		player.gameObject.name = $"Player [{player.GetNickName()}]";

	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}
	public override void OnLeftRoom()
	{
		SceneManager.LoadScene("_Lobby");
	}
}
