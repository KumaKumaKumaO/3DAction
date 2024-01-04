/// <summary>
/// InGameのステートを遷移させる
/// </summary>
public class InGameStateMachineScript
{
    private BaseInGameStateScript _nowState = default;
    private IInputPlayerAction _inputPlayerAction = default;
    /// <summary>
    /// ステートを更新する
    /// </summary>
    /// <returns>現在のステート</returns>
    public BaseInGameStateScript UpdateState()
    {
        if (_inputPlayerAction.IsOpenInventory())
        {
            //プレイ中だったら
            if(_nowState.GetType() == typeof(PlayStateScript))
            {
                _nowState = new InventoryStateScript();
			}
			else if(_nowState.GetType() == typeof(InventoryStateScript))
			{
                _nowState = new PlayStateScript();
			}
        }else if (_inputPlayerAction.IsOpenPose())
        {
            if(_nowState.GetType() == typeof(PlayStateScript))
			{
                _nowState = new PoseStateScript();
			}else if (_nowState.GetType() == typeof(PoseStateScript))
			{
                _nowState = new PlayStateScript();
			}
        }
        return _nowState;
    }
}