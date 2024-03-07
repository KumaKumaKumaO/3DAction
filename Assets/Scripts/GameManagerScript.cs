using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
	private static GameManagerScript _myInstance = default;
	private GameStateMachineScript _myStateMachine = default;

#if UNITY_EDITOR
	[SerializeField]
	private bool _debugFlag = default;
#endif

	public static GameManagerScript Instance { get { return _myInstance; } }
	public BaseGameStateScript NowState { get { return _myStateMachine.NowState; } }
	private void Start()
	{
		if (GameObject.FindGameObjectsWithTag("GameController").Length <= 1)
		{
			DontDestroyOnLoad(this);
			_myStateMachine = new GameStateMachineScript();
			_myInstance = this;
#if UNITY_EDITOR
			if (_debugFlag)
			{
				new ErrorManagerScript().InstantiationMyInstance();
			}
#endif
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		_myStateMachine.UpdateState().Execute();
	}

	private void OnDisable()
	{
#if UNITY_EDITOR
		if (ErrorManagerScript.MyInstance is not NullErrorManagerScript)
		{
			ErrorManagerScript.MyInstance.DeleteMyInstance();
		}
#endif
		if (_myStateMachine is not null)
		{
			_myStateMachine.Delete();
		}
	}
}