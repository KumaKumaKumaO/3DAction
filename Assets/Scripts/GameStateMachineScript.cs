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
    /// ��Ԃ�ς����ꍇ�͕ς���
    /// </summary>
    public void UpdateState()
    {
        
    }
    /// <summary>
    /// ���݂̏�Ԃł̓�������s����
    /// </summary>
    public void Execute()
    {
        _nowState.Execute();
    }
}
