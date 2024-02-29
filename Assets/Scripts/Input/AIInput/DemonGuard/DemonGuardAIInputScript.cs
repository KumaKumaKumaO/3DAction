using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class DemonGuardAIInputScript : BaseAIInputScript
{

	public DemonGuardAIInputScript(BaseCharacterScript myCharcterScript):base(myCharcterScript)
	{
		
	}
	//public virtual Vector2 MoveInput
	//{
	//	get
	//	{
	//		if(_playerCharcterScript is null || _playerCharcterScript.IsDeath)
	//		{
	//			return Vector2.zero;
	//		}

	//		Vector3 toPlayerVector 
	//			= CutYValue(_playerCharcterScript.MyTransform.position - _myCharcterScript.MyTransform.position);

	//		if (Mathf.Abs(toPlayerVector.normalized.x) < 0.05f && toPlayerVector.magnitude < _attackDistance)
	//		{
	//			return Vector2.zero;
	//		}
	//		return Vector2.right * toPlayerVector.x + Vector2.up * toPlayerVector.z;
	//	}
	//}

	//public virtual bool IsAttack
	//{
	//	get
	//	{
	//		if(_playerCharcterScript is null || _playerCharcterScript.IsDeath)
	//		{
	//			return false;
	//		}

	//		Vector3 toPlayerVector = CutYValue(_playerCharcterScript.MyTransform.position 
	//			- _myCharcterScript.MyTransform.position);

	//		if (toPlayerVector.magnitude < _attackDistance 
	//			&& Mathf.Abs(Vector3.SignedAngle(
	//				  _myCharcterScript.MyTransform.forward
	//				, toPlayerVector
	//				, _myCharcterScript.MyTransform.up)) < 0.1f)
	//		{
	//			return true;
	//		}
	//		return false;
	//	}
	//}

}
