using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体のステートを遷移させる
/// </summary>
public class GameStateMachineScript
{
	//前回のシーンの名前
	private string _beforeSceneName = default;
	//現在のシーンの名前
	private string _nowSceneName = default;
	//現在のステート
	private BaseGameStateScript _nowState = default;

	public BaseGameStateScript NowState { get { return _nowState; } }

	public GameStateMachineScript()
	{
		_nowSceneName = SceneManager.GetActiveScene().name;
		_beforeSceneName = _nowSceneName;
		_nowState = SelectState();
		_nowState.Enter();
	}

	public void Delete()
	{
		_nowState.Exit();
		_nowState = null;
	}

	/// <summary>
	/// ステートが変更できる場合は変更する
	/// </summary>
	/// <returns>現在のステート</returns>
	public BaseGameStateScript UpdateState()
	{
		//現在のシーンの名前
		_nowSceneName = SceneManager.GetActiveScene().name;
		//現在のシーンの名前と前回のシーンの名前が違うかの確認
		if (_nowSceneName != _beforeSceneName)
		{
			//現在のステートを終了させる
			_nowState.Exit();
			//変更先ステートを取得する
			_nowState = SelectState();
			//新しいステートを開始させる
			_nowState.Enter();
			//前回のシーン名に今回のシーン名を入れる
			_beforeSceneName = _nowSceneName;
		}
		return _nowState;
	}

	private BaseGameStateScript SelectState()
	{
		//現在のシーン名がTitleの場合
		if (_nowSceneName == "Title")
		{
			//新しいステートをインスタンスする
			return new TitleGameState();
		}
		//現在のシーン名がInGameの場合
		else if (_nowSceneName == "InGame")
		{
			//新しいステートをインスタンスする
			return new InGameStateScript();
		}
		//現在のシーン名がGameOverの場合
		else if (_nowSceneName == "GameOver")
		{
			//新しいステートをインスタンスする
			return new GameOverStateScript();
		}
		else if(_nowSceneName == "GameClear")
		{
			return new GameClearStateScript();
		}
		//現在のシーン名が特定できない場合
		else
		{
#if UNITY_EDITOR
			//エラーメッセージを出力
			Debug.LogError("存在しないシーン名です。");
#endif
			return null;
		}
	}
}
