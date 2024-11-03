using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePopupComponent : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void OnOpen()
    {
        panel.gameObject.SetActive(true);
    }

    public void OnClose()
    {
        panel.gameObject.SetActive(false);
    }
}
