using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // ���� ī�޶�
    [SerializeField] private CinemachineVirtualCamera vCamera;
    // �κ��丮 UI
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private PauseGameManager pauseManager;

    [SerializeField] private Transform lookCameraPosition; // lookAt�� �����ϰ� �ִ� ���� ������Ʈ Transform
    [SerializeField] private Transform cameraFollowTarget; // �⺻��ġ�� ������ �ִ� GameObject

    // �̺�Ʈ ��������Ʈ �޼ҵ� ���� ���� (��������Ʈ Ÿ�� ����)
    public delegate void OpenInventory(); // �Ͻ� ���� �̺�Ʈ ��������Ʈ ����
    public delegate void CloseInventory(); // �Ͻ� ���� ���� �̺�Ʈ ��������Ʈ ����

    // ��������Ʈ �̺�Ʈ ���� ����
    // * ��������Ʈ�� �� static���� ���� ������ �� �ʿ�� ����
    public static OpenInventory openInventory;
    public static CloseInventory closeInventory;

    // �Ͻ����� ����
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
            // �κ��丮 UI ����
            inventoryUI.OpenUI();
            // �κ��丮 UI �˾� bool ���� Ʈ��
            inventoryPopupOpen = true;
            pauseManager.InventoryPopupOpen = inventoryPopupOpen;
            OpenInventoryMethod();
        }
    }
    protected virtual void OnEnable()
    {
        // ���� ������Ʈ�� ���� �Ͻ� ���� �˸�(�̺�Ʈ)�� ���� �� �ְ� ��������Ʈ �޼ҵ带 �����
        PauseGameManager.onPauseDelegate += OnPause;
        // ���� ������Ʈ�� ���� �Ͻ� ���� ���� �˸�(�̺�Ʈ)�� ���� �� �ְ� ��������Ʈ �޼ҵ带 �����
        PauseGameManager.onResumeDelegate += OnResume;
    }
    protected virtual void OnDisable()
    {
        // ���� ������Ʈ�� ���� �Ͻ� ���� �˸�(�̺�Ʈ)�� ���� �� �ְ� ��������Ʈ �޼ҵ带 ����� ������
        PauseGameManager.onPauseDelegate -= OnPause;
        // ���� ������Ʈ�� ���� �Ͻ� ���� ���� �˸�(�̺�Ʈ)�� ���� �� �ְ� ��������Ʈ �޼ҵ带 ����� ������
        PauseGameManager.onResumeDelegate -= OnResume;
    }
    void OpenInventoryMethod()
    {
        // �� GameObject�� �������� �̵�
        lookCameraPosition.position = cameraFollowTarget.position + new Vector3(3.5f, 0, 0); // ���������� 2 ���� �̵�
        openInventory();
    }
    void CloseInventoryMethod()
    {
        // �� GameObject�� �÷��̾� ��ġ�� �ǵ���
        lookCameraPosition.position = cameraFollowTarget.position; // ���� ��ġ�� ���ư���
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
