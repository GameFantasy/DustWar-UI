using UnityEngine;
using System.Collections;

public abstract class BasePage : MonoBehaviour {
	
	public abstract void OnActiveBefre ();
	public void Start(){
		this.transform.localPosition = new Vector3 (0, 0, 0);
		this.transform.localScale = new Vector3 (1, 1, 1);
	}

	public void Close(){
		Destroy (this.gameObject);
	}
}
