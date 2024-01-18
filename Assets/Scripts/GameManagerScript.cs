using UnityEngine;

/// <summary>
/// シングルトンクラス
/// ゲーム全体をコントロールする
/// </summary>
public class GameManagerScript : MonoBehaviour
{
	//自分のインスタンス
	private static GameManagerScript _instanceGameManager = default;
	//自分のステートマシン
	private GameStateMachineScript _myStateMachine = default;
	private void Start()
	{
		//インスタンスが存在するかを確認する
		if (_instanceGameManager == null)
		{
			_instanceGameManager = this;
			//シーンが変わっても消えないようにする
			DontDestroyOnLoad(this);
			//ステートマシンを生成
			_myStateMachine = new GameStateMachineScript();
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
}