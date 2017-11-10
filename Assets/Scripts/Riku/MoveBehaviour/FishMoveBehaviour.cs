using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rt
{
    public class FishMoveBehaviour : MoveBehaviour
    {
        // ========================================
        // data
        // ========================================
        private Vector3 _moveSpeedVector;
        private float _moveSpeedScalar;
        private Vector3 _moveDirection;        //　計算で求められるので必要かどうか…
        private Vector3 _prevPosition;
        private Vector3 _currentFrameAcceleration;
        [SerializeField] private float _maxCurrentFrameAcceleration;
        [SerializeField] private float _maxSpeedScalar;

        // ========================================
        // property    
        // ========================================
        public Vector3 moveSpeedVector
        {
            protected set
            {
                _moveSpeedVector = value;
                _moveSpeedScalar = _moveSpeedVector.magnitude;
                _moveDirection = _moveSpeedVector;
                _moveDirection.Normalize();
                if (maxSpeedScalar > _moveSpeedScalar) return;
                _moveSpeedVector = _moveDirection * maxSpeedScalar;
                _moveSpeedScalar = maxSpeedScalar;
            }
            get
            {
                return _moveSpeedVector;
            }

        }

        public float moveSpeedScalar
        {
            get
            {
                return _moveSpeedScalar;
            }
        }

        public Vector3 moveDirection
        {
            get
            {
                return _moveDirection;
            }
        }

        public Vector3 PrevPosition
        {
            protected set
            {
                _prevPosition = value;
            }
            get
            {
                return _prevPosition;
            }
        }

        protected Vector3 currentFrameAcceleration
        {
            set
            {
                _currentFrameAcceleration = value;
            }
            get
            {
                return _currentFrameAcceleration;
            }
        }

        public float maxCurrentFrameAcceleration
        {
            set
            {
                _maxCurrentFrameAcceleration = value;
            }
            get
            {
                return _maxCurrentFrameAcceleration;
            }
        }

        public float maxSpeedScalar
        {
            set
            {
                _maxSpeedScalar = value;
            }
            get
            {
                return _maxSpeedScalar;
            }
        }

        // ========================================
        // コンストラクタ
        // ========================================
        public override void Start()
        {
            base.Start();
            maxCurrentFrameAcceleration = 3.0f;
            maxSpeedScalar = 3.0f;

            moveSpeedVector = new Vector3((Random.value - 0.5f) * 2.0f, (Random.value - 0.5f) * 2.0f, (Random.value - 0.5f) * 2.0f);
        }

        // ========================================
        // アップデート関数
        // Updateは使用せずにFishから呼び出す
        // ========================================
        public override void Exec(float tick)
        {
            UpdatePosition(tick); // 位置の更新
            UpdateForward(tick); // 向きの更新
            //ClampPosition(tick);  // 範囲処理
            UpdateAcceleration(tick); // 加速度の更新
            UpdateSpeed(tick); // 速度の更新
            _currentFrameAcceleration = Vector3.zero; // フレーム間加速度の初期化
        }

        // ========================================
        // 現在位置の更新
        // ========================================
        protected void UpdatePosition(float tick)
        {
            Vector3 pos = _transform.position;
            PrevPosition = pos;
            pos += moveSpeedVector * tick;
            _transform.position = pos;
        }

        // ========================================
        // 向きの更新
        // TODO: 適当
        // ========================================
        protected virtual void UpdateForward(float tick)
        {
            if (moveDirection.sqrMagnitude == 0) return;
           _transform.forward = Vector3.Lerp(_transform.forward, moveDirection, 1.0f);
        }

        // ========================================
        // 位置の境界でのクランプ処理（テスト用)
        // ========================================
        protected void ClampPosition(float tick)
        {
            Vector3 pos = _transform.position;
            float field_x = 20.0f;
            float field_y = 20.0f;
            float field_z = 20.0f;
            if (pos.x > field_x) pos.x = -field_x;
            if (pos.y > field_y) pos.y = -field_y;
            if (pos.z > field_z) pos.z = -field_z;
            if (pos.x < -field_x) pos.x = field_x;
            if (pos.y < -field_y) pos.y = field_y;
            if (pos.z < -field_z) pos.z = field_z;
            _transform.position = pos;
        }

        // ========================================
        // 加速度の更新
        // ========================================
        void UpdateAcceleration(float tick)
        {
            float lhs = maxCurrentFrameAcceleration * maxCurrentFrameAcceleration;
            float rhs = currentFrameAcceleration.sqrMagnitude;
            if (lhs < rhs)
            {
                currentFrameAcceleration.Normalize();
                currentFrameAcceleration *= maxCurrentFrameAcceleration;
            }
        }

        // ========================================
        // 速度の更新
        // ========================================
        void UpdateSpeed(float tick)
        {
            moveSpeedVector += currentFrameAcceleration;
        }

        // ========================================
        //  加速度の追加
        // ========================================
        public void AddCurrentFrameAcceleration(Vector3 acc)
        {
            _currentFrameAcceleration += acc;
        }

    }
}

