using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.UI;
using UnityEngine.UI;
public class popuihoverscript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Text texthover;
    public Text textthis;

    public void OnPointerClick(PointerEventData eventData)
    {




    }
    public void OnPointerExit(PointerEventData eventData)
    {
        texthover.text = "";
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        texthover.text = textthis.text;

    }
}
