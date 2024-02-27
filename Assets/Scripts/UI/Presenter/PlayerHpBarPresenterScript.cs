using UniRx;
using UnityEngine;
public class PlayerHpBarPresenterScript : MonoBehaviour
{
	[SerializeField]
	private HpBarViewScript _viewScript = default;
	public void PlayerHPBarInit(BaseCharacterScript script)
	{
		_viewScript.Init();
		script.MyCharcterStatus.Hp
			.Select(value => value = value / script.MyCharcterStatus.MaxHP)
			.Where(value => !float.IsNaN(value))
			.Subscribe(value => _viewScript.Display(value)).AddTo(script);
	}
}
