using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory : MonoBehaviour
{
    public Transform _transform;

    public class Wall
    {
        public Vector3 postion;
        public Vector3 normal;
        public Wall(Vector3 p, Vector3 n)
        {
            postion = p;
            normal = n;
        }

        public Vector3 GetDistance(Vector3 _pos)
        {
            Vector3 distance = _pos - this.postion;
            float d = Vector3.Dot(distance, normal);
            distance = d * normal;
            return distance;
        }
    }


    void Start()
    {
        _transform = GetComponent<Transform>();
        MeshFilter mf = GetComponent<MeshFilter>();
        mf.mesh.SetIndices(mf.mesh.GetIndices(0), MeshTopology.LineStrip, 0);
    }

    void Update()
    {

    }

    public bool IsInTerritory(Vector3 point)
    {
        Vector3 p = _transform.position;
        Vector3 s = _transform.localScale * 0.5f;
        if (p.x + s.x < point.x) return false;
        if (p.x - s.x > point.x) return false;
        if (p.y + s.y < point.y) return false;
        if (p.y - s.y > point.y) return false;
        if (p.z + s.z < point.z) return false;
        if (p.z - s.z > point.z) return false;
        return true;
    }

    public Wall[] GetWall()
    {
        Wall[] wall = new Wall[6];
        Vector3 pos = _transform.position;
        Vector3 scale = _transform.localScale;
        scale *= 0.5f;
        wall[0] = new Wall(new Vector3(0, pos.y + scale.y, 0), new Vector3(0, -1.0f, 0)); // 上
        wall[1] = new Wall(new Vector3(0, pos.y - scale.y, 0), new Vector3(0, +1.0f, 0)); // 下
        wall[2] = new Wall(new Vector3(pos.x - scale.x, 0, 0), new Vector3(+1.0f, 0, 0)); // 左
        wall[3] = new Wall(new Vector3(pos.x + scale.x, 0, 0), new Vector3(-1.0f, 0, 0)); // 右
        wall[4] = new Wall(new Vector3(0, 0, pos.z - scale.z), new Vector3(0, 0, +1.0f)); // 前
        wall[5] = new Wall(new Vector3(0, 0, pos.z + scale.z), new Vector3(0, 0, -1.0f)); // 奥
        return wall;
    }
  
}
