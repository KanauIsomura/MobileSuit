//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
//	RaderMarkerManager.cs
//	
//	作成者:佐々木瑞生		
//==================================================
//	概要
//	マーカーオブジェクトの管理、更新
//	
//==================================================
//	作成日：2017/11/02
//	
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaderMarkerManager : SingletonMonoBehaviour<RaderMarkerManager> {
	[SerializeField]
	private List<string> m_RaderViewObjectTagList = new List<string>();
	[SerializeField]
	private GameObject m_PlayerObj;         // プレイヤーのオブジェクト
	private ObjectUsingChecker<RaderMarker> m_MakerManager 
		= new ObjectUsingChecker<RaderMarker>(); // レーダーに映すものリスト

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void CheckAndSetMarker() {
		List<GameObject> m_RaderObjList = new List<GameObject>();
		m_MakerManager.DeleteObjAll();
		foreach(string tagName in m_RaderViewObjectTagList) {
			m_RaderObjList.AddRange(GameObject.FindGameObjectsWithTag(tagName));
		}

		foreach(GameObject obj in m_RaderObjList) {
			Vector3 position = obj.transform.position - m_PlayerObj.transform.position;
			m_MakerManager.NewObjGet().ObjBody.SetMakerInRader(position);
		}
		//testImage.transform.localPosition = new Vector3(position.x, position.z, 0.0f);
	}
}
