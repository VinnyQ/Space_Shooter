using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	private bool touched;
	private int pointerId;
	private bool canFire;

	void Awake()
	{
		touched = false;
	}

	public void OnPointerDown(PointerEventData data)
	{
		if (!touched) {
			touched = true;
			pointerId = data.pointerId;
			canFire = true;
		}
	}

	public void OnPointerUp(PointerEventData data)
	{
		if (data.pointerId == pointerId) 
		{
			canFire = false;
			touched = false;
		}
	}

	public bool CanFire()
	{
		return canFire;
	}

}
