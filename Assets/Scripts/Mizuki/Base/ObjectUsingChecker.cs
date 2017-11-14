
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
/*	ObjectUsingChecker.cs
//	
//	作成者:佐々木瑞生
//==================================================
//	概要
//	オブジェクトの管理をします。
//	テンプレートでやりたかったけど断念中
//==================================================
//	作成日：2017/11/08
*/
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectUsingChecker : ObjectBase {
	public struct ObjectData {
        //public T ObjBody;
        public GameObject ObjBody;
        public bool isUsing;
		public int NumberOfList;
	}
	private List<ObjectData> m_ObjList = new List<ObjectData>();
    private GameObject ObjectParent = null;

	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);

	}

    /// <summary>
    /// オブジェクト追加
    /// </summary>
    /// <param name="cloneObj">生成するオブジェクト(プレハブ)</param>
    /// <returns>生成したオブジェクト</returns>
	public ObjectData NewObjGet(GameObject cloneObj = null) {
		int i;
        // 未使用のオブジェクトを発見したらそれを使用する
		for(i = 0; i < m_ObjList.Count; i++) { 
			if(!m_ObjList[i].isUsing) {
				ObjectData clone = m_ObjList[i];
                clone.ObjBody.SetActive(true);
                clone.ObjBody = cloneObj;
                clone.isUsing = true;
				clone.NumberOfList = i;
				m_ObjList[i] = clone;
				return m_ObjList[i];
			}
		}

        // 未使用のオブジェクトが無かったら生成する
		ObjectData obj;
        //obj.ObjBody = default(T);
        obj.ObjBody = null;
        obj.ObjBody = Instantiate(cloneObj);
        if(ObjectParent != null) {
            obj.ObjBody.transform.parent = ObjectParent.transform;
        }
        obj.isUsing = true;
		obj.NumberOfList = i;
		m_ObjList.Add(obj);
		return obj;
	}

    /// <summary>
    /// 特定オブジェクトの削除(非アクティブ化)
    /// </summary>
    /// <param name="delObjNum">削除するオブジェクトの番号</param>
	public void DeleteObj(int delObjNum) {
		ObjectData obj = m_ObjList[delObjNum];
		obj.isUsing = false;
		obj.NumberOfList = -1;
        //obj.ObjBody = default(T);
        //Destroy(obj.ObjBody);
        obj.ObjBody.SetActive(false);
        m_ObjList[delObjNum] = obj;
	}

    /// <summary>
    /// 全オブジェクトの削除(非アクティブ化)
    /// </summary>
	public void DeleteObjAll() {
        foreach(ObjectData obj in m_ObjList) {
            //Destroy(obj.ObjBody);
            obj.ObjBody.SetActive(false);
        }
		//m_ObjList.Clear();
	}

    /// <summary>
    /// 親の変更
    /// </summary>
    /// <param name="obj">親オブジェクト</param>
    public void SetObjectParent(GameObject obj) {
        ObjectParent = obj;
    }

    /// <summary>
    /// 親のリセット(直下配置)
    /// </summary>
    public void ResetObjectParent() {
        ObjectParent = null;
    }
}
