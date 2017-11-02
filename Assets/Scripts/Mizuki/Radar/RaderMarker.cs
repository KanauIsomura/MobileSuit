//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//	RaderMarker.cs
//	
//	作成者:佐々木瑞生		
//==================================================
//	概要
//	マーカーオブジェクト
//	
//==================================================
//	作成日：2017/11/02
//	
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaderMarker : ObjectBase {
	[SerializeField]
	private Image m_ImageObj;	// 画像オブジェクト
	private bool m_Using;       // 使用中か否か
	//[SerializeField]
	//private List<Vector2> m_ImageSizeList;

	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);
		
		Debug.LogWarning("testオブジェクトは消しましたか？");
	}

	public override void Execute(float deltaTime) {

	}

	public override void LateExecute(float deltaTime) {

	}

	/// <summary>
	/// マーカーの表示変更
	/// </summary>
	/// <param name="position">マーカーのレーダ上の位置</param>
	/// <param name="type">マーカーのタイプ(敵、漂流物など)</param>
	public void SetMakerInRader(Vector2 position/*, EnemyType type*/) {
		m_ImageObj.transform.position = position;   // 位置
		//m_ImageObj.rectTransform.sizeDelta = m_ImageSizeList[(int)type];	// 大きさ変更
	}
}
