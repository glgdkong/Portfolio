using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathHandler : GameManager
{
    // TMP 배열 참조
    [SerializeField] private TextMeshProUGUI deathCanvas;


    // Update is called once per frame
    protected void Update()
    {
        if (playerHpControl.IsDead)
        {
            pauseGameManager.IsLoading = true;
            deathCanvas.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                playerHp = 0;
                animator.SetTrigger("Loading");
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}
