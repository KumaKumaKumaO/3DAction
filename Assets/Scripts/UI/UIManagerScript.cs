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

	private BaseCharacterScript _nowBossSetCharacter = default;
	private BaseCharacterScript _nowEnemySetCharcter = default;

	public BaseCharacterScript NowBossSetCharcterScript { get { return _nowBossSetCharacter; } }
	public BaseCharacterScript NowEnemySetCharacterScript { get { return _nowEnemySetCharcter; } }
	public void PlayerUIInit(CharcterStatus status)
	{
		_playerHpBarPresenterScript.HpBarInit(status);
		_playerStaminaBarPresenterScript.StaminaBarInit(status);
	}

	public void BossUIInit(BaseCharacterScript script)
	{
		_nowBossSetCharacter = script;
		_bossHPBarPresenterScript.HPBarInit(script.MyCharcterStatus);
	}

	public void EnemyUIInit(BaseCharacterScript script)
	{
		_nowEnemySetCharcter = script;
		_enemyHpBarPresenter.HPBarInit(script);
	}
}
