
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
/*	HarpoonShot.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	銛の発射処理
//	
//==================================================
//	作成日：2017/11/14
//  
//  現状のバグ…レイがプレイヤーに当たる。
*/
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarpoonShot : ObjectBase {
    private RaycastHit  m_Hit;          // 当たり判定
    private Image       m_TargetMarker; // 標準UI
    private Vector3     m_ScreenCenter; // 画面の中心
    private float       m_PlayerRange;  // 射程距離

    // Use this for initialization
    void Start() {
        m_OrderNumber = 0;
        ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
        m_ScreenCenter = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f);

        ///Debug///
        m_PlayerRange = 10000;
        ///////////
    }

    public override void Execute(float deltaTime) {
        Ray ray = Camera.main.ScreenPointToRay(m_ScreenCenter);
        Debug.DrawRay(ray.origin, ray.direction * m_PlayerRange, Color.green);
        if(Physics.Raycast(ray,out m_Hit, m_PlayerRange)) {
            if(m_Hit.collider.tag == "Enemy") {
                Debug.Log("hit");
            }
        }
    }

    public override void LateExecute(float deltaTime) {

    }

    /// <summary>
    /// 銛発射処理
    /// </summary>
    public void Shot() {

    }
}
