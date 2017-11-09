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

	public ObjectData NewObjGet(GameObject cloneObj = null) {
		int i;
		for(i = 0; i < m_ObjList.Count; i++) { 
			if(!m_ObjList[i].isUsing) {
				ObjectData clone = m_ObjList[i];
				clone.isUsing = true;
				clone.NumberOfList = i;
				m_ObjList[i] = clone;
				return m_ObjList[i];
			}
		}
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

	public void DeleteObj(int delObjNum) {
		ObjectData obj = m_ObjList[delObjNum];
		obj.isUsing = false;
		obj.NumberOfList = -1;
        //obj.ObjBody = default(T);
        Destroy(obj.ObjBody);
        m_ObjList[delObjNum] = obj;
	}

	public void DeleteObjAll() {
        foreach(ObjectData obj in m_ObjList) {
            Destroy(obj.ObjBody);
        }
		m_ObjList.Clear();
	}

    public void SetObjectParent(GameObject obj) {
        ObjectParent = obj;
    }

    public void ResetObjectParent() {
        ObjectParent = null;
    }
}
