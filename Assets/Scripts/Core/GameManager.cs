using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public PhotonView PV;
	public static GameManager Instance;
	[SerializeField] private List<Player> _players = new List<Player>();
	public Player CurrentPlayer;
	[SerializeField] private Sprite _X;
	[SerializeField] private Sprite _O;
	[SerializeField] private List<Cell> cells = new List<Cell>();
	[SerializeField] private PanelInfo _panelInfo;
	public bool _isGame = false;

	private int[,] variantsCells = new int[8, 3]
	{
		{0,1,2 },
		{3,4,5 },
		{6,7,8 },
		{0,3,6 },
		{1,4,7 },
		{2,5,8 },
		{0,4,8 },
		{2,4,6 },
	};
	private void Awake()
	{
		Instance = this;
	}
	public void AddPlayer(Player player)
	{
		_players.Add(player);
		CheckCountPlayersToRound();
	}

	public void CheckCells()
	{
		for (int i = 0; i < 8; i++)
		{
			var tempCells = new List<Cell>();
			for (int j = 0; j < 3; j++)
			{
				tempCells.Add(cells[variantsCells[i, j]]);
			}
			bool isCheck = false;
			foreach (var cell in tempCells)
			{
				if (cell.playerBusy == null)
				{
					isCheck = true;
					break;
				}
			}
			if (isCheck) continue;

			if(tempCells[0].playerBusy.GetNickName() == tempCells[1].playerBusy.GetNickName() && tempCells[1].playerBusy.GetNickName() == tempCells[2].playerBusy.GetNickName())
			{
				_isGame = false;
				foreach (var cell in tempCells)
				{
					cell.SetColor();
				}
				_panelInfo.gameObject.SetActive(true);
				if (tempCells[0].playerBusy._photonView.IsMine)
				{
					_panelInfo.SetStatus("ВЫ ВЫИГРАЛИ", Color.green);
				}
				else
				{
					_panelInfo.SetStatus("ВЫ ПРОИГРАЛИ", Color.red);
				}
				return;
			}
		}
	}
	public void SwitchPlayer()
	{
		for (int i = 0; i < _players.Count; i++)
		{
			if (_players[i] != CurrentPlayer)
			{
				PV.RPC("SetPlayer", RpcTarget.All, i);
				break;
			}
		}
	}
	private void CheckCountPlayersToRound()
	{
		if (_players.Count == 2)
		{
			_isGame = true;
			if (!PV.IsMine)
			{
				var temp = _players[0];
				var temp2 = _players[1];
				_players = new List<Player>();
				_players.Add(temp2);
				_players.Add(temp);
			}
			_players[0].SetSprite(_X);
			_players[1].SetSprite(_O);
			Debug.Log("Игра началась!");
			PV.RPC("SetPlayer", RpcTarget.All, 1);
		}
		else
		{
			Debug.Log("Ждем когда подклчюится соперник!");
		}
	}

	[PunRPC]
	public void SetPlayer(int id)
	{
		CurrentPlayer = _players[id];
	}
}
