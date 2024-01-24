public struct CollisionResultData
{
	private bool isCollisionTop;
	private bool isCollisionRight;
	private bool isCollisionLeft;
	private bool isCollisionBottom;
	public bool IsCollisionTop { get { return isCollisionTop; } }
	public bool IsCollisionRight { get { return isCollisionRight; } }
	public bool IsCollisionLeft { get { return isCollisionLeft; } }
	public bool IsCollisionBottom { get { return isCollisionBottom; } }
	
	public CollisionResultData(bool top,bool right,bool left ,bool bottom)
	{
		isCollisionTop = top;
		isCollisionRight = right;
		isCollisionLeft = left;
		isCollisionBottom = bottom;
	}
}