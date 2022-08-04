using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int _myScore;
    public int _stageNum;
    public int _continueNum;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start() 
    {
        Debug.Log(CheckThisSceneName());    
    }

    private string CheckThisSceneName()
    {
        var currentScene = SceneManager.GetActiveScene();   

        return currentScene.name;
    }
}

