using UnityEngine;

/// <summary>
/// �����蔻��̃��W�b�N
/// </summary>
public class CollisionSystem
{
	private Vector3 vector = default;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="targetA"></param>
	/// <param name="targetB"></param>
	/// <returns></returns>
	public bool IsCollision(CollisionData targetA, CollisionData targetB)
	{
		vector = (targetA.MyTransform.position + targetA.Offset + targetA.AreaHalfSize) 
			- (targetB.MyTransform.position + targetB.Offset + targetB.AreaHalfSize);

		return (AbsoluteProcess(vector.x) <= targetB.AreaHalfSize.x + targetA.AreaHalfSize.x
			&& AbsoluteProcess(vector.y) <= targetB.AreaHalfSize.y + targetA.AreaHalfSize.y
			&& AbsoluteProcess(vector.z) <= targetB.AreaHalfSize.z + targetA.AreaHalfSize.z);
	}

	/// <summary>
	/// �l�̐�Βl�����߂�
	/// </summary>
	/// <param name="value">��Βl�ɂ������l</param>
	/// <returns>����</returns>
	private float AbsoluteProcess(float value)
	{
		if (value < 0)
		{
			return value * -1;
		}
		return value;

	}
}