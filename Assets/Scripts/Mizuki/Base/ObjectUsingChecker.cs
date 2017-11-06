using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectUsingChecker<T> : ObjectBase {
	public struct ObjectData {
		public T ObjBody;
		public bool isUsing;
		public int NumberOfList;
	}
	private List<ObjectData> m_ObjList;

	// Use this for initialization
	void Start() {
		m_OrderNumber = 0;
		ObjectManager.Instance.RegistrationList(this, m_OrderNumber);

	}

	public ObjectData NewObjGet() {
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
		ObjectData obj = Instantiate(ObjectData);
		obj.ObjBody = default(T);
		obj.isUsing = true;
		obj.NumberOfList = i;
		m_ObjList.Add(obj);
		return obj;
	}

	public void DeleteObj(int delObjNum) {
		ObjectData obj = m_ObjList[delObjNum];
		obj.isUsing = false;
		obj.NumberOfList = -1;
		obj.ObjBody = default(T);
		m_ObjList[delObjNum] = obj;
	}

	public void DeleteObjAll() {
		m_ObjList.Clear();
	}
}
