using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �v���C���[�̃X�^�~�i�̃r���[
/// </summary>
public class PlayerStaminaBarViewScript : BaseViewScript
{
	[SerializeField]
	private Image _staminaBar = default;
	[SerializeField]
	private Image _staminaBackGroundBar = default;

	public override void Display(float value)
	{
		_staminaBar.fillAmount = value;
		_staminaBackGroundBar.fillAmount = 1 - value;
	}
}
