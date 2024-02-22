using UniRx;

using UnityEngine;
public class PlayerHpBarPresenterScript : MonoBehaviour
{
	[SerializeField]
	private PlayerHpBarViewScript _viewScript = default;
	public void HpBarInit(CharcterStatus status)
	{
		status.Hp
			.Select(value => value = value / status.MaxHP)
			.Where(value => !float.IsNaN(value))
			.Subscribe(value => _viewScript.Display(value)).AddTo(this);
	}

}
