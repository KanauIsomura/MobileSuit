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
	private List<RaderMarker> m_MarkerObjList = new List<RaderMarker>();
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void CheckAndSetMarker() {
		List<GameObject> m_RaderObjList = new List<GameObject>();// レーダーに映すものリスト
		foreach(string tagName in m_RaderViewObjectTagList) {
			m_RaderObjList.AddRange(GameObject.FindGameObjectsWithTag(tagName));
		}

		foreach(GameObject obj in m_RaderObjList) {
			//m_MarkerObjList
		}
		//Vector3 position = testObj.transform.position - m_PlayerObj.transform.position;
		//testImage.transform.localPosition = new Vector3(position.x, position.z, 0.0f);
	}
}
