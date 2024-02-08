using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharcterAttackMoveDistaceDatas")]
public class AttackDistanceScriptableScript : ScriptableObject
{
	[SerializeField]
	private Vector3[] _attackMoveDistanceDatas = new Vector3[0];
	public Vector3 this[int x]
	{
		get
		{
			if(_attackMoveDistanceDatas.Length > x)
			{
				return _attackMoveDistanceDatas[x];
			}
			else
			{
				ErrorManagerScript.MyInstance.OverFlow(this.name,x);
				return _attackMoveDistanceDatas[_attackMoveDistanceDatas.Length - 1];
			}
		}
	}
}
