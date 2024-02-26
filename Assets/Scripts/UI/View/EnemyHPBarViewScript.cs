using UnityEngine.UI;
using UnityEngine;

public class EnemyHPBarViewScript : BaseViewScript
{
	[SerializeField]
	private Image _hpBar = default;
	[SerializeField]
	private Image _backgroundHPBar = default;
	private Transform _targetTransform = default;
	private Transform _myTransform = default;
	private Vector3 _offset = default;
	public void Init(Transform transform,Vector3 offsetVector)
	{
		_offset = offsetVector;
		_myTransform = this.transform;
		_targetTransform = transform;
	}
	public void UpdatePos()
	{
		_myTransform.position
			= RectTransformUtility.WorldToScreenPoint(Camera.main, _targetTransform.position + _offset);
	}

	public override void Display(float value)
	{
		if(_targetTransform is null) 
		{
			ErrorManagerScript.MyInstance.NullSettingError("targetTransform");
			return;
		}

		_hpBar.fillAmount = value;
		_backgroundHPBar.fillAmount = 1 - value;

	}
}
