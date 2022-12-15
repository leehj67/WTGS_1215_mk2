using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveCompleteEventArgs
{
    public GameObject targetObject;
    public Vector3 position;
    public Quaternion quaternion;
}

public class Camera_movement : MonoBehaviour
{

    public static event System.EventHandler<MoveCompleteEventArgs> EventHandler_CameraMoveTargtet;


    public float xmove = 0;  // X�� ���� �̵���
    public float ymove = 0;  // Y�� ���� �̵���
    public float first_distance;
    public float distance;
    public Transform first_transform;

    ///public float rotationSpeed = 0.01f;
    public float SmoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;

    private int toggleView = 3; // 1=1��Ī, 3=3��Ī

    private float wheelspeed = 0.8f;
    private bool Check_wheel_clicked = false;
    Vector2 clickPoint;

    private float init_rot_x;
    private float init_rot_y;

    private bool init_check = false;

    private float camera_rot_x;
    private float camera_rot_y;
    private float camera_rot_z;


    /// <summary>
    /// ī�޶�
    /// </summary>
    public GameObject camera;

    /// <summary>
    /// ���� ��� ������Ʈ
    /// </summary>
    private Transform targetObject;

    /// <summary>
    /// ���� ���� ��ġ
    /// </summary>
    public Transform subTarget;
    public Transform subTarget1;
    public Transform subTarget2;
    public Transform subTarget3;
    public Transform subTarget4;
    public Transform subTarget5;
    public Transform subTarget5_5;

    /// <summary>
    /// �ε巴�� �̵��� ����
    /// </summary>
    public float smoothTime = 0.1f;


    /// <summary>
    /// ī�޶� Ÿ�� ���� ���� �÷���
    /// </summary>
    public static bool IsActive = false;

    /// <summary>
    /// ���� ���� -�� Ŭ���� �ܾƿ�
    /// </summary>
    public float Zoomin = -5;

    /// <summary>
    /// ������Ʈ ũ�⿡ ���� �ܱ�� ���� ���� ������
    /// </summary>
    private Bounds boundsData;
    private bool isBounds = true;

    /// <summary>
    /// �������� ���� �� ���� ī��Ʈ
    /// </summary>
    private int PassCount = 0;
    // Update is called once per frame

    private GameObject View_target;
    public GameObject View_target_1;
    public GameObject View_target_2;

    private bool Seq_start = false;
    private bool mouse_active = false;

    private bool check_menu = false;
    void Start()
    {
        Target_changed_1();
        act0();
        //mouse_active = true;
        //check_menu = true;
    }

