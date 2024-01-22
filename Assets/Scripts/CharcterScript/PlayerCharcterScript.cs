using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharcterScript : BaseCharcterScript
{
	private void Start()
	{
		GetComponent<Renderer>().material.color = Color.black;
	}
	public override void ObjectUpdate()
	{
		base.ObjectUpdate();
	}
}
