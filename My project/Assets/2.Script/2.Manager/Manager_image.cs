using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_image : MonoBehaviour
{
    public static Manager_image instance = null;   //�ν��Ͻ� �̸��� ��ũ��Ʈ �̸��� �����ϰ�

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
    


    public Texture2D Get_arrow_image()
    {
        return this.GetComponent<Image_mouse>().Arrow_image();
    }

    public Texture2D Get_hand_image()
    {
        return this.GetComponent<Image_mouse>().Hand_image();
    }
}
