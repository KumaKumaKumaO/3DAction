using UnityEngine;

/// <summary>
/// 当たり判定のロジック
/// </summary>
public class CollisionSystem
{
	private Vector3 vector = default;

	/// <summary>
	/// 衝突しているかどうか
	/// </summary>
	/// <param name="targetA">当たり判定で使うデータ</param>
	/// <param name="targetB">当たり判定で使うデータ</param>
	/// <returns></returns>
	public bool IsCollision(CollisionData targetA, CollisionData targetB)
	{
		vector = (targetA.MyTransform.position + targetA.Offset) - (targetB.MyTransform.position + targetB.Offset);

		return (AbsoluteProcess(vector.x) <= targetB.HalfAreaSize.x + targetA.HalfAreaSize.x
			&& AbsoluteProcess(vector.y) <= targetB.HalfAreaSize.y + targetA.HalfAreaSize.y
			&& AbsoluteProcess(vector.z) <= targetB.HalfAreaSize.z + targetA.HalfAreaSize.z);
	}

	/// <summary>
	/// 値の絶対値を求める
	/// </summary>
	/// <param name="value">絶対値にしたい値</param>
	/// <returns>結果</returns>
	private float AbsoluteProcess(float value)
	{
		if (value < 0)
		{
			return value * -1;
		}
		return value;

	}
}