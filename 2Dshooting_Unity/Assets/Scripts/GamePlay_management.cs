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

    //Player의 현재 hp를 나타내기 위해 Player의 정보 가져오기
    [SerializeField]
    private GameObject Player;

    private float Player_hp;

    public TextMeshProUGUI Player_Hp_ui_text;

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

        Player_hp = Player.GetComponent<Player_control>().Player_hp;

        Player_Hp_ui_text.SetText($"Hp = {Player_hp}");

    }

    //Player의 사격에 대응해서 현재 잔탄 수 Text를 바꿔주는 함수
    public void Change_bullet_ui_text(float l) {

        Loaded_bullet_ui_text.SetText($"{l}/{Bullet_megazine}");

    }

    //Player의 피격에 반응해 Hp를 표시하는 Text를 바꿔주는 함수
    public void Change_Hp_ui_text(float l) {
        Player_Hp_ui_text.SetText($"Hp = {l}");

        //전달받은 l, 즉 Player_hp가 0이하가 되면 Game_over
        if (l <= 0) {
            Invoke("Game_over", 3f);
        }

    }

    //Game_over Scene으로 넘어가는 함수
    private void Game_over() {
        SceneManager.LoadScene("Game_over");
    }



}
