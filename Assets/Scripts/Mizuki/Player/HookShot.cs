//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//	ObjectCatcher.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	フックショットの挙動
//
//	
//==================================================
//	作成日：2017/10/25
//	
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : ObjectBase {
	private float m_CoolTime;           // 再使用可能までのタイマー
	[SerializeField]
	private float m_CoolTimeDefault;    // 再使用可能までの時間
	private bool m_UseHook;             // 使用中か否か
	private bool m_isHit;               // 何かにあたったか否か
	[SerializeField]
	private float m_Speed;              // 速度
	private Transform m_PlayerTrandform;// プレイヤーのTransform
	private Rigidbody rigidBody;        // 銛のRigitbody
	[SerializeField]
	private float ShotPower;			// 銛を打ち出す強さ

	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
		m_CoolTime = 0;
		m_UseHook = false;
		m_isHit = false;
		rigidBody = GetComponent<Rigidbody>();
	}

	public override void Execute(float deltaTime) {
		if(m_UseHook) {

		} else if(m_isHit) {

		} else {
			m_CoolTime -= deltaTime;
		}
	}

	public override void LateExecute(float deltaTime) {

	}

	/// <summary>
	/// 発射処理
	/// </summary>
	/// <param name="player">プレイヤーのTransform</param>
	public void Shot(Transform player) {
		if(m_CoolTime > 0) return;

		m_CoolTime = m_CoolTimeDefault;
		m_UseHook = true;
		m_isHit = false;
		rigidBody.AddForce(player.forward * ShotPower);
	}

	public void OnCollisionEnter(Collision obj) {
		if(obj.transform.tag == "CatchedObject") {
			// 捕まえられる物に当たったらオブジェクトを銛の子に
			obj.transform.parent = transform;
		} else if(obj.transform.tag == "Ground") {
			// 床に当たったら
			/// 引っ張られる処理
		}
	}
}