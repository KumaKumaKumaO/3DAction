using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// 敵のHPのビュー
/// </summary>
public class EnemyHPBarViewScript : BaseViewScript
{
	[SerializeField]
	private Image _hpBar = default;
	[SerializeField]
	private Image _backgroundHPBar = default;
	private Transform _targetTransform = default;
	private Transform _myTransform = default;
	private Vector3 _objectOffset = default;
	[SerializeField]
	private Vector3 _offset = default;

	/// <summary>
	/// 初期化
	/// </summary>
	/// <param name="transform">対象のTransform</param>
	/// <param name="offsetVector">表示位置のオフセット</param>
	public void Init(Transform transform,Vector3 offsetVector)
	{
		gameObject.SetActive(true);
		_objectOffset = offsetVector;
		_myTransform = this.transform;
		_targetTransform = transform;
		UpdatePos();
	}

	/// <summary>
	/// 位置を更新する
	/// </summary>
	public void UpdatePos()
	{
		if(_targetTransform is null) { return; }
		_myTransform.position
			= RectTransformUtility.WorldToScreenPoint(Camera.main
			, _targetTransform.position + _objectOffset + _offset);
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

	/// <summary>
	/// 隠す
	/// </summary>
	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
