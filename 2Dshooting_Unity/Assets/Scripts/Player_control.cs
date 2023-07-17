using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_control : MonoBehaviour
{   
    [SerializeField]
    //총알 받아오기
    public GameObject Bullet;


    //Regidbody 정보를 받을 변수 선언
    private Rigidbody2D Rigid_body2;

    //Player의 이동벡터를 담을 변수
    private Vector2 Move_vector;

    //이동 속도를 담을 변수 
    public float Move_speed= 5f;

    //마우스의 위치를 저장할 변수
    private Vector2 Mousepos;

    private Vector2 angle2;

    //메인 카메라 받기
    private Camera Cam;

    //Bullet_control에서 가져올 Bullet_speed를 저장할 변수
    // private float B_s;

    // Start is called before the first frame update
    void Start()
    {   
        //게임 시작 시, rigidbody 2d component 받아오기
        Rigid_body2 = gameObject.GetComponent<Rigidbody2D>();

        //시작 시 main camera 받기
        Cam = Camera.main;

        // B_s = Bullet.GetComponent<Bullet_control>().Bullet_speed;
    }

    // Update is called once per frame
    void Update()
    {   

        //이 함수를 통해 카메라에서 마우스가 위치한 좌표를 얻어낼 수 있다
        //ScreenToWolrdPoint를 통해 스크린상의 좌표를 World상의 좌표로 변환해서 준다.
        //이래야 게임 내에서 좌표 계산을 할 수 있다
        Mousepos = Cam.ScreenToWorldPoint(Input.mousePosition);
       
        //atan 없이 마우스 좌표와 각도 따오기
        angle2 = Mousepos -  (Vector2)transform.position;

        //player을 회전시키는 문장
        transform.right = angle2.normalized;
        //
        

        //Bullet 발사 구현
        if (Input.GetMouseButtonDown(0)) {

            //Instantiate로 Bullet 생성

            Instantiate(Bullet, transform.position, transform.rotation);
            // Vector2 Bullet_vec = new Vector2(angle2.x, angle2.y);
            // Bullet.GetComponent<Rigidbody2D>().velocity = Bullet_vec.normalized * B_s;
            
        }
        //


        //Player 이동구현

        Move_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Rigid_body2.velocity = Move_vector.normalized * Move_speed;
        //

    }
}
