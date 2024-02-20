using UnityEngine;

/// <summary>
/// �����蔻��̃��W�b�N
/// </summary>
public class CollisionSystem
{
	public bool IsCollision(CollisionAreaData myAreaData, CollisionAreaData targetData, MoveDirection moveDirection)
	{
		Vector3 hitDistanceVector = myAreaData.HalfAreaSize + targetData.HalfAreaSize;
		Vector3 targetToMeVector = (myAreaData.MyTransform.position + myAreaData.Offset)
			- (targetData.MyTransform.position + targetData.Offset);

		if (moveDirection == MoveDirection.Forward)
		{
			return IsCollision(targetToMeVector
			+ myAreaData.MyTransform.forward * (myAreaData.AreaWidth + myAreaData.Offset.z)
			, hitDistanceVector);
		}
		else if (moveDirection == MoveDirection.Back)
		{
			return IsCollision(targetToMeVector
			+ -myAreaData.MyTransform.forward * (myAreaData.AreaWidth + myAreaData.Offset.z)
			, hitDistanceVector);
		}
		else if (moveDirection == MoveDirection.Up)
		{
			return IsCollision(targetToMeVector
			+ myAreaData.MyTransform.up * (myAreaData.AreaWidth + myAreaData.Offset.y)
			, hitDistanceVector);
		}
		else if (moveDirection == MoveDirection.Down)
		{
			return IsCollision(targetToMeVector
			+ -myAreaData.MyTransform.up * (myAreaData.AreaWidth + myAreaData.Offset.y)
			, hitDistanceVector);
		}
		else if (moveDirection == MoveDirection.Right)
		{
			return IsCollision(targetToMeVector
			+ myAreaData.MyTransform.right * (myAreaData.AreaWidth + myAreaData.Offset.x)
			, hitDistanceVector);
		}
		//��
		else
		{
			return IsCollision(targetToMeVector
			+ -myAreaData.MyTransform.right * (myAreaData.AreaWidth + myAreaData.Offset.x)
			, hitDistanceVector);
		}
	}
	/// <summary>
	/// �d�Ȃ��Ă��邩
	/// </summary>
	/// <param name="myAreaData"></param>
	/// <param name="targetData"></param>
	/// <returns></returns>
	public bool IsCollision(CollisionAreaData myAreaData, CollisionAreaData targetData)
	{
		Vector3 hitDistanceVector = myAreaData.HalfAreaSize + targetData.HalfAreaSize;
		Vector3 targetToMeVector = (myAreaData.MyTransform.position + myAreaData.Offset)
			- (targetData.MyTransform.position + targetData.Offset);
		return IsCollision(targetToMeVector, hitDistanceVector);
	}

	private bool IsCollision(Vector3 myColDistance, Vector3 targetColDistance)
	{
		return Mathf.Abs(myColDistance.x) < targetColDistance.x
			&& Mathf.Abs(myColDistance.y) < targetColDistance.y
			&& Mathf.Abs(myColDistance.z) < targetColDistance.z;
	}
}