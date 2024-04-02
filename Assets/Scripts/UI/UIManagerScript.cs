using UnityEngine;

/// <summary>
/// UI���Ǘ�����N���X
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
	/// �v���C���[UI�̏�����
	/// </summary>
	/// <param name="characterScript">�v���C���[�̃L�����N�^�[�X�N���v�g</param>
	public void PlayerUIInit(BaseCharacterScript characterScript)
	{
		_playerHpBarPresenterScript.PlayerHPBarInit(characterScript);
		_playerStaminaBarPresenterScript.StaminaBarInit(characterScript);
	}

	/// <summary>
	/// �{�X��UI�̏�����
	/// </summary>
	/// <param name="characterScript">�{�X�̃L�����N�^�[�X�N���v�g</param>
	public void BossUIInit(BaseCharacterScript characterScript)
	{
		_bossHPBarPresenterScript.BossHPBarInit(characterScript);
	}

	/// <summary>
	/// �G��UI�̏�����
	/// </summary>
	/// <param name="characterScript">�L�����N�^�[�X�N���v�g</param>
	public void EnemyUIInit(BaseCharacterScript characterScript)
	{
		_enemyHpBarPresenter.HPBarInit(characterScript);
	}
}
