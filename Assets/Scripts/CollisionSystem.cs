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

        bool isTopTemp = IsCollision(targetToMeVector + Vector3.up * (myAreaData.HalfAreaSize.y + myAreaData.HalfAreaWidth)
            , hitDistanceVector + Vector3.up * -(myAreaData.HalfAreaSize.y - myAreaData.AreaWidth));

        bool isBottomTemp = IsCollision(targetToMeVector + Vector3.down * (myAreaData.HalfAreaSize.y + myAreaData.HalfAreaWidth)
            , hitDistanceVector + Vector3.up * -(myAreaData.HalfAreaSize.y - myAreaData.AreaWidth));

        bool isRightTemp = IsCollision(targetToMeVector + Vector3.right * (myAreaData.HalfAreaSize.x + myAreaData.HalfAreaWidth)
            , hitDistanceVector + Vector3.right * -(myAreaData.HalfAreaSize.x - myAreaData.AreaWidth));

        bool isLeftTemp = IsCollision(targetToMeVector + Vector3.left * (myAreaData.HalfAreaSize.x + myAreaData.HalfAreaWidth)
            , hitDistanceVector + Vector3.right * -(myAreaData.HalfAreaSize.x - myAreaData.AreaWidth));

        bool isForwardTemp = IsCollision(targetToMeVector + Vector3.forward * (myAreaData.HalfAreaSize.z + myAreaData.HalfAreaWidth)
            , hitDistanceVector + Vector3.forward * -(myAreaData.HalfAreaSize.z - myAreaData.AreaWidth));

        bool isBackTemp = IsCollision(targetToMeVector + Vector3.back * (myAreaData.HalfAreaSize.z + myAreaData.HalfAreaWidth)
            , hitDistanceVector + Vector3.forward * -(myAreaData.HalfAreaSize.z - myAreaData.AreaWidth));

        return new CollisionResultData(isRightTemp, isLeftTemp, isTopTemp, isBottomTemp, isForwardTemp, isBackTemp, targetData);
    }
    private bool IsCollision(Vector3 myColDistance, Vector3 targetColDistance)
    {
        //Debug.Log(myColDistance + ":" + targetColDistance);
        return Mathf.Abs(myColDistance.x) < targetColDistance.x
            && Mathf.Abs(myColDistance.y) < targetColDistance.y
            && Mathf.Abs(myColDistance.z) < targetColDistance.z;
    }
}