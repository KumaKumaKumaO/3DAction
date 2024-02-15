using UnityEngine;

[System.Serializable]
public struct CollisionResultData
{
	//ぶつかっているところに対してキャラクターが左右のヒット判定を取る？
	[SerializeField]
	private bool isCollisionTop;
	[SerializeField]
	private bool isCollisionBottom;
	[SerializeField]
	private bool isCollisionRight;
	[SerializeField]
	private bool isCollisionLeft;
	[SerializeField]
	private bool isCollisionForward;
	[SerializeField]
	private bool isCollisionBack;
	private bool isOverLap;
	private BaseObjectScript _objectData;
	public bool IsCollision { 
		get
		{
			return (isCollisionRight || isCollisionLeft 
				|| isCollisionTop || isCollisionBottom 
				|| isCollisionForward || isCollisionBack);
		} 
	}
	public bool IsCollisionTop { get { return isCollisionTop; } }
	public bool IsCollisionBottom { get { return isCollisionBottom; } }

	public bool IsCollisionRight { get { return isCollisionRight; } }
	public bool IsCollisionLeft { get { return isCollisionLeft; } }
	public bool IsCollisionForward { get { return isCollisionForward; } }
	public bool IsCollisionBack { get { return isCollisionBack; } }
	public bool IsOverLap { get { return isOverLap; } }
	public BaseObjectScript ObjectData { get { return _objectData; } }
	public CollisionResultData(bool right,bool left , bool top, bool bottom , bool forward , bool back,bool overlap,BaseObjectScript collisionObjectData )
	{
		isCollisionTop = top;
		isCollisionBottom = bottom;
		isCollisionRight = right;
		isCollisionLeft = left;
		isCollisionForward = forward;
		isCollisionBack = back;
		isOverLap = overlap;
		this._objectData = collisionObjectData;
	}
}