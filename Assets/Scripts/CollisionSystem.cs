using UnityEngine;

/// <summary>
/// �����蔻��̃��W�b�N
/// </summary>
public class CollisionSystem
{
	/// <summary>
	/// �ǂ��̕������Փ˂��Ă��邩�ǂ���
	/// </summary>
	/// <param name="myAreaData">���M�̓����蔻��f�[�^</param>
	/// <param name="targetData">�Ώۂ̃I�u�W�F�N�g�f�[�^</param>
	/// <returns></returns>
	public CollisionResultData GetCollisionResult(CollisionAreaData myAreaData, BaseObjectScript targetData)
	{
		Vector3 hitDistanceVector = myAreaData.HalfAreaSize + targetData.MyCollisionAreaData.HalfAreaSize;
		Vector3 targetToMeVector = (myAreaData.MyTransform.position + myAreaData.Offset)
			- (targetData.MyCollisionAreaData.MyTransform.position + targetData.MyCollisionAreaData.Offset);

		bool isTopTemp = IsCollision(targetToMeVector + myAreaData.MyTransform.up * (myAreaData.HalfAreaSize.y + myAreaData.HalfAreaWidth)
			, hitDistanceVector + myAreaData.MyTransform.up * -(myAreaData.HalfAreaSize.y - myAreaData.HalfAreaWidth));

		bool isBottomTemp = IsCollision(targetToMeVector + -myAreaData.MyTransform.up * (myAreaData.HalfAreaSize.y + myAreaData.HalfAreaWidth)
			, hitDistanceVector + myAreaData.MyTransform.up * -(myAreaData.HalfAreaSize.y - myAreaData.HalfAreaWidth));

		bool isRightTemp = IsCollision(targetToMeVector + myAreaData.MyTransform.right * (myAreaData.HalfAreaSize.x + myAreaData.HalfAreaWidth)
			, hitDistanceVector + myAreaData.MyTransform.right * -(myAreaData.HalfAreaSize.x - myAreaData.HalfAreaWidth));

		bool isLeftTemp = IsCollision(targetToMeVector + -myAreaData.MyTransform.right * (myAreaData.HalfAreaSize.x + myAreaData.HalfAreaWidth)
			, hitDistanceVector + myAreaData.MyTransform.right * -(myAreaData.HalfAreaSize.x - myAreaData.HalfAreaWidth));

		bool isForwardTemp = IsCollision(targetToMeVector + myAreaData.MyTransform.forward * (myAreaData.HalfAreaSize.z + myAreaData.HalfAreaWidth)
			, hitDistanceVector + myAreaData.MyTransform.forward * -(myAreaData.HalfAreaSize.z - myAreaData.HalfAreaWidth));

		bool isBackTemp = IsCollision(targetToMeVector + -myAreaData.MyTransform.forward * (myAreaData.HalfAreaSize.z + myAreaData.HalfAreaWidth)
			, hitDistanceVector + myAreaData.MyTransform.forward * -(myAreaData.HalfAreaSize.z - myAreaData.HalfAreaWidth));

		return new CollisionResultData(isRightTemp, isLeftTemp, isTopTemp, isBottomTemp, isForwardTemp, isBackTemp, targetData);
	}
	private bool IsCollision(Vector3 myColDistance, Vector3 targetColDistance)
	{	
		return Mathf.Abs(myColDistance.x) < targetColDistance.x
			&& Mathf.Abs(myColDistance.y) < targetColDistance.y
			&& Mathf.Abs(myColDistance.z) < targetColDistance.z;
	}
}