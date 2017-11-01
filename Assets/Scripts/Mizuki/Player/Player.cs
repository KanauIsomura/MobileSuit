using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ObjectBase {
	[SerializeField]
	private HookShot hookShot;

	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);

	}

	public override void Execute(float deltaTime) {
		if(MultiInput.Instance.GetTriggerButton(MultiInput.CONTROLLER_BUTTON.CIRCLE)) {
			hookShot.Shot(transform);
		}
	}

	public override void LateExecute(float deltaTime) {

	}
}