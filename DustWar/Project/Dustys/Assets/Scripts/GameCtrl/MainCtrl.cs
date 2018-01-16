using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Utils;

public class MainCtrl : Singleton<MainCtrl> {
    // 数据
    public int m_LayerIndex;
    public int m_OldScore;

    public int m_ReviveNeed = 2;
    public int m_VideoAdd = 20;

    public int m_FallTimes;
    public int m_WalkDis;
    public int m_UseTimerTimes;

    public MainCtrl() {
        
    }
}
