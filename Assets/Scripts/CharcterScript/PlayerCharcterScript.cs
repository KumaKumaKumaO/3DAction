using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�ɃA�^�b�`����N���X
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
