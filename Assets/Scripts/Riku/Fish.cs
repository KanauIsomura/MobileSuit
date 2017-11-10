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

        void Start()
        {
            _moveBehaviour = GetComponent<FishMoveBehaviour>();
        }

        void Update()
        {
            float tick = Time.deltaTime;
            _moveBehaviour.AddCurrentFrameAcceleration(new Vector3(1.0f, 0, 1.0f));
            _moveBehaviour.Exec(tick);  // 移動          
        }
    }

}
