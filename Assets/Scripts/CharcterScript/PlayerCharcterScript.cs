using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーにアタッチするクラス
/// </summary>
public class PlayerCharcterScript : BaseCharcterScript
{
	private void Start()
	{
		GetComponent<Renderer>().material.color = Color.black;
	}
	public void SetPlayerInput(IInputCharcterAction input)
	{
		_myInput = input;
	}
	public override void Init()
	{
		base.Init();
	}
}
