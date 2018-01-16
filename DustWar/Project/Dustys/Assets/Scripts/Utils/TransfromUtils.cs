using UnityEngine;
using System.Collections;

public class TransfromUtils{

	public static Vector2 WorldToLocalInRect(Vector3 worldPos,Camera UICamera,RectTransform inRect){
		// 先转屏幕坐标，再转相对坐标
		Vector2 localP;
		Vector2 screenpos = UICamera.WorldToScreenPoint(worldPos);
		RectTransformUtility.ScreenPointToLocalPointInRectangle(inRect, screenpos, UICamera, out localP);
		return localP;
	}
}
