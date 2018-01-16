using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.ResData {

    [System.Serializable]
    public class PlayerResData : ResData {
        #region 玩家属性
        public int m_Id;
        public string m_PlayerName;
        #endregion

        public PlayerResData() : base(){
        }
    }
}
