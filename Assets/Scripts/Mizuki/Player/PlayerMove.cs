using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : ObjectBase {
	[SerializeField]
	private float m_PlayerMoveSpeed;			// 移動速度
	[SerializeField]
	private float m_PlayerRotSpeed;				// 回転速度
	private int[] m_RotReverse = new int[2];	// 回転方向の反転(0 = x,1 = y)

	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
		m_RotReverse[0] = -1;
		m_RotReverse[1] = 1;
	}

	public override void Execute(float deltaTime) {
		// 前進後退
		var pos = transform.position;pos += transform.forward * m_PlayerMoveSpeed * MultiInput.Instance.GetLeftStickAxis().y * deltaTime;
		pos += transform.right * m_PlayerMoveSpeed * MultiInput.Instance.GetLeftStickAxis().x * deltaTime;
		transform.position = pos;

		// 向き変更
		transform.Rotate(new Vector3(
			MultiInput.Instance.GetRightStickAxis().y * deltaTime * m_PlayerRotSpeed * m_RotReverse[0],
			MultiInput.Instance.GetRightStickAxis().x * deltaTime * m_PlayerRotSpeed * m_RotReverse[1],
			0.0f));
	}

	public override void LateExecute(float deltaTime) {

	}
}
