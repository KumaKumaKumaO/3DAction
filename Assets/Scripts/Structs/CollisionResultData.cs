public struct CollisionResultData
{
	private bool isCollisionTop;
	private bool isCollisionBottom;
	private bool isCollisionRight;
	private bool isCollisionLeft;
	private bool isCollisionForward;
	private bool isCollisionBack;
	private BaseObjectScript _collisionObjectData;
	
	public bool IsCollision { 
		get
		{
			return (isCollisionRight || isCollisionLeft 
				|| isCollisionTop || isCollisionBottom 
				|| isCollisionForward || isCollisionBack);
		} }
	public bool IsCollisionTop { get { return isCollisionTop; } }
	public bool IsCollisionBottom { get { return isCollisionBottom; } }

	public bool IsCollisionRight { get { return isCollisionRight; } }
	public bool IsCollisionLeft { get { return isCollisionLeft; } }
	public bool IsCollisionForward { get { return isCollisionForward; } }
	public bool IsCollisionBack { get { return isCollisionBack; } }
	public BaseObjectScript CollisionObjectData { get { return _collisionObjectData; } }
	
	public CollisionResultData(bool right,bool left , bool top, bool bottom , bool forward , bool back,BaseObjectScript collisionObjectData)
	{
		isCollisionTop = top;
		isCollisionBottom = bottom;
		isCollisionRight = right;
		isCollisionLeft = left;
		isCollisionForward = forward;
		isCollisionBack = back;
		this._collisionObjectData = collisionObjectData;
	}
}