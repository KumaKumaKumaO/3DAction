using UnityEngine;
using UniRx;

public class PlayerStaminaBarPresenterScript : MonoBehaviour
{
	[SerializeField]
	private PlayerStaminaBarViewScript _viewScript = default;
	public void StaminaBarInit(CharcterStatus status)
	{
		status.Stamina
			.Select(value => value = value / status.MaxStamina)
			.Where(value => !float.IsNaN(value))
			.Subscribe(value => _viewScript.Display(value)).AddTo(this);
	}
}
