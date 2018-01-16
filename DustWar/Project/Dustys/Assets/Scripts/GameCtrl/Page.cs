using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.GamePage {

    public class PageHeler{
        public bool m_IsRegister;
        public bool m_CanDrag;
        public bool m_IsOpen;
        public Vector3 m_LocalPos;
        // 参数值
        public object m_Para;

        public PageHeler()
        {
            m_IsRegister = true;
            m_CanDrag = true;
            m_IsOpen = false;
        }
        public void Close() {
            m_IsOpen = false;
        }
        public bool IsOpen() {
            return m_IsOpen;
        }
        public void Open() {
            m_IsOpen = true;
        }
    }

    public abstract class Page : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler{
        public delegate void CloseCallBack(Page p);
        public CloseCallBack _closeCallBack;

        private RectTransform m_MyRectTransfrom;
        private Vector2 m_LastPointPos;

        public PageHeler _owner;
        
        public abstract void OnActiveBefre();
        public void Start()
        {
            m_MyRectTransfrom = this.transform as RectTransform;
            if (_owner != null)
            {
                this.transform.localPosition = _owner.m_LocalPos;
            }
            else
            {
                //this.transform.localPosition = Vector3.zero;
            }
            this.transform.localScale = new Vector3(1, 1, 1);
        }

        public void Init(CloseCallBack closeCallBack)
        {
            _closeCallBack = closeCallBack;
        }
        public void Show() {
            this.gameObject.SetActive(true);
        }
        public void Hide() {
            this.gameObject.SetActive(false);
            if (_closeCallBack != null) {
                _closeCallBack(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_owner.m_CanDrag)
            {
                m_LastPointPos = eventData.position;
            }
        }

        public void OnDrag(PointerEventData eventData) {
            if (_owner.m_CanDrag)
            {
                Vector3 globalMousePos1;
                Vector3 globalMousePos2;
                RectTransformUtility.ScreenPointToWorldPointInRectangle(
                    m_MyRectTransfrom.parent as RectTransform, m_LastPointPos, eventData.pressEventCamera, out globalMousePos1);

                RectTransformUtility.ScreenPointToWorldPointInRectangle(
                    m_MyRectTransfrom.parent as RectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos2);

                m_MyRectTransfrom.position += globalMousePos2 - globalMousePos1;

                m_LastPointPos = eventData.position;
            }
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (_owner.m_CanDrag)
            {
                _owner.m_LocalPos = m_MyRectTransfrom.localPosition;
            }
        }

        public void Close()
        {
            this.gameObject.SetActive(false);
            if (_closeCallBack != null)
            {
                _closeCallBack(this);
            }
        }

        public void OnPointerDown(PointerEventData eventData) {
            OnTop();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if (eventData.dragging == true) {
                OnTop();
            }
        }

        private void OnTop() {
            // 拉到最顶部
            Transform parent = m_MyRectTransfrom.parent;
            m_MyRectTransfrom.parent = PageCtrl.Ins.poolRect;
            m_MyRectTransfrom.parent = parent;

            Page[] ps = PageCtrl.Ins.parentRect.GetComponentsInChildren<Page>();
            int index = ps.Length - 1;
            // 找到注册的页面 
            while (!ps[index]._owner.m_IsRegister)
            {
                index -= 1;
            }
            PageCtrl.Ins.topPage = ps[index];
        }
    }
}
