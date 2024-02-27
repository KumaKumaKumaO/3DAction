using UnityEngine;
using UniRx;

public class PlayerStaminaBarPresenterScript : MonoBehaviour
{
	[SerializeField]
	private PlayerStaminaBarViewScript _viewScript = default;
	public void StaminaBarInit(BaseCharacterScript script)
	{
		script.MyCharcterStatus.Stamina
			.Select(value => value = value / script.MyCharcterStatus.MaxStamina)
			.Where(value => !float.IsNaN(value))
			.Subscribe(value => _viewScript.Display(value)).AddTo(script);
	}
}