    void Update()
    {
        
        if (mouse_active == true&&check_menu==false)
        {
            Debug.Log("on");
            if (Input.GetMouseButton(0))
            {

                xmove += Input.GetAxis("Mouse X"); // ���콺�� �¿� �̵����� xmove �� �����մϴ�.
                ymove -= Input.GetAxis("Mouse Y"); // ���콺�� ���� �̵����� ymove �� �����մϴ�.
                clickPoint = Input.mousePosition;

            }
            transform.rotation = Quaternion.Euler(camera_rot_x + ymove, camera_rot_y + xmove, camera_rot_z); // �̵����� ���� ī�޶��� �ٶ󺸴� ������ �����մϴ�.

            //if (Input.GetMouseButtonDown(2)==true)
            //{
            //    toggleView = 4 - toggleView;
            //    Debug.Log("mouse wheel rotated");
            //}
            if (Input.GetMouseButton(2))
            {
                Vector3 position
                 = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint);

                Vector3 move = position * (Time.deltaTime * 2.0f);

                transform.Translate(move);

            }

            if (toggleView == 3)
            {
                distance -= Input.GetAxis("Mouse ScrollWheel") * wheelspeed;
                if (distance < 10f) distance = 10f;
                if (distance > 100f) distance = 100f;

                Vector3 reverseDistance = new Vector3(0.0f, 0.0f, distance); // ī�޶� �ٶ󺸴� �չ����� Z ���Դϴ�. �̵����� ���� Z ������� ���͸� ���մϴ�.

                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    View_target.transform.position - transform.rotation * reverseDistance,
                    ref velocity,
                    SmoothTime);
            }

        }


        if (IsActive == true && check_menu == false)
        {
            Vector3 targetPosition;

            //���������� �ִٸ� ��ǥ������ �����켱���� �����Ѵ�
            if (subTarget != null && PassCount == 0)
            {
                targetPosition = subTarget.transform.position;
                smoothTime = 1.0f;
            }
            else
            {
                //���������� ���ٸ� bounds üũ �� ��ǥ������ �������� �����Ѵ�
                if (!isBounds)
                    targetPosition = targetObject.TransformPoint(new Vector3(0, 10, Zoomin));
                else
                    targetPosition = new Vector3(boundsData.center.x, boundsData.center.y + boundsData.size.y, boundsData.center.z - boundsData.size.z + Zoomin);
            }

            if (Seq_start == true)
            {
                //������ ������ ��ġ�� �ε巴�� �̵�
                camera.transform.position = Vector3.SmoothDamp(camera.transform.position, targetPosition, ref velocity, smoothTime);
                //camera.transform.position = Vector3.Lerp(camera.transform.position, targetPosition, 0.01f);
                camera.transform.LookAt(targetObject);
                if (Vector3.Magnitude(camera.transform.position - targetPosition) < 0.01)
                {
                    Seq_start = false;
                    Debug.Log("camera move complete");
                    Set_distance();
                    Set_camera_value(this.transform);
                    mouse_active = true;
                }
            }


        }


    }

    public bool Get_menu_check()
    {
        return check_menu;
    }
    public void check_meunu_enabled()
    {
        check_menu = true;
        Debug.Log("check_menu : " + check_menu);
    }
    public void check_meunu_disabled()
    {
        check_menu = false;
        Debug.Log("check_menu : " + check_menu);
    }
    /// <summary>
    /// target ������Ʈ�� ������ �̵��ϴ� �Լ�
    /// </summary>
    /// <param name="target">��ǥ ������Ʈ</param>
    /// <param name="bounds">������Ʈ ũ�⿡ ���� ���� ����</param>
    public void SetTarget(GameObject target, bool bounds = true)
    {

        if (target == null)
            return;
        IsActive = true;
        targetObject = target.transform;

        //bounds�� true�ϰ�� target�� bounds �����͸� ����
        if (bounds)
        {
            Bounds combinedBounds = new Bounds();
            var renderers = target.GetComponentsInChildren<Renderer>();
            foreach (var render in renderers)
            {
                combinedBounds.Encapsulate(render.bounds);
            }

            boundsData = combinedBounds;
            isBounds = true;
        }
        else
        {
            boundsData = new Bounds();
            isBounds = false;
        }

    }
    //ó���� ��ũ��Ʈ ������Ʈ �κ� Ȱ��ȭ �ϱ� ���� �߰�
    public void act0()
    {
        Debug.Log("Camera way 0");
        mouse_active = false;
        subTarget = subTarget1;
        SetTarget(View_target);
        Seq_start = true;
    }

    public void act1()
    {
        Target_changed_2();
        Debug.Log("Camera way 1");
        mouse_active = false;
        subTarget = subTarget2;
        SetTarget(View_target);
        Seq_start = true;
    }

    public void act2()
    {
        Target_changed_1();
        Debug.Log("Camera way 2");
        mouse_active = false;
        subTarget = subTarget3;
        SetTarget(View_target);
        Seq_start = true;
    }
    public void act3() //s3.3
    {
        Debug.Log("Camera way 3");
        mouse_active = false;
        subTarget = subTarget3;
        SetTarget(View_target);
        Seq_start = true;
    }
    public void act4() //s3.3
    {
        Debug.Log("Camera way 4");
        mouse_active = false;
        subTarget = subTarget4;
        SetTarget(View_target);
        Seq_start = true;
    }
    public void act5() //s3.3
    {
        Debug.Log("Camera way 5");
        mouse_active = false;
        subTarget = subTarget5;
        SetTarget(View_target);
        Seq_start = true;
    }

    public void act5_5()
    {
        Debug.Log("Camera way 5_5");
        mouse_active = false;
        subTarget = subTarget5_5;
        SetTarget(View_target);
        Seq_start = true;
    }

    public void Clear()
    {
        smoothTime = 0.3f;
        IsActive = false;
        targetObject = null;
        PassCount = 0;
    }

    public void Set_camera_value(Transform camera_transform)
    {

        camera_rot_x = camera_transform.rotation.eulerAngles.x;
        camera_rot_y = camera_transform.rotation.eulerAngles.y;
        camera_rot_z = camera_transform.rotation.eulerAngles.z;
    }

    public void Reset()
    {
        xmove = 0;
        ymove = 0;
        SetTarget(View_target);
        distance = Vector3.Magnitude(subTarget.transform.position - View_target.transform.position);
        this.transform.position = subTarget.transform.position;
        Debug.Log("RESET Camera position");
    }
    public void Target_changed_1()
    {
        View_target = View_target_1;
    }

    public void Target_changed_2()
    {
        View_target = View_target_2;
    }

    public void Set_distance()
    {
        distance = Vector3.Magnitude(this.transform.position - View_target.transform.position);
        xmove = 0;
        ymove = 0;
    }

}