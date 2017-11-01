using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarStickRotetion : ObjectBase {
	[SerializeField]
	private float m_StickRoteSpeed;

	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);

	}

	public override void Execute(float deltaTime) {
		transform.Rotate(new Vector3(0.0f, 0.0f, m_StickRoteSpeed * deltaTime));
	}

	public override void LateExecute(float deltaTime) {

	}
}
