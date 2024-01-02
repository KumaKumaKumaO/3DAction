using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シングルトンクラス
/// ゲーム全体をコントロールする
/// </summary>
public class GameManagerScript : MonoBehaviour
{
	private static GameManagerScript _instanceGameManager = default;
	private GameStateMachineScript _myStateMachine = default;
	private BaseGameStateScript _nowState = default;
	public static GameManagerScript GameManager
	{
		get { return _instanceGameManager; }
	}
	private void Start()
	{
		if (_instanceGameManager != null)
		{
			_instanceGameManager = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		_nowState = _myStateMachine.UpdateState();
		_nowState.Execute();
	}
}