using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private static GameManagerScript _instanceGameManager;
    private GameStateMachineScript _myStateMachine;
    public static GameManagerScript GameManager { 
        get { return _instanceGameManager; }
    }
    private void Start()
    {
        if(_instanceGameManager != null)
        {
            _instanceGameManager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        _myStateMachine.UpdateState();
        _myStateMachine.Execute();
    }
}
