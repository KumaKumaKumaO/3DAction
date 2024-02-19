using UnityEngine;

/// <summary>
/// 当たり判定のロジック
/// </summary>
public class CollisionSystem
{
	/// <summary>
	/// どこの部分が衝突しているかどうか
	/// </summary>
	/// <param name="myAreaData">自信の当たり判定データ</param>
	/// <param name="targetData">対象のオブジェクトデータ</param>
	/// <returns></returns>
	public CollisionResultData GetCollisionResult(CollisionAreaData myAreaData, BaseObjectScript targetData)
	{
		Vector3 hitDistanceVector = myAreaData.HalfAreaSize + targetData.MyCollisionAreaData.HalfAreaSize;
		Vector3 targetToMeVector = (myAreaData.MyTransform.position + myAreaData.Offset)
			- (targetData.MyCollisionAreaData.MyTransform.position + targetData.MyCollisionAreaData.Offset);

		bool isOverlap = IsCollision(targetToMeVector, hitDistanceVector);

		bool isTopTemp = IsCollision(targetToMeVector 
			+ myAreaData.MyTransform.up * (myAreaData.AreaWidth + myAreaData.Offset.y)
			, hitDistanceVector );

		bool isBottomTemp = IsCollision(targetToMeVector 
			+ -myAreaData.MyTransform.up * (myAreaData.AreaWidth + myAreaData.Offset.y)
			, hitDistanceVector );

		bool isRightTemp = IsCollision(targetToMeVector 
			+ myAreaData.MyTransform.right * (myAreaData.AreaWidth + myAreaData.Offset.x)
			, hitDistanceVector);

		bool isLeftTemp = IsCollision(targetToMeVector
			+ -myAreaData.MyTransform.right * (myAreaData.AreaWidth + myAreaData.Offset.x)
			, hitDistanceVector );

		bool isForwardTemp = IsCollision(targetToMeVector 
			+ myAreaData.MyTransform.forward * (myAreaData.AreaWidth + myAreaData.Offset.z)
			, hitDistanceVector);

		bool isBackTemp = IsCollision(targetToMeVector 
			+ -myAreaData.MyTransform.forward * (myAreaData.AreaWidth + myAreaData.Offset.z)
			, hitDistanceVector);

		return new CollisionResultData(isRightTemp, isLeftTemp, isTopTemp, isBottomTemp, isForwardTemp, isBackTemp,isOverlap, targetData);
	}

	public CollisionResultData GetCollisionResult(CollisionAreaData myAreaData, BaseObjectScript
		targetData,Vector3 moveVector)
	{
		return GetCollisionResult(new CollisionAreaData(myAreaData.HalfAreaSize + moveVector / 2,myAreaData.Offset + moveVector / 2,myAreaData.MyTransform,myAreaData.AreaWidth), targetData);
	}


	private bool IsCollision(Vector3 myColDistance, Vector3 targetColDistance)
	{
		return Mathf.Abs(myColDistance.x) < targetColDistance.x
			&& Mathf.Abs(myColDistance.y) < targetColDistance.y
			&& Mathf.Abs(myColDistance.z) < targetColDistance.z;
	}
}