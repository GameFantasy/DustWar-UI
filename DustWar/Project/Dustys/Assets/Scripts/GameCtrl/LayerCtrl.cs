using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.GameLayer;
using Assets.Utils;

public class LayerCtrl : SingletonMono<LayerCtrl> {
    private string path;
    private RectTransform parentRect;

    public void Init(string path, RectTransform parentRect) {
        this.path = path;
        this.parentRect = parentRect;
    }

    private Dictionary<string, Layer> _allControls = new Dictionary<string, Layer>();

    public void Register<T>() where T : Layer {
        string Tname = typeof(T).ToString();
        T l = Resources.Load<T>(path + "/" + Tname);
        T go = GameObject.Instantiate<T>(l);
        go.transform.SetParent(parentRect);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;

        string name = typeof(T).ToString();
        if (!_allControls.ContainsKey(name)) {
            _allControls.Add(name, go);
        }
    }

    public void Unregister<T>() {
        string name = typeof(T).ToString();
        if (_allControls.ContainsKey(name)) {
            GameObject.Destroy(_allControls[name].gameObject);
            _allControls.Remove(name);
        }
    }

    public void HideAll() {
        foreach (KeyValuePair<string, Layer> l in _allControls) {
            l.Value.Hide();
        }
    }

    public void Show<T>() {
        HideAll();
        string name = typeof(T).ToString();
        if (_allControls.ContainsKey(name)) {
            _allControls[name].Show();
        }
    }
}
