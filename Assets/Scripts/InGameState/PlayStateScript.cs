using UnityEngine;

/// <summary>
/// プレイしているときの処理
/// </summary>
public class PlayStateScript : BaseInGameStateScript
{
	private ObjectManagerScript _objectManagerScript = default;
	public PlayStateScript(InGamePlayerInput input)
	{
		GameObject objectManager = GameObject.FindWithTag("ObjectManager");
#if UNITY_EDITOR
		if (objectManager is null)
		{
			ErrorManagerScript.MyInstance.NullGameObjectError("ObjectManager");
		}
#endif
		_objectManagerScript = objectManager.GetComponent<ObjectManagerScript>();
#if UNITY_EDITOR
		if (_objectManagerScript is null)
		{
			ErrorManagerScript.MyInstance.NullScriptError("ObjectManagerScript");
		}
#endif
		_objectManagerScript.Init(input);
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