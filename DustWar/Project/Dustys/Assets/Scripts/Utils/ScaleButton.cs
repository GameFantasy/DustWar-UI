using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class ScaleButton : Selectable, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    [FormerlySerializedAs("onClick")]
    public Button.ButtonClickedEvent m_OnClick = new Button.ButtonClickedEvent();

    protected RectTransform m_TransSelf;

    protected Vector3 m_vOriginalScale;
    protected Vector3 m_vPointerDownScale;

    public ListBtn m_ListBtn;
    public Sprite m_SelectedImage;
    public Sprite m_UnSelectedImage;

    protected override void Awake()
    {
        base.Awake();
		m_TransSelf = transform as RectTransform;
		m_vOriginalScale = transform.localScale;
		m_vPointerDownScale = m_vOriginalScale * 0.8f;
        m_ListBtn = GetComponentInParent<ListBtn>();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        // 
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        m_TransSelf.localScale = m_vOriginalScale;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        m_TransSelf.localScale = m_vPointerDownScale;
        if (m_SelectedImage != null)
        {
            foreach (ScaleButton btn in m_ListBtn.m_LlistBtn)
            {
                btn.gameObject.GetComponent<Image>().sprite = btn.m_UnSelectedImage;
            }
            gameObject.GetComponent<Image>().sprite = m_SelectedImage;
        }
        //Debug.Log("按下");
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        m_TransSelf.localScale = m_vOriginalScale;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        m_OnClick.Invoke();
        m_TransSelf.localScale = m_vOriginalScale;
        //Debug.Log("抬起");
    }
}
