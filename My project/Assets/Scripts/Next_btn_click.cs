using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Next_btn_click : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject boardscript;
    private GameObject textboxcount;
    private Text scriptbix;

    private int Textbox_Count = 0;
    private GameObject board;
    // Start is called before the first frame update


    public void Textbox1()
    {
        //scriptbix.text="1번내용";
    }
    public void Textbox2()
    {
        //scriptbix.text="2번내용";
    }
    public void Textbox3()
    {
        //scriptbix.text="3번내용";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        scriptbix = GameObject.Find("text_box").GetComponent<Text>();

        boardscript.GetComponent<boardcount>().prev();

        if (eventData.button == PointerEventData.InputButton.Left)
        {

            if (Textbox_Count == 0)
            {
                Textbox1();
                Textbox_Count++;
            }
            else if (Textbox_Count == 1)
            {
                Textbox2();
                Textbox_Count++;
            }
            else if (Textbox_Count == 2)
            {
                Textbox3();
                Textbox_Count++;
            }


        }


        Debug.Log("Mouse Position : " + eventData.position);
        Debug.Log("Mouse Click Count : " + eventData.clickCount);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("soundplay");



        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("soundplay");



        }
    }

}
