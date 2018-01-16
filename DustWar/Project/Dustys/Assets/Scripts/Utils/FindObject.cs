using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FindObject{

    public static List<GameObject> FindChildsInGameObject(GameObject go, string name)
    {
		List<GameObject> rets = new List<GameObject>();
		Transform[] ts = go.GetComponentsInChildren<Transform> ();
		foreach (Transform t in ts) {
			if (t.name == name) {
				rets.Add(t.gameObject);
			}
		}
		return rets;
	}
}
