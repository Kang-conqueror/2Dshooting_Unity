using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_control : MonoBehaviour
{
    //Regidbody 정보를 받을 변수 선언
    private Rigidbody2D Rigid_body2;

    //마우스의 위치를 저장할 변수
    private Vector2 Mousepos;

    //발사대의 각도를 조정할 변수
    private float angle;

    //회전 속도를 제어할 함수
    public float rotation_speed = 10f;

    // Start is called before the first frame update
    void Start()
    {   
        //게임 시작 시, rigidbody 2d component 받아오기
        Rigid_body2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   

        //이 함수를 통해 카메라에서 마우스가 위치한 좌표를 얻어낼 수 있다
        //ScreenToWolrdPoint를 통해 스크린상의 좌표를 World상의 좌표로 변환해서 준다.
        //이래야 게임 내에서 좌표 계산을 할 수 있다
        Mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Mathf.Atan2(y, x)를 통해 tan(y/x)값을 구할 수 있다.
        //이를 통해 각도값을 만들어줄 것이다. 참고로 Atan2는 Radian 값을 반환한다
        angle = Mathf.Atan2(Mousepos.y - transform.position.y, Mousepos.x - transform.position.x);

        //player을 회전시키는 문장

        //angle 값에 Mathf.Rad2Deg 를 곱해 Radian 값을 degree 값을 바꿔준다.
        //이 degree 값을, Quanternion.AngleAxis에 넣어 회전시켜준다
        //뒤에 넣는 벡터는 회전축인데, 우린 탑뷰이므로 foward를 넣어준다
        //transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);


        //회전 속도를 제어할 수 있는 코드
        transform.rotation = Quaternion.Lerp(transform.rotation, 
            Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward), rotation_speed *Time.deltaTime);


    }
}
