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
    private GameObject m_ImagePrefab;           // マーカーアイコンプレハブ
    public float m_RaderRange = 1.0f;           // レーダーの視認範囲比率(大きいほうが広域が分かる)
    [SerializeField]
    private float m_RaderRangeLimit;            // レーダーの視認範囲(キャンバスのレーダー半径でいいです。初期値は100)

    // Use this for initialization
    void Start () {
        m_MakerManager = new ObjectUsingChecker();
        m_MakerManager.SetObjectParent(gameObject);
        if(m_RaderRange == 0.0f) {
            m_RaderRange = 1.0f;
        }
    }

    /// <summary>
    /// 一定時間ごとにマーカーの設置(現状スティッククラスから呼ばれます)
    /// </summary>
	public void CheckAndSetMarker() {
		List<GameObject> m_RaderObjList = new List<GameObject>();
		m_MakerManager.DeleteObjAll();
        // レーダーに乗せるオブジェクトを全検索
		foreach(string tagName in m_RaderViewObjectTagList) {
			m_RaderObjList.AddRange(GameObject.FindGameObjectsWithTag(tagName));
		}

        // レーダー上のオブジェクト位置を計算
		foreach(GameObject obj in m_RaderObjList) {
			Vector3 position = (obj.transform.position / m_RaderRange) - (m_PlayerObj.transform.position / m_RaderRange);
            // 範囲外ならレーダー上に表示しない
            if(m_RaderRangeLimit < Mathematics.VectorSize(new Vector3(position.x,position.z, 0.0f))) {
                continue;
            }
			var clone = m_MakerManager.NewObjGet(m_ImagePrefab).ObjBody;
            clone.GetComponent<RaderMarker>().SetMakerInRader(new Vector2(position.x, position.z));
        }
		//testImage.transform.localPosition = new Vector3(position.x, position.z, 0.0f);
	}
}
