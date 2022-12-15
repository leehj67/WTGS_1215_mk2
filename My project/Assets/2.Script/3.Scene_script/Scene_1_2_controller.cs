using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene_1_2_controller : MonoBehaviour
{
    public GameObject Camera;
    public GameObject MainObject;
    public GameObject Metalball;
    public GameObject Metalball_hold;
    public GameObject Belt;

    public GameObject Text3D1;
    public GameObject Text3D2;
    public GameObject Text3D3;
    public GameObject Text3D4;
    public GameObject Text3D6;

    public GameObject Pallete;


    private Text text;
    bool toggle = true;
    int nowCount;
    int postCount;
    bool Prev_Status = false;
    private int phase = 0;

    private List<string> anim_list = new List<string>();
    // Start is called before the first frame update

    private Animation anim;
    void Start()
    {
        anim = MainObject.GetComponent<Animation>();
        anim.Play("WTG_rotation_animation");
    }
    void StartAct()
    {

    }

    void Act1()
    {
        //if (Prev_Status == true)
        //{
        //    Prev_Status = false;
        //    MainObject.GetComponent<Animation>().Stop();
        //    Pallete.transform.localPosition = new Vector3(0.2891898f, 0.9893606f, -0.1129792f);
        //    Belt.GetComponent<Animation>().Stop();
        //}
        //MainObject.GetComponent<Animation>().Stop();
        //Pallete.transform.localPosition = new Vector3(0.2891898f, 0.9893606f, -0.1129792f);

        Debug.Log("act1");
    }
    void Act2()
    {
        //if (Prev_Status == true)
        //{
        //    Prev_Status = false;
        //    Camera.GetComponent<Camera_movement>().act2();
        //}

        //Camera.GetComponent<Animation>().Stop();
        //this.GetComponent<NarrationController>().Audio.clip = this.GetComponent<NarrationController>().AudioFiles[1];
        //this.GetComponent<NarrationController>().Audio.Play();
        //Belt.GetComponent<Animation>().Play("belt_rotation");

        //MainObject.GetComponent<Animation>().Play("S3.1_act_1");
        Camera.GetComponent<Camera_movement>().act1();
        Debug.Log("act2");
    }
    void Act3()
    {
        anim.Play("WTG_boxopen");
        Camera.GetComponent<Camera_movement>().act2();
        Debug.Log("act3");

    }
    void Act4()
    {
        anim.Play("WTG_blade_example");
        Debug.Log("act4");

    }
    void Act5()
    {
       
        Debug.Log("act5");
    }
    void Act6()
    {
       
        Debug.Log("act6");
    }
    void Act7()
    {
       
        Debug.Log("act7");
       
    }
    void Act8()
    {
        Debug.Log("act8");
    }

    //애니메이션 재생하면서 오브젝트 물리엔진 일시정지 및 활성화
    void KineticDisable()
    {
        Transform AllMetalball = Metalball.GetComponentInChildren<Transform>();
        foreach (Transform child in AllMetalball)
        {
            child.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    void KineticEnable()
    {
        Transform AllMetalball = Metalball.GetComponentInChildren<Transform>();
        foreach (Transform child in AllMetalball)
        {
            child.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        BtnCountToggle();
        if (gameObject.GetComponent<Script_controller>().btnCount == 0 && toggle)
        {
            Act1();
            toggle = false;
        }
        if (gameObject.GetComponent<Script_controller>().btnCount == 1 && toggle)
        {
            Act2();
            toggle = false;
        }
        if (gameObject.GetComponent<Script_controller>().btnCount == 2 && toggle)
        {
            Act3();
            toggle = false;
        }
        if (gameObject.GetComponent<Script_controller>().btnCount == 3 && toggle)
        {
            Invoke("Act4", 0f);
            //Invoke("Act6", 29f);
            toggle = false;
        }
        if (gameObject.GetComponent<Script_controller>().btnCount == 4 && toggle)
        {
            Invoke("Act7", 0f);
            Invoke("Act8", 3f);
            toggle = false;
        }
    }

    void BtnCountToggle()
    {
        nowCount = gameObject.GetComponent<Script_controller>().btnCount;
        if (nowCount != postCount)
        {
            toggle = true;

            if (nowCount < postCount)
            {
                Prev_Status = true;
            }

        }
        postCount = nowCount;
    }

}
