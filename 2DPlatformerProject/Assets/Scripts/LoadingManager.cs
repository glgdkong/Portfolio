using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static int sceneIndex;
    [SerializeField] private float waitTime;
    private float time = 0;

    private void Update()
    {
        time += Time.deltaTime;
        if(time >= waitTime)
        {
            SceneManager.LoadScene(sceneIndex);
            time = 0;
        }
    }
}
