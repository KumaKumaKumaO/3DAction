using UnityEngine;
using UniRx;

/// <summary>
/// �v���C���[�̃X�^�~�i�̃v���[���^�[
/// </summary>
public class PlayerStaminaBarPresenterScript : MonoBehaviour
{
	[SerializeField]
	private PlayerStaminaBarViewScript _viewScript = default;

	/// <summary>
	/// ������
	/// </summary>
	/// <param name="characterScript">�L�����N�^�[�X�N���v�g</param>
	public void StaminaBarInit(BaseCharacterScript characterScript)
	{
		characterScript.MyCharcterStatus.Stamina
			.Select(value => value = value / characterScript.MyCharcterStatus.MaxStamina)
			.Where(value => !float.IsNaN(value))
			.Subscribe(value => _viewScript.Display(value)).AddTo(characterScript);
	}
}
