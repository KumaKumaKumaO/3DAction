using UnityEngine;

/// <summary>
/// 当たり判定のロジック
/// </summary>
public class CollisionSystem
{
	private Vector3 vector = default;
	private Vector3 _hitDistanceVector = default;
	private bool isRightTemp;
	private bool isLeftTemp;
	private bool isTopTemp;
	private bool isBottomTemp;
	private bool isForwardTemp;
	private bool isBackTemp;

	/// <summary>
	/// どこの部分が衝突しているかどうか
	/// </summary>
	/// <param name="myAreaData">当たり判定で使うデータ</param>
	/// <param name="targetData">当たり判定で使うデータ</param>
	/// <returns></returns>
	public CollisionResultData GetCollisionResult(CollisionAreaData myAreaData, BaseObjectScript targetData)
	{
		_hitDistanceVector = myAreaData.HalfAreaSize + targetData.MyCollisionData.HalfAreaSize;
		vector = (myAreaData.MyTransform.position + myAreaData.Offset) - (targetData.MyCollisionData.MyTransform.position 
			+ targetData.MyCollisionData.Offset);

		isRightTemp = (vector.x < 0 &&vector.x >= -_hitDistanceVector.x);
		isLeftTemp = (vector.x > 0 &&vector.x <= _hitDistanceVector.x);
		isTopTemp = (vector.y < 0 && vector.y >= -_hitDistanceVector.y);
		isBottomTemp =  (vector.y > 0 && vector.y <= _hitDistanceVector.y);
		isForwardTemp  =  (vector.z < 0 && vector.z >= -_hitDistanceVector.z);
		isBackTemp =  (vector.z > 0 && vector.z <= _hitDistanceVector.z);

		return new CollisionResultData(isRightTemp,isLeftTemp,isTopTemp,isBottomTemp,isForwardTemp,isBottomTemp,targetData);
	}
}