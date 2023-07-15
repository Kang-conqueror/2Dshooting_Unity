using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//dropdown 사용 위해 tmpro 받아오기
using TMPro;


public class Start_video_control : MonoBehaviour
{
    //resolution 에 지원하는 해상도가 담겨져있음. 우선 resolution 형태의 인자를 받을 
    //list를 생성하자
    List<Resolution> resolutions = new List<Resolution>();

    //Dropdown을 담을 변수 생성
    public TMP_Dropdown resolution_dropdown;

    //dropdown의 value값을 담을 변수
    int Dropdown_num;

    //resolution list의 idx를 관리할 변수 선언
    int option_num = 0;

    //toggle을 담을 변수
    public Toggle Fullscreen_toggle;

    //Fullscreen 상태를 확인하기 위해, FullScreenMode 형 변수 선언
    FullScreenMode Screen_mode;


    // Start is called before the first frame update
    //맨 처음 게임을 시작할 때 dropdown에 해상도 정보 넣기
    void Start()
    {
        
        //Screen.resolution 으로 지원 해상도를 받아 list에 add
        resolutions.AddRange(Screen.resolutions);
        
        //먼저, dropdown에 있는 option들을 지워주기
        resolution_dropdown.options.Clear();

        //resolution list에 담겨있는 해상도들을 foreach로 전부 접근해 dropdown에 넣기
        foreach(Resolution video in resolutions) {

            //optiondata형 변수 생성하기
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();

            //optiondata의 text에 각각 가로, 세로, hz 넣어주기
            option.text = video.width + " x " + video.height + ", " + video.refreshRateRatio;

            //dropdown에 optiondata 추가해주기
            resolution_dropdown.options.Add(option);
            
            //현재 모니터의 해상도에 맞는 값을 맨 처음 dropdown 값으로 띄우기 위해 idx구하기
            if(video.width == Screen.width && video.height == Screen.height) {

                //원래 모니터의 해상도와 같은 값을 가지는 원소의 idx 저장하기
                resolution_dropdown.value = option_num;
            }

            //
            option_num += 1;

        }
        //dropdown 새로고침
        resolution_dropdown.RefreshShownValue();

        //Fullscreen 체크 유무를 넘겨주는 것 같은데?
        //게임 시작시, Screen의 fullScreenMode가 괄호 안과 같으면 true, 아니면 false 돌려주기
        Fullscreen_toggle.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    //dropdown 의 value(화살표 안내리면 보이는 값)의 변경을 반영해줄 함수
    //dropdown의 On Value Changed가 이 함수를 참조하게 해, value값이 변경될 떄마다
    //그 value의 idx값을 돌려준다.
    public void Dropdown_change(int x) {

        //x에 인자로 변경된 value의 idx값을 받고, 그 값을 저장한다
        Dropdown_num = x;

    }

    //Fullscreen 의 유무를 확인할 함수
    public void Fullscreen_toggle_check(bool isFull) {

        //? 연산자를 사용해 참이면 왼쪽, 아니면 오른쪽 값을 넘겨준다
        Screen_mode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

        

    }

    //Change 버튼을 눌렀을 때 작동시킬 함수
    //현재 해상도의 가로, 세로, FullScreen 유무 값을 Screen.SetResolution에 넣어준다
    public void Change_button() {
        Screen.SetResolution(resolutions[Dropdown_num].width,
        resolutions[Dropdown_num].height,
        Screen_mode);
    }

}
