using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rt
{
    class Debug
    {
        [System.Diagnostics.Conditional("DEBUG")]
        public static void Assert(bool expression, string msg)
        {
            if (expression) return;
            UnityEngine.Debug.LogError(msg);
        }
    }


}