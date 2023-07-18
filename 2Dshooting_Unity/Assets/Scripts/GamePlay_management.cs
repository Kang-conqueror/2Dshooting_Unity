using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GamePlay_management : MonoBehaviour
{   

    //현재 잔탄 수를 표시할 UI를 위해 Bullet의 정보 가져오기
    [SerializeField]

    private GameObject Bullet;

    private float Bullet_megazine;

    public TextMeshProUGUI Loaded_bullet_ui_text;

    public static GamePlay_management instance = null;



    void  Awake() {
        if (instance == null) {
            instance = this;
        }
    }


    void Start() {

        //현재 Bullet의 한 탄창당 총알 수 가져오기
         
        Bullet_megazine = Bullet.GetComponent<Bullet_control>().Megazine;

        Loaded_bullet_ui_text.SetText($"{Bullet_megazine}/{Bullet_megazine}");

    }

    //Player의 사격에 대응해서 현재 잔탄 수 Text를 바꿔주는 함수
    public void Change_bullet_ui_text(float l) {

        Loaded_bullet_ui_text.SetText($"{l}/{Bullet_megazine}");

    }









}
