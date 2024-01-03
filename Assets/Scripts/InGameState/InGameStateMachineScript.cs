/// <summary>
/// InGameのステートを遷移させる
/// </summary>
public class InGameStateMachineScript
{
    private BaseInGameStateScript _beforeState = default;
    private IInputPlayerAction _inputPlayerAction = default;
    /// <summary>
    /// ステートを更新する
    /// </summary>
    /// <returns>現在のステート</returns>
    public BaseInGameStateScript UpdateState()
    {
        if (_inputPlayerAction.IsOpenInventory())
        {

        }else if (_inputPlayerAction.IsOpenPose())
        {

        }
        

        return null;
    }
}