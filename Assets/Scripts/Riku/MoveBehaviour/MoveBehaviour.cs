using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rt
{

    public class MoveBehaviour : MonoBehaviour
    {

        protected Transform _transform;

        public virtual void Start()
        {
            _transform = GetComponent<Transform>();
        }

        public virtual void Exec(float tick)
        {

        }
    }

}

