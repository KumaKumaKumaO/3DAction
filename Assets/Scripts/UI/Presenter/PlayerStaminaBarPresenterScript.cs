using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーのスタミナのプレゼンター
/// </summary>
public class PlayerStaminaBarPresenterScript : MonoBehaviour
{
	[SerializeField]
	private PlayerStaminaBarViewScript _viewScript = default;

	/// <summary>
	/// 初期化
	/// </summary>
	/// <param name="characterScript">キャラクタースクリプト</param>
	public void StaminaBarInit(BaseCharacterScript characterScript)
	{
		characterScript.MyCharcterStatus.Stamina
			.Select(value => value = value / characterScript.MyCharcterStatus.MaxStamina)
			.Where(value => !float.IsNaN(value))
			.Subscribe(value => _viewScript.Display(value)).AddTo(characterScript);
	}
}
