using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class UIManagerScript:MonoBehaviour
{
	[SerializeField]
	private PlayerHpBarPresenterScript _playerHpBarPresenterScript = default;
	[SerializeField]
	private PlayerStaminaBarPresenterScript _playerStaminaBarPresenterScript = default;

	public void PlayerUIInit(CharcterStatus status)
	{
		_playerHpBarPresenterScript.HpBarInit(status);
		_playerStaminaBarPresenterScript.StaminaBarInit(status);
	}

}
