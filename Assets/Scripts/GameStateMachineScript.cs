using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachineScript
{
    private BaseGameStateScript _nowState;
    public GameStateMachineScript(BaseGameStateScript initState)
    {
        _nowState = initState;
    }
    /// <summary>
    /// 状態を変えれる場合は変える
    /// </summary>
    public void UpdateState()
    {
        
    }
    /// <summary>
    /// 現在の状態での動作を実行する
    /// </summary>
    public void Execute()
    {
        _nowState.Execute();
    }
}
