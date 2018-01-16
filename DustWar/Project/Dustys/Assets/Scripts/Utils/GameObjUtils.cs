using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjUtils
{
    public static GameObject Create(Transform parentTran, GameObject go)
    {
        GameObject retGo = GameObject.Instantiate<GameObject>(go);
        retGo.transform.parent = parentTran;
        retGo.transform.localPosition = Vector3.zero;
        retGo.transform.localScale = Vector3.one;
        return retGo;
    }

    public static void Destory(Transform parentTran)
    {
        foreach (Transform go in parentTran.GetComponentsInChildren<Transform>())
            if (go != parentTran)
            {
                GameObject.Destroy(go.gameObject);
            }
    }
}
