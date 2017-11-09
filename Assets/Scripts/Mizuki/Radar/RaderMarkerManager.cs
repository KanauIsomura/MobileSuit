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
	private List<string> m_RaderViewObjectTagList = new List<string>(); // レーダーに映すオブジェクトのタグ名リスト(Inspectorでいじること)
	[SerializeField]
	private GameObject m_PlayerObj;             // プレイヤーのオブジェクト
	private ObjectUsingChecker m_MakerManager;  // レーダーに映すものリスト
    [SerializeField]
    private GameObject m_ImagePrefub;           // マーカーアイコンプレハブ
    public float m_RaderRange;                  // レーダーの視認範囲比率

	// Use this for initialization
	void Start () {
        m_MakerManager = new ObjectUsingChecker();
        m_MakerManager.SetObjectParent(gameObject);
    }

    /// <summary>
    /// 一定時間ごとにマーカーの設置(現状スティッククラスから呼ばれます)
    /// </summary>
	public void CheckAndSetMarker() {
		List<GameObject> m_RaderObjList = new List<GameObject>();
		m_MakerManager.DeleteObjAll();
		foreach(string tagName in m_RaderViewObjectTagList) {
			m_RaderObjList.AddRange(GameObject.FindGameObjectsWithTag(tagName));
		}

		foreach(GameObject obj in m_RaderObjList) {
			Vector3 position = (obj.transform.position / m_RaderRange) - (m_PlayerObj.transform.position / m_RaderRange);
			var clone = m_MakerManager.NewObjGet(m_ImagePrefub).ObjBody;
            clone.GetComponent<RaderMarker>().SetMakerInRader(position);
        }
		//testImage.transform.localPosition = new Vector3(position.x, position.z, 0.0f);
	}
}
