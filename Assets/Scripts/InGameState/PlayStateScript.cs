using UnityEngine;

/// <summary>
/// ƒvƒŒƒC‚µ‚Ä‚¢‚é‚Æ‚«‚Ìˆ—
/// </summary>
public class PlayStateScript : BaseInGameStateScript
{
	private ObjectManagerScript _objectManagerScript = default;
	public PlayStateScript(InGamePlayerInput input)
	{
		GameObject objectManager = GameObject.FindWithTag("ObjectManager");
		if (objectManager == null)
		{
			ErrorManagerScript.MyInstance.NullGameObjectError("ObjectManager");
		}
		else if (!objectManager.TryGetComponent<ObjectManagerScript>(out _objectManagerScript))
		{
			ErrorManagerScript.MyInstance.NullScriptError("ObjectManagerScript");
		}
		_objectManagerScript.Init(input);
	}

	public override void Enter()
	{
		
		base.Enter();
	}
	public override void Execute()
	{
		base.Execute();
		_objectManagerScript.AllObjectUpdate();
	}
	public override void Exit()
	{
		base.Exit();
		_objectManagerScript.Delete();
		_objectManagerScript = null;
	}
}