using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_spawn_control : MonoBehaviour
{   

    public GameObject[] Enemies;

    public Transform[] Spawn_position;


    [SerializeField]
    private float Spawn_interval;


    void Start(){
        Start_enemy_spawn();
    }

    //Enemy 생성 함수 Coroutine 실행 및 정지
    public void Start_enemy_spawn(){
        StartCoroutine("Enemy_spawn");
    }

    public void Stop_enemy_spwan() {
        StopCoroutine("Enemy_spwan");
    }   


    //Enemy 생성을 구현한 함수
    IEnumerator Enemy_spawn() {

        //게임 시작하자마자 바로 생성되는 것을 막기 위해, 3초 대기시간 부여
        yield return new WaitForSeconds(3f);

        while (true) {

            //각각 Random하게 생성할 적과, 생성할 위치를 정함
            int Enemy_idx = Random.Range(0, Enemies.Length);

            //0~4는 Top, 5~7은 Left, 8~10은 Right 부분에서 나온다
            int Spawn_position_idx = Random.Range(0, Spawn_position.Length);

            //Instantiate를 위해 Spawn position의 벡터값 구하기
            Vector2 Spawn_pos = new Vector2(
                Spawn_position[Spawn_position_idx].position.x, Spawn_position[Spawn_position_idx].position.y);

            GameObject E = Instantiate(Enemies[Enemy_idx], Spawn_pos, Quaternion.identity);
            
            Rigidbody2D R2 = E.GetComponent<Rigidbody2D>();
            Enemy_control E_c = E.GetComponent<Enemy_control>();

            //Spawn 위치가 위쪽일 때
            if (Spawn_position_idx <5) {
                 
                R2.velocity = new Vector2(0, E_c.Enemy_speed * (-1));
            }

            //Spawn 위치가 왼쪽일 때
            else if (Spawn_position_idx >= 5 && Spawn_position_idx < 8) {
                                 
                R2.velocity = new Vector2(E_c.Enemy_speed, 0);
                
            }

            //Spawn 위치가 오른쪽일 때
            else if (Spawn_position_idx >= 8) {
               
                R2.velocity = new Vector2(E_c.Enemy_speed * (-1), 0);  

            }
            
            yield return new WaitForSeconds(Spawn_interval);
           
        }
         
    }













}
