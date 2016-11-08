using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;  
using UnityEngine.UI;

public class ChangeColorOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public Text text;

	public void OnPointerEnter(PointerEventData eventData) {
		text.color = Color.white;
	}

	public void OnPointerExit(PointerEventData eventData) {
		text.color = Color.black;
	}
}
