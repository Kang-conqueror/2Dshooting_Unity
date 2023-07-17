using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_control : MonoBehaviour
{

    private Rigidbody2D Rigid_body2;

    //메인 카메라 받기
    private Camera Cam;

    //마우스의 위치를 저장할 변수
    private Vector2 Mousepos;

    private Vector2 angle2;

    public float Bullet_speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Rigid_body2 = gameObject.GetComponent<Rigidbody2D>();

        Cam = Camera.main;

        Mousepos = Cam.ScreenToWorldPoint(Input.mousePosition);

        //atan 없이 마우스 좌표와 각도 따오기
        angle2 = new Vector2(transform.position.x - Mousepos.x, transform.position.y - Mousepos.y);
    }

    // Update is called once per frame
    void Update()
    {
        
        
       
        
        Rigid_body2.velocity = (-angle2).normalized * Bullet_speed;


    }
}
