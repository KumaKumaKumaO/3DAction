using UniRx;
using UnityEngine;

/// <summary>
/// �v���C���[HP�̃v���[���^�[
/// </summary>
public class PlayerHpBarPresenterScript : MonoBehaviour
{
	[SerializeField]
	private HpBarViewScript _viewScript = default;

	/// <summary>
	/// ������
	/// </summary>
	/// <param name="characterScript">�L�����N�^�[�X�N���v�g</param>
	public void PlayerHPBarInit(BaseCharacterScript characterScript)
	{
		_viewScript.Init();
		characterScript.MyCharcterStatus.Hp
			.Select(value => value = value / characterScript.MyCharcterStatus.MaxHP)
			.Where(value => !float.IsNaN(value))
			.Subscribe(value => _viewScript.Display(value)).AddTo(characterScript);
	}
}
