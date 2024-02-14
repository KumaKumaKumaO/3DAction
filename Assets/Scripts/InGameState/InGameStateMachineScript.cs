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
	private InGamePlayerInput _playerInput = default;

	/// <summary>
	/// 初期化
	/// </summary>
	/// <param name="initState">初期のステート</param>
	public InGameStateMachineScript(BaseInGameStateScript initState,InGamePlayerInput input)
	{
		_playerInput = input;
		this._nowState = initState;
		_nowState.Enter();
	}

	/// <summary>
	/// ステートを更新する
	/// </summary>
	/// <returns>現在のステート</returns>
	public BaseInGameStateScript UpdateState()
	{
		//入力が存在する場合
		if (_playerInput != null)
		{
			//インベントリーを開くボタンを押したら
			if (_playerInput.IsOpenInventory)
			{
				//プレイ中だったら
				if (_nowState is PlayStateScript)
				{
					//現在のステートを保持する
					_beforeState = _nowState;
					//現在のステートをインベントリーにする
					_nowState = new InventoryStateScript(_playerInput);
					
				}
				//インベントリーを開いていたら
				else if (_nowState is InventoryStateScript)
				{
					_nowState.Exit();
					//前回のステートに戻す
					_nowState = _beforeState;
					//前回のステートのアドレスを破棄する
					_beforeState = null;
				}
				_nowState.Enter();
			}
			//ポーズを開くボタンを押したら
			else if (_playerInput.IsOpenPose)
			{
				//プレイ中だったら
				if (_nowState is PlayStateScript)
				{
					//現在のステートを保持する
					_beforeState = _nowState;
					//現在のステートをポーズステートにする
					_nowState = new PoseStateScript(_playerInput);
					
				}
				//ポーズを開いていたら
				else if (_nowState is PoseStateScript)
				{
					_nowState.Exit();
					//前回のステートに戻す
					_nowState = _beforeState;
					//前回のステートのアドレスを破棄する
					_beforeState = null;
				}
				_nowState.Enter();
			}

		}
		//現在のステートを返す
		return _nowState;
	}
}