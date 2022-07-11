using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LobbyManager : MonoBehaviourPunCallbacks
{
	private void Awake()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
		PhotonNetwork.GameVersion = "0.0.1";
		PhotonNetwork.ConnectUsingSettings();
	}

	public void CreateRoom()
	{
		var options = new Photon.Realtime.RoomOptions
		{
			MaxPlayers = 2
		};
		PhotonNetwork.CreateRoom(null, options);
	}

	public void JoinRandomRoom()
	{
		PhotonNetwork.JoinRandomRoom();
	}
	public override void OnConnectedToMaster()
	{
		Debug.Log("OnConnectedToMaster");
	}
	public override void OnJoinedRoom()
	{
		//Console.Logger.Instance.AddLog("Joined the room");

		PhotonNetwork.LoadLevel("_Game");
	}

	public override void OnCreatedRoom()
	{
		
	}
}
