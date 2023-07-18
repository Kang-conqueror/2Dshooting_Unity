using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_control : MonoBehaviour
{
    //Enemy의 속도, 체력 정보 기입 변수
    public float Enemy_speed;

    public float Enemy_hp;

    //Enemy가 사용하는 Bullet 저장할 변수
    public GameObject E_bullet;

    //Enemy의 Bullet 발사 interval
    public float E_B_interval = 3f;

    //Sprite, SpriteRenderer, Rigidbody 2d 받기
    private SpriteRenderer S_r;

    public Sprite[] Sprites;

    public Rigidbody2D Rigid_body2;

    // Start is called before the first frame update
    //Start시 coroutine 실행
    void Start() {
        //Coroutine 실행
        Start_shoot_e_bullet();

        //Rigidbody 2d와 SpriteRenderer 가져오기
        Rigid_body2 = gameObject.GetComponent<Rigidbody2D>();
        S_r = gameObject.GetComponent<SpriteRenderer>();
    }


    public void Start_shoot_e_bullet() {
        StartCoroutine("Shoot_e_bullet");
    }



    IEnumerator Shoot_e_bullet(){

        yield return new WaitForSeconds(1.5f);

        while (true) {
            Instantiate(E_bullet, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(E_B_interval);
        }

    }





    


    //Sprite를 변경해주는 함수
    void Return_sprites() {
        S_r.sprite = Sprites[0];
    }

    //Enemy의 충돌 처리
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Bullet") {

            float Bullet = other.gameObject.GetComponent<Bullet_control>().Bullet_power;

            Enemy_hp -= Bullet;

            S_r.sprite = Sprites[1];
            Invoke("Return_sprites", 0.2f);

            if (Enemy_hp <= 0) {
                Destroy(gameObject);
            }
        }
        
        if (other.gameObject.tag == "Enemy_border") {
            Destroy(gameObject);
        }
    }
}
