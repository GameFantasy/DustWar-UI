using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Assets.ResData {

    [Serializable]
    public class PoolResDataSet {

        // 玩家列表
        public Dictionary<int, PlayerResData> m_PlayerDic = new Dictionary<int, PlayerResData>();
        // 装备列表
        public Dictionary<int, CommonResData> m_EquipDic = new Dictionary<int, CommonResData>();

        private static PoolResDataSet m_Ins;
        public static PoolResDataSet Ins {
            get {
                if (m_Ins == null) {
                    m_Ins = new PoolResDataSet();
                    return m_Ins;
                }
                return m_Ins;
            }
            set {
                m_Ins = value;
            }
        }
    }
}
