using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	private Animator _myAnimator = default;
	private IInputCharcterActionGetable _input = default;
	// Start is called before the first frame update
	private void Start()
	{
		_myAnimator = GetComponent<Animator>();
		_input = new InGamePlayerInput();
	}

	// Update is called once per frame
	private void Update()
	{
		if (_input.IsAttack)
		{
			_myAnimator.SetTrigger("Test");
		}
	}
}
