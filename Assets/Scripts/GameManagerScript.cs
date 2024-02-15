using UnityEngine;

/// <summary>
/// シングルトンクラス
/// ゲーム全体をコントロールする
/// </summary>
public class GameManagerScript : MonoBehaviour
{
	//自分のステートマシン
	private GameStateMachineScript _myStateMachine = default;
	[SerializeField]
	private bool _debugFlag = default;
	
	private void Start()
	{
		//GameControllerが他に存在するかを確認する
		if (GameObject.FindGameObjectsWithTag("GameController").Length <= 1)
		{
			//シーンが変わっても消えないようにする
			DontDestroyOnLoad(this);
			//ステートマシンを生成
			_myStateMachine = new GameStateMachineScript();
			if (_debugFlag)
			{
				new ErrorManagerScript().InstantiationMyInstance();
			}
		}
		else
		{
			//インスタンスが存在する場合は自分を削除する
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		//現在のステートを実行する
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