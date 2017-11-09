using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rt
{

    public class MoveBehavior
    {
        private Transform _transform;
        public Transform ParentTransform
        {
            set { this._transform = value; }
            protected get { return this._transform; }
        }

        public virtual void Exec(float tick)
        {

        }

    }

}
