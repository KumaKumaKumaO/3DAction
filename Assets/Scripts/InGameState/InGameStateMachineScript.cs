/// <summary>
/// InGameのステートを遷移させる
/// </summary>
public class InGameStateMachineScript
{
	//前回のステート
	private BaseInGameStateScript _beforeState = default;
	//現在のステート
	private BaseInGameStateScript _nowState = default;
	//プレイヤーの入力
	private IInputPlayerAction _inputPlayerAction = default;

	/// <summary>
	/// 初期化
	/// </summary>
	/// <param name="initState">初期のステート</param>
	public InGameStateMachineScript(BaseInGameStateScript initState)
	{
		this._nowState = initState;
	}

	/// <summary>
	/// 入力を設定する
	/// </summary>
	/// <param name="inputPlayerAction">プレイヤー入力</param>
	public void SetPlayerInput(IInputPlayerAction inputPlayerAction)
	{
		this._inputPlayerAction = inputPlayerAction;
	}

	/// <summary>
	/// ステートを更新する
	/// </summary>
	/// <returns>現在のステート</returns>
	public BaseInGameStateScript UpdateState()
	{
		//入力が存在する場合
		if (_inputPlayerAction != null)
		{
			//インベントリーを開くボタンを押したら
			if (_inputPlayerAction.IsOpenInventory())
			{
				//プレイ中だったら
				if (_nowState.GetType() == typeof(PlayStateScript))
				{
					//現在のステートを保持する
					_beforeState = _nowState;
					//現在のステートをインベントリーにする
					_nowState = new InventoryStateScript();
					_nowState.Enter();
				}
				//インベントリーを開いていたら
				else if (_nowState.GetType() == typeof(InventoryStateScript))
				{
					_nowState.Exit();
					//前回のステートに戻す
					_nowState = _beforeState;
					//前回のステートのアドレスを破棄する
					_beforeState = null;
				}
			}
			//ポーズを開くボタンを押したら
			else if (_inputPlayerAction.IsOpenPose())
			{
				//プレイ中だったら
				if (_nowState.GetType() == typeof(PlayStateScript))
				{
					//現在のステートを保持する
					_beforeState = _nowState;
					//現在のステートをポーズステートにする
					_nowState = new PoseStateScript();
					_nowState.Enter();
				}
				//ポーズを開いていたら
				else if (_nowState.GetType() == typeof(PoseStateScript))
				{
					_nowState.Exit();
					//前回のステートに戻す
					_nowState = _beforeState;
					//前回のステートのアドレスを破棄する
					_beforeState = null;
				}
			}
		}
		//現在のステートを返す
		return _nowState;
	}
}