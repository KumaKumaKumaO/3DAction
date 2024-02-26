using UniRx;
using UnityEngine;

public class BossHPBarPresenterScript : MonoBehaviour
{
	[SerializeField]
	private HpBarViewScript _viewScript = default;
	public void HPBarInit(CharcterStatus status)
	{
		status.Hp
			.Select(value => value = value / status.MaxHP)
			.Where(value => !float.IsNaN(value))
			.Subscribe(value => _viewScript.Display(value)).AddTo(this);
	}
}
