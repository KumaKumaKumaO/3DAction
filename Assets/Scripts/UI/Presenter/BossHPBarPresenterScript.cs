using UniRx;
using UnityEngine;

/// <summary>
/// ボスのHPのプレゼンター
/// </summary>
public class BossHPBarPresenterScript : MonoBehaviour
{
	[SerializeField]
	private HpBarViewScript _viewScript = default;
	
	/// <summary>
	/// 初期化
	/// </summary>
	/// <param name="characterScript">キャラクタースクリプト</param>
	public void BossHPBarInit(BaseCharacterScript characterScript)
	{
		_viewScript.Init();
		characterScript.MyCharcterStatus.Hp
			.Select(value => value = value / characterScript.MyCharcterStatus.MaxHP)
			.Where(value => !float.IsNaN(value))
			.Subscribe(value => _viewScript.Display(value)).AddTo(characterScript);
	}
}
