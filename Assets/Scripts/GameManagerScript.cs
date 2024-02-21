using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
	private GameStateMachineScript _myStateMachine = default;
	[SerializeField]
	private bool _debugFlag = default;
	
	private void Start()
	{
		if (GameObject.FindGameObjectsWithTag("GameController").Length <= 1)
		{
			DontDestroyOnLoad(this);
			_myStateMachine = new GameStateMachineScript();
			if (_debugFlag)
			{
				new ErrorManagerScript().InstantiationMyInstance();
			}
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
		if(ErrorManagerScript.MyInstance is not NullErrorManagerScript)
		{
			ErrorManagerScript.MyInstance.DeleteMyInstance();
		}
		if (_myStateMachine is not null)
		{
			_myStateMachine.Delete();
		}
	}
}