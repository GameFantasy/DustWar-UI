using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class MouseScaleButton : Selectable, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

	[FormerlySerializedAs("onClick")]
	public Button.ButtonClickedEvent m_OnRightClick = new Button.ButtonClickedEvent();
	public Button.ButtonClickedEvent m_OnLeftClick = new Button.ButtonClickedEvent();
	
	protected RectTransform m_TransSelf;
	
	protected Vector3 m_vOriginalScale;
	protected Vector3 m_vPointerDownScale;
	
	protected override void Awake()
	{
		base.Awake();
		m_TransSelf = transform as RectTransform;
		m_vOriginalScale = transform.localScale;
		m_vPointerDownScale = m_vOriginalScale * 0.8f;
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
        //Debug.Log("按下");
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        m_TransSelf.localScale = m_vOriginalScale;
    }
	
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.pointerId == -1) {
			m_OnLeftClick.Invoke();
		}else if(eventData.pointerId == -2) {
		    m_OnRightClick.Invoke();
		}
		//Debug.Log("抬起");
	}
}
