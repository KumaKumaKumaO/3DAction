using UnityEngine.UI;
using UnityEngine;

public class PlayerHpBarViewScript : BaseViewScript
{
	[SerializeField]
	private Image _hpBar = default;
	[SerializeField]
	private Image _redHPBar = default;
	public override void Display(float value)
	{
		_hpBar.fillAmount = value;
		_redHPBar.fillAmount = 1 - value;
	}
}
