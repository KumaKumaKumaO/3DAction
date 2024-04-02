using UnityEngine;

/// <summary>
/// UIを管理するクラス
/// </summary>
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

	/// <summary>
	/// プレイヤーUIの初期化
	/// </summary>
	/// <param name="characterScript">プレイヤーのキャラクタースクリプト</param>
	public void PlayerUIInit(BaseCharacterScript characterScript)
	{
		_playerHpBarPresenterScript.PlayerHPBarInit(characterScript);
		_playerStaminaBarPresenterScript.StaminaBarInit(characterScript);
	}

	/// <summary>
	/// ボスのUIの初期化
	/// </summary>
	/// <param name="characterScript">ボスのキャラクタースクリプト</param>
	public void BossUIInit(BaseCharacterScript characterScript)
	{
		_bossHPBarPresenterScript.BossHPBarInit(characterScript);
	}

	/// <summary>
	/// 敵のUIの初期化
	/// </summary>
	/// <param name="characterScript">キャラクタースクリプト</param>
	public void EnemyUIInit(BaseCharacterScript characterScript)
	{
		_enemyHpBarPresenter.HPBarInit(characterScript);
	}
}
