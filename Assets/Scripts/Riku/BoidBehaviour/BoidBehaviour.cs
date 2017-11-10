﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rt
{

    [RequireComponent(typeof(FishMoveBehaviour))]
    public class BoidBehaviour : MonoBehaviour
    {
        // ========================================
        // data
        // ========================================
        [SerializeField] private float _swimPower;
        [SerializeField] private float _fieldOfView;
        [SerializeField] private float _minFishDistance;
        [SerializeField] private float _matchInfluence;
        [SerializeField] private float _steerInfluence;
        [SerializeField] private Vector3 _territorialityLength;
        [SerializeField] private Vector3 _territorialityOrigin;
        private Transform _transform;
        private FishMoveBehaviour _moveBehaviour;
        private List<Fish> _visibleFishList;
        private Fish _nearestFish;
        private float _nearestDistance;
        private float _swingSize;

        // ========================================
        // class
        // ========================================
        class TerritorialityWall
        {
            public Vector3 postion;
            public Vector3 normal;
            public TerritorialityWall(Vector3 p, Vector3 n)
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
        

        // ========================================
        // 初期処理
        // ========================================
        void Start()
        {
            _transform = GetComponent<Transform>();
            _moveBehaviour = GetComponent<FishMoveBehaviour>();
            _visibleFishList = new List<Fish>();
            // とりあえずここで設定
            _swimPower = 20.0f;
            _fieldOfView = 5.0f;
            _minFishDistance = 0.8f;
            _matchInfluence = 0.5f;
            _swingSize = 0.3f;
            _steerInfluence = 0.1f;
            _territorialityLength = new Vector3(20.0f, 20.0f, 20.0f);
            _territorialityOrigin = new Vector3(0, 0, 0);
        }

        // ========================================
        // 更新処理
        // ========================================
        public virtual void Exec(float tick)
        {
            // 魚を探す
            SearchFish();

            // 加速度計算
            Vector3 acc;
            acc = PersonalMove();
            acc += TerritorialityFlow();
            acc += AvoidObstacle();
            if (_nearestFish != null)
            {
                acc += TakeDistance();
                acc += MatchMove();
                acc += SteerToCenter();
            }
            
            _moveBehaviour.AddCurrentFrameAcceleration(acc * _swimPower * tick);
        }

        // ========================================
        // 個人移動
        // @return 加速度
        // ========================================
        protected virtual Vector3 PersonalMove()
        {
            Vector3 moveSpeed = _moveBehaviour.moveSpeedVector;

            // 横縦
            float rand = Random.value;
            float sign = Mathf.Sign(Random.value - 0.5f);
            if (rand < 0.45f)
            {
                moveSpeed.x += _swingSize * sign;
            }
            else if (rand < 0.9f)
            {
                moveSpeed.z += _swingSize * sign;
            }
            else
            {
                moveSpeed.y += _swingSize * sign;
            }

            // 進行方向
            moveSpeed.Normalize();
            moveSpeed *= (-1 * sign * _swingSize) * 0.1f;


            return moveSpeed;
        }

        // ========================================
        // 魚を探す
        // ========================================
        protected virtual void SearchFish()
        {
            // リストの初期化
            _visibleFishList.Clear();
            _nearestFish = null;
            _nearestDistance = float.MaxValue;

            // Fishリストを取得して走査
            GameObject parentObject = _transform.root.gameObject;

            Transform parentTransform = parentObject.GetComponent<Transform>();
            foreach(Transform targetTransform in parentTransform)
            {
                // 自身はスルー
                if (targetTransform.GetInstanceID() == _transform.GetInstanceID())
                {
                    continue;
                }

                // 単純に近いかどうか
                float distance = Vector3.Distance(_transform.position, targetTransform.position);
                if (_fieldOfView < distance) continue;
                Fish targetFish = targetTransform.gameObject.GetComponent<Fish>();
                if (targetFish == null) continue;
                _visibleFishList.Add(targetFish);
                if (_nearestDistance < distance) continue;
                _nearestFish = targetFish;
                _nearestDistance = distance;
            }
        }

        // ========================================
        // 魚間で距離をとる
        // @return 加速度
        // ========================================
        protected virtual Vector3 TakeDistance()
        {
            float rate = _nearestDistance / _minFishDistance;
            Vector3 dir = _nearestFish.GetComponent<Transform>().position - _transform.position;
            dir.Normalize();
            rate = Mathf.Clamp(rate, 0.05f, 0.1f);
            if (_nearestDistance < _minFishDistance)
            {
                dir *= -rate;
            }
            else if (_nearestDistance > _minFishDistance)
            {
                dir *= rate;
            }
            else
            {
                dir *= 0;
            }
            return dir;
        }

        // ========================================
        // 同じ方向を目指す
        // @return 加速度
        // ========================================
        protected virtual Vector3 MatchMove()
        {
            return _nearestFish.moveDirection * _matchInfluence;
        }

        // ========================================
        // 集団の形を整える
        // @return 加速度
        // ========================================
        protected virtual Vector3 SteerToCenter()
        {
            Vector3 center = Vector3.zero;
            for (int i = 0, l = _visibleFishList.Count; i < l;  ++i)
            {
                center += _visibleFishList[i]._transform.position;
            }
            center /= _visibleFishList.Count;
            Vector3 v = center - _transform.position;
            v.Normalize();
            v *= _steerInfluence;
            return v;
        }

        // ========================================
        // 境界内を自然に留まる処理
        // 流れの方向が一定になってしまうので要対策
        // @return 加速度
        // ========================================
        protected virtual Vector3 TerritorialityFlow()
        {
            TerritorialityWall[] wall = new TerritorialityWall[6];
            wall[0] = new TerritorialityWall( new Vector3(0, _territorialityOrigin.y + _territorialityLength.y, 0), new Vector3(0, -1.0f, 0)); // 上
            wall[1] = new TerritorialityWall( new Vector3(0, _territorialityOrigin.y - _territorialityLength.y, 0), new Vector3(0, +1.0f, 0)); // 下
            wall[2] = new TerritorialityWall(new Vector3(_territorialityOrigin.x - _territorialityLength.x, 0, 0), new Vector3(+1.0f, 0, 0));  // 左
            wall[3] = new TerritorialityWall(new Vector3(_territorialityOrigin.x + _territorialityLength.x, 0, 0), new Vector3(-1.0f, 0, 0));  // 右
            wall[4] = new TerritorialityWall(new Vector3(0, 0, _territorialityOrigin.z - _territorialityLength.z), new Vector3(0, 0, +1.0f));  // 前
            wall[5] = new TerritorialityWall(new Vector3(0, 0, _territorialityOrigin.z + _territorialityLength.z), new Vector3(0, 0, -1.0f));  // 奥

            Vector3 force = Vector3.zero;
            bool isVisible = false;
            Vector3 position = _transform.position;
            for (int i = 0; i < 6; ++i)
            {
                Vector3 vDistance = wall[i].GetDistance(position);
                float fDistance = vDistance.magnitude + float.Epsilon;
                if (fDistance > _fieldOfView) continue;
                isVisible = true;
                vDistance.Normalize();
                force += vDistance / fDistance;
            }

            if (isVisible)
            {
                float r = Vector3.Dot(force, force);
                r = Mathf.Clamp(r, 0.05f, 1.0f);
                force.Normalize();
                force *= r;
            }

            return force;
        }

        // ========================================
        // 障害物をよける動き
        // @return 加速度
        // ========================================
        protected virtual Vector3 AvoidObstacle()
        {
            return Vector3.zero;
        }
    }

}

