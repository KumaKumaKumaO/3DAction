using UnityEngine;

public class UIManagerScript : MonoBehaviour
{
	[SerializeField]
	private PlayerHpBarPresenterScript _playerHpBarPresenterScript = default;
	[SerializeField]
	private PlayerStaminaBarPresenterScript _playerStaminaBarPresenterScript = default;
	[SerializeField]
	private BossHPBarPresenterScript _bossHPBarPresenterScript = default;
	[SerializeField]
	private EnemyHpBarPresenterScript _enemyHpBarPresenter = default;

	public void PlayerUIInit(BaseCharacterScript script)
	{
		_playerHpBarPresenterScript.PlayerHPBarInit(script);
		_playerStaminaBarPresenterScript.StaminaBarInit(script);
	}

	public void BossUIInit(BaseCharacterScript script)
	{
		_bossHPBarPresenterScript.BossHPBarInit(script);
	}


	public void EnemyUIInit(BaseCharacterScript script)
	{
		_enemyHpBarPresenter.HPBarInit(script);
	}
}
