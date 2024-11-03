using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameManager : MonoBehaviour
{
    private bool isPaused = false; // 일시 정지 여부
    private bool isLoading = false;

    [SerializeField] private PausePopupComponent popup;

    // 이벤트 델리게이트 메소드 원형 선언 (델리게이트 타입 선언)
    public delegate void OnPauseDelegate(); // 일시 정지 이벤트 델리게이트 선언
    public delegate void OnResumeDelegate(); // 일시 정지 해제 이벤트 델리게이트 선언

    // 델리게이트 이벤트 변수 선언
    // * 델리게이트는 꼭 static으로 변수 선언을 할 필요는 없음
    public static OnPauseDelegate onPauseDelegate;
    public static OnResumeDelegate onResumeDelegate;

    public bool IsLoading { get => isLoading; set => isLoading = value; }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& !IsLoading)
        {
            TogglePause();  // 일시 정지 처리 반전 
        }
        if(isPaused && Input.GetKeyDown(KeyCode.Space) )
        {
            SceneManager.LoadScene("StartScene");
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // 일시정지 이벤트 통지 델리게이트 메소드 실행
            popup.OnOpen();
            onPauseDelegate();
        }
        else
        {
            // 일시정지 해제 이벤트 통지 델리게이트 메소드 실행
            popup.OnClose();
            onResumeDelegate();
        }
    }
}
