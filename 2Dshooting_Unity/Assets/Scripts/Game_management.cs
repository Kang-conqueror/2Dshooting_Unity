using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_management : MonoBehaviour
{
    //start_ui를 담을 GameObject 변수
    [SerializeField]
    private GameObject Start_ui;

    public GameObject Game_playing;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //게임을 시작시키는 함수
    public void Game_start() {

        //게임 시작 시, SetActive 이용 start_ui 비활성화 
        Start_ui.SetActive(false);
        //게임 active true, 실행
        Game_playing.SetActive(true);

    }



}
