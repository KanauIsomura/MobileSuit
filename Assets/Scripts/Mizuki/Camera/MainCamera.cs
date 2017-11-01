using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : ObjectBase {
	[SerializeField]
	private GameObject m_PlayerObj;
	private Vector3 m_PlayerForward;
	[SerializeField]
	float m_CameraMoveSpeed;
	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
		m_PlayerForward = m_PlayerObj.transform.forward;
		transform.position = -m_PlayerForward * 5.0f;
	}

	public override void Execute(float deltaTime) {
		transform.rotation = Quaternion.Slerp(transform.rotation, m_PlayerObj.transform.rotation, Mathf.Clamp(deltaTime / m_CameraMoveSpeed, 0.0f, 1.0f));
		m_PlayerForward = m_PlayerObj.transform.forward;
		transform.position = m_PlayerObj.transform.position - m_PlayerForward * 5.0f;

		//transform.LookAt(m_PlayerObj.transform);
	}

	public override void LateExecute(float deltaTime) {

	}
}
