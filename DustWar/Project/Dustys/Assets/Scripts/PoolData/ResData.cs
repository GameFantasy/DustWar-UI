using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.ResData {
    [System.Serializable]
    public class ResData {
        public Dictionary<string, object> m_DataList;
        // Use this for initialization
        public ResData() {
            m_DataList = new Dictionary<string, object>();
        }

        public void AddData(string name, object val) {
            m_DataList.Add(name, val);
        }

        public int GetInt(string name) {
            return int.Parse(m_DataList[name].ToString());
        }

        public int GetInt(string name, int index) {
            return int.Parse(m_DataList[name].ToString().Split(',')[index]);
        }

        public float GetFloat(string name) {
            return float.Parse(m_DataList[name].ToString());
        }

        public float GetFloat(string name, int index) {
            return float.Parse(m_DataList[name].ToString().Split(',')[index]);
        }

        public string GetString(string name) {
            return m_DataList[name].ToString();
        }

        public string GetString(string name, int index) {
            return m_DataList[name].ToString().Split(',')[index];
        }
    }
}
