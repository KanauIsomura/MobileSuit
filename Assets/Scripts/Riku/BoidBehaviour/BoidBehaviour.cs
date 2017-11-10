using System.Collections;
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
        [SerializeField] private float _fieldOfView;
        [SerializeField] private float _minFishDistance;
        [SerializeField] private float _matchInfluence;
        [SerializeField] private float _steerInfluence;
        private Transform _transform;
        private FishMoveBehaviour _moveBehaviour;
        private List<Fish> _visibleFishList;
        private Fish _nearestFish;
        private float _nearestDistance;
        private float _swingSize;

        // ========================================
        // 初期処理
        // ========================================
        void Start()
        {
            _transform = GetComponent<Transform>();
            _moveBehaviour = GetComponent<FishMoveBehaviour>();
            _visibleFishList = new List<Fish>();
            // とりあえずここで設定
            _fieldOfView = 5.0f;
            _minFishDistance = 0.8f;
            _matchInfluence = 0.5f;
            _swingSize = 0.3f;
            _steerInfluence = 0.1f;
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
            if (_nearestFish != null)
            {
                acc += TakeDistance();
                acc += MatchMove();
                acc += SteerToCenter();
            }
            
            float force = 20.0f;
            _moveBehaviour.AddCurrentFrameAcceleration(acc * tick * force);
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
            moveSpeed *= (-1 * sign * _swingSize);


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
        // @return 加速度
        // ========================================
        protected virtual Vector3 BoundaryFlow()
        {
            return Vector3.zero;
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

