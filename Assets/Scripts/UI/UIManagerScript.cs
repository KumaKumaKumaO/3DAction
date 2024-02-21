using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class UIManagerScript:MonoBehaviour
{
	public void PlayerUIInit(BaseCharacterScript script)
	{
		HpBarViewScript viewScript = GetComponent<HpBarViewScript>();
		//Œã‚ÅŠ„‚é
		script.MyCharcterStatus.Hp.Subscribe(value => viewScript.Display(value));
	}
}
