using UnityEngine;

/// <summary>
/// �����蔻��̃��W�b�N
/// </summary>
public class CollisionSystem
{
	private Vector3 vector = default;

	/// <summary>
	/// �Փ˂��Ă��邩�ǂ���
	/// </summary>
	/// <param name="targetA">�����蔻��Ŏg���f�[�^</param>
	/// <param name="targetB">�����蔻��Ŏg���f�[�^</param>
	/// <returns></returns>
	public bool IsCollision(CollisionData targetA, CollisionData targetB)
	{
		vector = (targetA.MyTransform.position + targetA.Offset) - (targetB.MyTransform.position + targetB.Offset);

		return (AbsoluteProcess(vector.x) <= targetB.HalfAreaSize.x + targetA.HalfAreaSize.x
			&& AbsoluteProcess(vector.y) <= targetB.HalfAreaSize.y + targetA.HalfAreaSize.y
			&& AbsoluteProcess(vector.z) <= targetB.HalfAreaSize.z + targetA.HalfAreaSize.z);
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