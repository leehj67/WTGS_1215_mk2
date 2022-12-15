using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class Script_controller : MonoBehaviour
{
    
    public int btnCount = 0;

    public Text text;   //��ũ��Ʈ, ���Ŀ� �ڵ����� �������� ������� �ϴ°� ���� ��, Ư��, UI���� �����ϴ� �͵��� UI_study�� ���� �����ϵ��� ������ ��
    public GameObject TextPanel;    //�ִϸ��̼ǿ� ��ũ��Ʈ �г�

    public bool FadeOut = false;
    GameObject Fader;
    float script_time_now;


    private float Time_limit;
    private bool status_UI_script_auto;
    private bool First_status_UI;

    [Multiline(3)]
    public List<string> textList = new List<string>();
    //1�� public���� �׽�Ʈ �� ���� csv���Ϸ� �����ؼ� �Ҿ���� ������� �����ϱ�

    // Start is called before the first frame update
    void Start()
    {
        //Read_txt();
        status_UI_script_auto = false;
        First_status_UI = true;

        Time_limit = 20f;
        script_time_now = Time_limit;

        //textList = this.GetComponent<Manager_script>().textList;
        //if (Manager_audio.instance != null) , BGM �κ�
           

        if (Manager_scene.instance != null)
        {
            status_UI_script_auto = Manager_scene.instance.Status_Check_script_auto_over();

        }
        //Fader = GameObject.Find("Fader");   //�� ������ �ִ� ��Ʈ�� �г�, ���̴��� �� ������ �ϳ��� �־��ִ°ɷ� 1017

    }

    // Update is called once per frame
    void Update()
    {
        //�н� �ڵ� ���� ���
        if (Manager_scene.instance!= null)
        {
            status_UI_script_auto = Manager_scene.instance.Status_Check_script_auto_over();
           
        }
        if (status_UI_script_auto == true && First_status_UI == true)
        {
            Timer_set();
            First_status_UI = false;
            //Debug.Log("auto button timer start" + script_time_now);
        }
        if (status_UI_script_auto == true)
        {
            script_time_now -= Time.deltaTime;
            if (script_time_now < 0)
            {
                NextBtn();
                //Debug.Log("timer done");
            }
        }
    }

    public void NextBtn()
    {
        Debug.Log("NEXTBUTTON");
        if (status_UI_script_auto == true)
        {
            Timer_set();
        }
        btnCount++;
        ScriptCount();
        if (TextPanel != null)
            //next, prev ������ �� �ִϸ��̼� ��� �Ǵ� �κ�
        {
            TextPanel.GetComponent<Scriptopen>().OpenPanel();
        }
    }
    public void PrevBtn()
    {
        Debug.Log("PREVBUTTON");
        if (status_UI_script_auto == true)
        {
            Timer_set();
        }
        btnCount--;
        ScriptCount();
        if (TextPanel != null)
        {
            TextPanel.GetComponent<Scriptopen>().OpenPanel();
        }
    }
    public void ScriptCount()
    {
        if (btnCount >= textList.Count)
        {
            if (Fader != null && FadeOut == true)
            {
                Fader.GetComponent<Fader>().FadeOut(1);
                Invoke("InvokeNextScene", 1f);
            }
            else
                this.GetComponent<Dual_scene_loader>().LoadNextScene();
        }
        else if (btnCount < 0)
        {
            this.GetComponent<Dual_scene_loader>().LoadPrevScene();
        }
        else
        {
            Invoke("InvokeAct", 0.2f);
        }
    }
    void InvokeAct()
    {
        text.text = textList[btnCount];
    }
    void InvokeNextScene()
    {
        this.GetComponent<Dual_scene_loader>().LoadNextScene();
    }

    void Timer_set()
    {
        script_time_now = Time_limit;
        Debug.Log("timer start" + script_time_now);
    }
     void Read_txt()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/유니티텍스트.txt");

        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_String = sr.ReadLine();
            if(data_String == null)
            {
                endOfFile = true;
                break;
            }
            var data_values = data_String.Split('.');
            for(int i = 0; i < data_values.Length; i++)
            {
                //Debug.Log("v: " + i.ToString() + " " + data_values[i].ToString());
                textList[i]=data_values[i];
                Debug.Log(textList[i]);
            }
        }

    }

}
