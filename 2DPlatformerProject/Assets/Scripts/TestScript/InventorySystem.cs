using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // 가상 카메라
    [SerializeField] private CinemachineVirtualCamera vCamera;
    // 인벤토리 UI
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private PauseGameManager pauseManager;

    [SerializeField] private Transform lookCameraPosition; // lookAt을 참조하고 있는 게임 오브젝트 Transform
    [SerializeField] private Transform cameraFollowTarget; // 기본위치를 가지고 있는 GameObject

    // 이벤트 델리게이트 메소드 원형 선언 (델리게이트 타입 선언)
    public delegate void OpenInventory(); // 일시 정지 이벤트 델리게이트 선언
    public delegate void CloseInventory(); // 일시 정지 해제 이벤트 델리게이트 선언

    // 델리게이트 이벤트 변수 선언
    // * 델리게이트는 꼭 static으로 변수 선언을 할 필요는 없음
    public static OpenInventory openInventory;
    public static CloseInventory closeInventory;

    // 일시정지 변수
    private bool isPaused = false;
    private bool inventoryPopupOpen;

    public bool InventoryPopupOpen => inventoryPopupOpen;

    void Update()
    {
        if (isPaused) return;


        OpenInventoryPopup();
       
    }

    private void OpenInventoryPopup()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !inventoryPopupOpen)
        {
            // 인벤토리 UI 열기
            inventoryUI.OpenUI();
            // 인벤토리 UI 팝업 bool 변수 트루
            inventoryPopupOpen = true;
            pauseManager.InventoryPopupOpen = inventoryPopupOpen;
            OpenInventoryMethod();
        }
    }
    protected virtual void OnEnable()
    {
        // 현재 컴포넌트는 게임 일시 정지 알림(이벤트)를 받을 수 있게 델리게이트 메소드를 등록함
        PauseGameManager.onPauseDelegate += OnPause;
        // 현재 컴포넌트는 게임 일시 정지 해제 알림(이벤트)를 받을 수 있게 델리게이트 메소드를 등록함
        PauseGameManager.onResumeDelegate += OnResume;
    }
    protected virtual void OnDisable()
    {
        // 현재 컴포넌트는 게임 일시 정지 알림(이벤트)를 받을 수 있게 델리게이트 메소드를 등록을 해제함
        PauseGameManager.onPauseDelegate -= OnPause;
        // 현재 컴포넌트는 게임 일시 정지 해제 알림(이벤트)를 받을 수 있게 델리게이트 메소드를 등록을 해제함
        PauseGameManager.onResumeDelegate -= OnResume;
    }
    void OpenInventoryMethod()
    {
        // 빈 GameObject를 우측으로 이동
        lookCameraPosition.position = cameraFollowTarget.position + new Vector3(3.5f, 0, 0); // 오른쪽으로 2 유닛 이동
        openInventory();
    }
    void CloseInventoryMethod()
    {
        // 빈 GameObject를 플레이어 위치로 되돌림
        lookCameraPosition.position = cameraFollowTarget.position; // 원래 위치로 돌아가기
        closeInventory();
    }
    private void OnPause()
    {
        isPaused = true;
    }
    private void OnResume()
    {
        isPaused = false;
    }
}
