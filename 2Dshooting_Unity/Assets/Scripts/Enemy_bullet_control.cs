using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_bullet_control : MonoBehaviour
{
    private Rigidbody2D R_b2;

    //Bullet의 hp, speed 변수 
    //power와 hp는 같아야 함

    public float Bullet_hp;

    public float Bullet_power;

    public float Bullet_speed;

    private Vector2 Angle;

    // Start is called before the first frame update

    void Start()
    {   
        //생성 시 Rigidbody 2d 가져오기
        R_b2 = gameObject.GetComponent<Rigidbody2D>();
        //Player의 위치정보 가져오기
        GameObject p = GameObject.Find("Player");     
        Transform T = p.transform;

        //Bullet 생성 시 Player쪽으로 이동하도록 Vector 생성
        Angle = new Vector2(
            T.position.x - transform.position.x, T.position.y - transform.position.y);

    }

    // Update is called once per frame
    //Bullet 이동
    void Update()
    {
        //총알 발사 구현 
        R_b2.velocity = Angle.normalized * Bullet_speed;
        
    }

    //충돌 처리 구현
    private void OnTriggerEnter2D(Collider2D other) {
        
        //경계선에 닿았을 시 자동 삭제
        if (other.gameObject.tag == "Border") {
            Destroy(gameObject);
        }

        //Player와 충돌 시 피해를 입히고 없어짐
        if (other.gameObject.tag == "Player") {
            //피해를 입히는 코드는 Player_control script 에 있음

            Destroy(gameObject);
        }

        //Player의 Bullet과 충돌 시 판정
        //총알과 총알끼리의 충돌 판정이 제대로 작동하지 않음.
        if (other.gameObject.tag == "Bullet") {
            float B = other.gameObject.GetComponent<Bullet_control>().Bullet_power;
            Bullet_hp -= B;
            if (Bullet_hp <= 0) {
                Destroy(gameObject);
            }

        }


        
    }
}
