using UnityEngine;

/// <summary>
/// ビューの基底クラス
/// </summary>
public abstract class BaseViewScript : MonoBehaviour
{
	/// <summary>
	/// 表示する
	/// </summary>
	/// <param name="value">正規化された値</param>
	public abstract void Display(float value);
}
