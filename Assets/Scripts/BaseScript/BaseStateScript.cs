using UnityEngine;
/// <summary>
/// �X�e�[�g�̊��N���X
/// </summary>
public abstract class BaseStateScript
{

	/// <summary>
	/// �X�e�[�g�ɓ������Ƃ���1�x�������s�����
	/// </summary>
	public virtual void Enter()
	{
		Debug.Log(this + "�X�e�[�g�ɑJ��");
	}

	/// <summary>
	/// ���t���[�����s�����
	/// </summary>
	public virtual void Execute()
	{

	}

	/// <summary>
	/// �X�e�[�g����o��Ƃ���1�x�������s�����
	/// </summary>
	public virtual void Exit()
	{

	}
}
