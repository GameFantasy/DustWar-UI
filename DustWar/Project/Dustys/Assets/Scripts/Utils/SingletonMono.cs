using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils {
    public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour {
        public static T Ins {
            get {
                return FindObjectOfType<T>();
            }
        }
    }
}
