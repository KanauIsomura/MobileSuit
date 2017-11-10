using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rt
{
    [RequireComponent(typeof(FishMoveBehaviour))]
    public class Fish : MonoBehaviour
    {
        [SerializeField]
        FishMoveBehaviour _moveBehaviour;
        BoidBehaviour _boidBehaviour;
        public Transform _transform;

        public Vector3 moveSpeed
        {
            get
            {
                return _moveBehaviour.moveSpeedVector;
            }
        }

        public Vector3 moveDirection
        {
            get
            {
                return _moveBehaviour.moveDirection;
            }
        }

        void Start()
        {
            _moveBehaviour = GetComponent<FishMoveBehaviour>();
            _boidBehaviour = GetComponent<BoidBehaviour>();
            _transform = GetComponent<Transform>();
            GetComponent<Transform>().position = new Vector3((Random.value - 0.5f) * 10.0f, 0, (Random.value - 0.5f) * 10.0f);
            Vector3 v = new Vector3((Random.value - 0.5f) * 2.0f, (Random.value - 0.5f) * 10.0f, (Random.value - 0.5f) * 10.0f);
            v.Normalize();
            GetComponent<Transform>().forward = v;
        }

        void Update()
        {
            float tick = Time.deltaTime;
            _moveBehaviour.Exec(tick);  // 移動          
            _boidBehaviour.Exec(tick);
        }
    }

}
