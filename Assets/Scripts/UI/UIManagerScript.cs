using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class UIManagerScript:MonoBehaviour
{
	[SerializeField]
	private PlayerHpBarPresenterScript _playerHpBarPresenterScript = default;

	public void PlayerUIInit(CharcterStatus status)
	{
		_playerHpBarPresenterScript.HpBarInit(status);
	}
}
