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

    //Bullet의 속도를 저장할 변수
    public float Bullet_speed = 10f;

    //Bullet의 연사 속도를 조절할 변수
    public float Bullet_interval = 0.5f;

    //Bullet의 한 탄창당 수를 저장할 변수
    public float Megazine = 10f;

    //Bullet의 재장전 속도를 저장할 변수

    public float Reload_time = 3f;


    //각각 Bullet의 공격력와 체력을 저장할 변수
    //power와 hp는 같아야 함
    public float Bullet_power = 5f;

    public float Bullet_hp = 5f;

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

    //Bullet과 다른 unit과의 충돌 관리 함수
    private void OnTriggerEnter2D(Collider2D other) {
        
        //경계선에 닿았을 시 자동 삭제
        if (other.gameObject.tag == "Border") {
            Destroy(gameObject);
        }

        //Enemy와 닿았을 시 자동 삭제
        if (other.gameObject.tag == "Enemy") {
            Destroy(gameObject);
        }

        //Enemy Bullet과 충돌 시 피해를 서로 주고 받음
        if (other.gameObject.tag == "Enemy_bullet") {

            float E_B = other.gameObject.GetComponent<Enemy_bullet_control>().Bullet_power;
            Bullet_hp -= E_B;
            if (Bullet_hp <= 0) {
                
                Destroy(gameObject);
            }
        }
    }
}
