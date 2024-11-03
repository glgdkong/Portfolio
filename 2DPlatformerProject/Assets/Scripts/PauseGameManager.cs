using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameManager : MonoBehaviour
{
    private bool isPaused = false; // �Ͻ� ���� ����
    private bool isLoading = false;

    [SerializeField] private PausePopupComponent popup;

    // �̺�Ʈ ��������Ʈ �޼ҵ� ���� ���� (��������Ʈ Ÿ�� ����)
    public delegate void OnPauseDelegate(); // �Ͻ� ���� �̺�Ʈ ��������Ʈ ����
    public delegate void OnResumeDelegate(); // �Ͻ� ���� ���� �̺�Ʈ ��������Ʈ ����

    // ��������Ʈ �̺�Ʈ ���� ����
    // * ��������Ʈ�� �� static���� ���� ������ �� �ʿ�� ����
    public static OnPauseDelegate onPauseDelegate;
    public static OnResumeDelegate onResumeDelegate;

    public bool IsLoading { get => isLoading; set => isLoading = value; }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& !IsLoading)
        {
            TogglePause();  // �Ͻ� ���� ó�� ���� 
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
            // �Ͻ����� �̺�Ʈ ���� ��������Ʈ �޼ҵ� ����
            popup.OnOpen();
            onPauseDelegate();
        }
        else
        {
            // �Ͻ����� ���� �̺�Ʈ ���� ��������Ʈ �޼ҵ� ����
            popup.OnClose();
            onResumeDelegate();
        }
    }
}
