using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// �v���C���[��HP�̃r���[
/// </summary>
public class HpBarViewScript : BaseViewScript
{
	[SerializeField]
	private Image _hpBar = default;
	[SerializeField]
	private Image _backgroundHPBar = default;

	/// <summary>
	/// ������
	/// </summary>
	public void Init()
	{
		gameObject.SetActive(true);
	}

	public override void Display(float value)
	{
		_hpBar.fillAmount = value;
		_backgroundHPBar.fillAmount = 1 - value;
	}
}
