using UnityEngine;

/// <summary>
/// �����蔻��̃��W�b�N
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
	/// �ǂ��̕������Փ˂��Ă��邩�ǂ���
	/// </summary>
	/// <param name="myAreaData">���M�̓����蔻��f�[�^</param>
	/// <param name="targetData">�Ώۂ̃I�u�W�F�N�g�f�[�^</param>
	/// <returns></returns>
	public CollisionResultData GetCollisionResult(CollisionAreaData myAreaData, BaseObjectScript targetData)
	{
		_hitDistanceVector = myAreaData.HalfAreaSize + targetData.MyCollisionAreaData.HalfAreaSize;
		vector = (targetData.MyCollisionAreaData.MyTransform.position + targetData.MyCollisionAreaData.Offset)-(myAreaData.MyTransform.position + myAreaData.Offset);

		isRightTemp = (vector.x < 0 &&vector.x >= -_hitDistanceVector.x && IsYCollision() && IsZCollision());
		isLeftTemp = (vector.x > 0 &&vector.x <= _hitDistanceVector.x && IsYCollision() && IsZCollision());
		isTopTemp = (vector.y < 0 && vector.y >= -_hitDistanceVector.y && IsXCollision() && IsZCollision());
		isBottomTemp =  (vector.y > 0 && vector.y <= _hitDistanceVector.y && IsXCollision() && IsZCollision());
		isForwardTemp  =  (vector.z < 0 && vector.z >= -_hitDistanceVector.z && IsYCollision() && IsXCollision());
		isBackTemp =  (vector.z > 0 && vector.z <= _hitDistanceVector.z && IsYCollision() && IsXCollision());

		return new CollisionResultData(isRightTemp,isLeftTemp,isTopTemp,isBottomTemp,isForwardTemp,isBackTemp,targetData);
	}
	private bool IsXCollision()
    {
		return Mathf.Abs(vector.x) <= _hitDistanceVector.x;
    }
	private bool IsZCollision()
    {
		return Mathf.Abs(vector.z) <= _hitDistanceVector.z;
	}

	private bool IsYCollision()
    {
		return Mathf.Abs(vector.y) <= _hitDistanceVector.y;
    }
}