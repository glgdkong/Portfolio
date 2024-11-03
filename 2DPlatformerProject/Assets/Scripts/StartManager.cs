using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public void RoadSceneStage1()
    {
        SceneManager.LoadScene("Stage1GameScene");
    }
    public void RoadSceneTutorial() 
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void GameExit()
    { }
}
