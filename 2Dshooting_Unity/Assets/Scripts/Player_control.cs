using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_control : MonoBehaviour
{   
    [SerializeField]
    //총알 받아오기
    public GameObject Bullet;
    //

    //Bullet이 Gun에서 발사되도록 하기 위해, 가져오기
    public Transform Gun;
    //

    //Bullet_control 에서 연사 속도, 한 탄창당 총알 수, 재장전 시간을 저장할 변수
    private float B_interval;

    public float B_megazine;

    private float B_reload;
    //

    [SerializeField]
    //현재 탄창에 장전되어있는 총알 수를 저장할 변수
    public float Loaded_bullet;
    //

    //연사 속도 제한을 걸기 위해, Time 단위로 계산
    private float Shoot_time;
    //

    //Regidbody 정보를 받을 변수 선언
    private Rigidbody2D Rigid_body2;
    //

    //Player의 이동벡터를 담을 변수
    private Vector2 Move_vector;
    //

    //이동 속도를 담을 변수 
    public float Move_speed= 5f;
    //

    //Player 체력을 담을 변수
    public float Player_hp = 50f;

    //마우스의 위치를 저장할 변수
    private Vector2 Mousepos;

    private Vector2 angle2;
    //

    //메인 카메라 받기
    private Camera Cam;
 

    // Start is called before the first frame update
    void Start()
    {   
        //게임 시작 시, rigidbody 2d component 받아오기
        Rigid_body2 = gameObject.GetComponent<Rigidbody2D>();

        //시작 시 main camera 받기
        Cam = Camera.main;

        //시작 시 Bullet의 연사속도 받아오기
        B_interval =  Bullet.GetComponent<Bullet_control>().Bullet_interval;

        B_megazine = Bullet.GetComponent<Bullet_control>().Megazine;

        Loaded_bullet = B_megazine;

        B_reload = Bullet.GetComponent<Bullet_control>().Reload_time;

        //연사 속도 제한을 걸기 위해 사격 시간을 저장할 변수        
        Shoot_time = Time.time - B_interval;

        //Gun의 정보를 가져오기
        Gun = transform.Find("Gun");

        Start_shoot_bullet();
    }

    public void Start_shoot_bullet() {
        StartCoroutine("Shoot_bullet");
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
        //Shoot_bullet();
    
        //Player 이동구현

        Move_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Rigid_body2.velocity = Move_vector.normalized * Move_speed;
        //

    }

    //Bullet 발사 구현
    IEnumerator Shoot_bullet() {

        while(true) {

            if (Input.GetMouseButton(0)) {

                if (Time.time - Shoot_time >= B_interval) {
                    //Instantiate로 Bullet 생성
                    Instantiate(Bullet, Gun.position, Gun.rotation);    

                    Loaded_bullet -= 1;
                    //사격 시 Time을 저장해 연사 조절용으로 사용  
                    Shoot_time = Time.time;  

                    //장전된 총알 수 Text 변경하는 함수
                    GamePlay_management.instance.Change_bullet_ui_text(Loaded_bullet);

                    //탄창을 다 소모하면, Invoke를 이용해 재장전 시간이 지난 후에 탄창 채우기
                    if (Loaded_bullet <= 0) {
                        
                        FindObjectOfType<GamePlay_management>().Loaded_bullet_ui_text.SetText("Reloding");

                        yield return new WaitForSeconds(B_reload);
                        Reload();
                        }
                    }               
            }
            //수동으로 재장전을 하는 기능
            else if (Input.GetKeyDown(KeyCode.R)) {
                
                FindObjectOfType<GamePlay_management>().Loaded_bullet_ui_text.SetText("Reloding");

                yield return new WaitForSeconds(B_reload);
                Reload();
            }
            yield return new WaitForSeconds(0f);
        }
    }

    //재장전 함수
    void Reload() {
        Loaded_bullet = B_megazine;
        GamePlay_management.instance.Change_bullet_ui_text(B_megazine);
    }



    //Player와 Enemy_bullet과의 충돌 판정
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Enemy_bullet") {
            float E_B = other.gameObject.GetComponent<Enemy_bullet_control>().Bullet_hp; 
            Player_hp -= E_B;
            if (Player_hp <= 0) {
                Destroy(gameObject);
            }

        }
    }












}
