/// <summary>
/// UIで使うデータを制御する
/// </summary>
public class BaseBar
{
	private float _barValue = default;
	private float _barMaxValue = default;
	/// <summary>
	/// 値を変更する
	/// </summary>
	/// <param name="value"></param>
	public virtual void ChangeBarValue(float value)
	{

	}
	/// <summary>
	/// 最大値を変更する
	/// </summary>
	/// <param name="value"></param>
	public virtual void ChangeBarMaxValue(float value)
	{

	}
}