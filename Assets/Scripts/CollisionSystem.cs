using UnityEngine;

/// <summary>
/// 当たり判定のロジック
/// </summary>
public class CollisionSystem
{
	private Vector3 _vector = default;
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
	/// <param name="myAreaData">自信の当たり判定データ</param>
	/// <param name="targetData">対象のオブジェクトデータ</param>
	/// <returns></returns>
	public CollisionResultData GetCollisionResult(CollisionAreaData myAreaData, BaseObjectScript targetData)
	{
		Init();
		_hitDistanceVector = myAreaData.HalfAreaSize + targetData.MyCollisionAreaData.HalfAreaSize;
		_vector = (myAreaData.MyTransform.position + myAreaData.Offset)
			- (targetData.MyCollisionAreaData.MyTransform.position + targetData.MyCollisionAreaData.Offset);

		isTopTemp = (myAreaData.TopYPos >= targetData.MyCollisionAreaData.BottomYPos
			&& myAreaData.TopYPos < targetData.MyCollisionAreaData.TopYPos && IsXCollision() && IsZCollision());

		isBottomTemp = (myAreaData.BottomYPos <= targetData.MyCollisionAreaData.TopYPos
			&& myAreaData.BottomYPos > targetData.MyCollisionAreaData.BottomYPos && IsXCollision() && IsZCollision());

		isRightTemp = (myAreaData.RightXPos >= targetData.MyCollisionAreaData.LeftXPos
			&& myAreaData.RightXPos < targetData.MyCollisionAreaData.RightXPos && IsYCollision(myAreaData, targetData.MyCollisionAreaData) && IsZCollision());

		isLeftTemp = (myAreaData.LeftXPos <= targetData.MyCollisionAreaData.RightXPos
			&& myAreaData.LeftXPos > targetData.MyCollisionAreaData.LeftXPos && IsYCollision() && IsZCollision());

		isForwardTemp = (myAreaData.ForwardZPos >= targetData.MyCollisionAreaData.BackZPos
			&& myAreaData.ForwardZPos < targetData.MyCollisionAreaData.ForwardZPos && IsYCollision() && IsXCollision());

		isBackTemp = (myAreaData.BackZPos <= targetData.MyCollisionAreaData.ForwardZPos
			&& myAreaData.BackZPos > targetData.MyCollisionAreaData.BackZPos && IsYCollision() && IsXCollision());

		return new CollisionResultData(isRightTemp, isLeftTemp, isTopTemp, isBottomTemp, isForwardTemp, isBackTemp, targetData);
	}
	private bool IsXCollision()
	{
		return Mathf.Abs(_vector.x) < _hitDistanceVector.x;
	}
	private bool IsZCollision()
	{
		return Mathf.Abs(_vector.z) < _hitDistanceVector.z;
	}

	private bool IsYCollision()
	{
		return Mathf.Abs(_vector.y) < _hitDistanceVector.y;
	}
	private bool IsYCollision(CollisionAreaData myAreaData, CollisionAreaData targetAreaData)
	{
		return myAreaData.BottomYPos >= targetAreaData.TopYPos && myAreaData.TopYPos <= targetAreaData.BottomYPos;
	}
	private void Init()
	{
		_vector = default;
		_hitDistanceVector = default;
		isTopTemp = false;
		isBottomTemp = false;
		isRightTemp = false;
		isLeftTemp = false;
		isForwardTemp = false;
		isBackTemp = false;
	}
}