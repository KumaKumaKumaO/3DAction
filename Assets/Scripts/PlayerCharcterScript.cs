using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharcterScript : BaseCharacterScript
{
    private InputScript _inputScript = new InputScript();
    public override void Initialize(InputScript inputScript)
    {
        base.Initialize(inputScript);
    }
    public override void MoveMyCharcter(Vector3 input)
    {
        
    }
}
