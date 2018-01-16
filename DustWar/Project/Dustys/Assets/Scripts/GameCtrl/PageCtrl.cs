using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.GamePage;
using Assets.Utils;

public class PageCtrl : SingletonMono<PageCtrl> {
    private string path;
    public RectTransform parentRect;
    public RectTransform poolRect;
    public Page topPage;

    public void Init(string path, RectTransform parentRect, RectTransform poolRect) {
        this.path = path;
        this.parentRect = parentRect;
        this.poolRect = poolRect;
    }

    private Dictionary<string, Page> _allControls = new Dictionary<string, Page>();
    private Dictionary<string, List<Page>> _allShow = new Dictionary<string, List<Page>>();

    public void Register<T>() where T : Page {
        string Tname = typeof(T).ToString();
        GameObject go0 = Resources.Load<GameObject>(path + "/" + Tname);
        T go = GameObjUtils.Create(poolRect, go0).GetComponent<T>();
        go.Init(CloseCallback);
        string name = typeof(T).ToString();
        if (!_allControls.ContainsKey(name)) {
            _allControls.Add(name, go);
        }
    }

    public void CloseCallback(Page p)
    {
        p._owner.Close();
        p.transform.parent = poolRect;
        _allShow.Remove(p.GetType().ToString());
        Page[] ps = parentRect.GetComponentsInChildren<Page>();
        if (ps.Length != 0)
        {
            topPage = ps[ps.Length - 1];
        }
        Destroy(p.gameObject);
    }

    public void Unregister<T>() {
        string name = typeof(T).ToString();
        if (_allControls.ContainsKey(name)) {
            GameObject.Destroy(_allControls[name].gameObject);
            _allControls.Remove(name);
        }
    }

    public void HideAll() {
        //foreach (KeyValuePair<string, Page> l in _allControls)
        //{
        //    l.Value.transform.parent = poolRect;
        //    l.Value.Hide();
        //}
    }

    public T Show<T>(PageHeler ip) where T : Page
    {
        string name = typeof(T).ToString();
        if (_allControls.ContainsKey(name)) {
            if (ip.IsOpen()) {
                foreach (Page p in _allShow[name]) {
                    if (ip == p._owner) {
                        p.Hide();
                        return null;
                    }
                }
            }
            GameObject go = GameObjUtils.Create(parentRect, _allControls[name].gameObject);
            T t = go.GetComponent<T>();
            t.Show();
            t.OnActiveBefre();
            if (_allShow.ContainsKey(name)) {
                _allShow[name].Add(t);
                t.Init(CloseCallback);
            } else {
                List<Page> list = new List<Page>();
                list.Add(t);
                _allShow.Add(name, list);
                t.Init(CloseCallback);
            }
            t._owner = ip;
            ip.Open();
            topPage = t;
            return t;
        }
        return null;
    }

    public T Find<T>(Page p = null) where T : Page
    {
        string name = typeof(T).ToString();
        if (_allShow.ContainsKey(name))
        {
            if (p != null)
            {
                foreach (Page tp in _allShow[name])
                {
                    if (tp == p)
                    {
                        return (T)tp;
                    }
                }
            }
            else
            {
                return (T)_allShow[name][0];
            }
        }
        return null;
    }
}
