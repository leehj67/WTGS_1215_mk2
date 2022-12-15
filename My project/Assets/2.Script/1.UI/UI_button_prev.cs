using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_button_prev : MonoBehaviour, IPointerClickHandler
{
   
    private GameObject SceneController;
  
    // Start is called before the first frame update
    void Start()
    {
        SceneController = GameObject.FindGameObjectWithTag("Scene_controller");
        //�� ���� ��ũ��Ʈ ��Ʈ�ѷ��� �ε带 �ؾ���
    }
   
    //Update is called once per frame
    //void Update()
    //{

    //}

    public void OnPointerClick(PointerEventData eventData)
    {
        BtnAct();
    }

    public void BtnAct()
    {
        SceneController.GetComponent<Script_controller>().PrevBtn();
        Debug.Log("prev button click");
    }
}
