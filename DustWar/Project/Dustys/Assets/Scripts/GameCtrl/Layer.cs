using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameLayer {
    public class Layer : MonoBehaviour{
        public void Show() {
            this.gameObject.SetActive(true);
        }
        public void Hide() {
            this.gameObject.SetActive(false);
        }
    }
}
