using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarStickRotetion : ObjectBase {
	[SerializeField]
	private float m_StickRoteSpeed;
	private float m_StickRoteZ;

	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
		m_StickRoteZ = 360.0f;
	}

	public override void Execute(float deltaTime) {
		m_StickRoteZ -= m_StickRoteSpeed * deltaTime; 
		transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, m_StickRoteZ));
		// レーダーの針が一周したら
		if(m_StickRoteZ < 0.0f) {
			m_StickRoteZ += 360.0f;                 // 角度を正の数にする
			RaderMarkerManager.Instance.CheckAndSetMarker();
		}
	}

	public override void LateExecute(float deltaTime) {

	}
}
