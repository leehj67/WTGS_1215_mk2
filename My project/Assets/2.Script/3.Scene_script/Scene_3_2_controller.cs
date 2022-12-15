using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_3_2_controller : MonoBehaviour
{
    public GameObject PC_Image;
    public GameObject[] PC_Image_Array;
    int PostCount;

    // Start is called before the first frame update
    void Start()
    {

        //PC_Image_Array = PC_Image.gameObject.GetComponentsInChildren<Transform>();
        // PC_Image_Array = GameObject.FindGameObjectsWithTag("PC_Sprite");
        PC_Image.SetActive(false);

        Invoke("PC_ON", 2f);    // 5초 뒤에 해당 오브젝트 화면에 투사
    }
    private void PC_ON()
    {
        PC_Image.SetActive(true);
        for (int i = 0; i < PC_Image_Array.Length; i++)
        {
            PC_Image_Array[i].gameObject.SetActive(false);
        }
        PC_Image_Array[0].gameObject.SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {
        int BtnCount = gameObject.GetComponent<Script_controller>().btnCount;
        if (PostCount != BtnCount)
            for (int i = 0; i < PC_Image_Array.Length; i++)
            {
                PC_Image_Array[i].gameObject.SetActive(false);
            }
        //PC_Image_Array[0].gameObject.SetActive(true);
        PC_Image_Array[BtnCount].SetActive(true);
        PostCount = BtnCount;
    }
}
