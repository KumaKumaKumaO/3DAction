using UnityEngine.UI;
using UnityEngine;

public class HpBarViewScript : BaseViewScript
{
	[SerializeField]
	private Image _hpBar = default;
	[SerializeField]
	private Image _backgroundHPBar = default;
	public override void Display(float value)
	{
		_hpBar.fillAmount = value;
		_backgroundHPBar.fillAmount = 1 - value;
	}
}
