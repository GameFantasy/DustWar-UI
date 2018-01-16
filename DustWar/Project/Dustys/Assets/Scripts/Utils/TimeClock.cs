using UnityEngine;
using System.Collections;

public class TimeClock{
	private float m_CurTime;
	private float m_TimeScale;

	public TimeClock(float scale){
		m_CurTime = 0;
		m_TimeScale = scale;
	}

	public void Update(){
		m_CurTime += Time.deltaTime;
	}

	public bool Timeout(){
		if (m_CurTime >= m_TimeScale) {
			m_CurTime = 0;
			return true;
		} else {
			return false;
		}
	}

	public void SetTimeScale(float scale){
		m_TimeScale = scale;
	}
}
