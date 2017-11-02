using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectUsingChecker<T> : ObjectBase {
	private List<T> m_ObjList;

	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);

	}

	public override void Execute(float deltaTime) {

	}

	public override void LateExecute(float deltaTime) {

	}

	//public void 
}
