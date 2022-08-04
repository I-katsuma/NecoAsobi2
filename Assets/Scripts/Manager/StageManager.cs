using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class StageManager : MonoBehaviour
{
    [SerializeField] Fade fade;

    public enum ChangeStageSelect
    {
        stage1,
        stage2,
        stage3,
    }

    public ChangeStageSelect changeStage = ChangeStageSelect.stage1;

    private void Start() 
    {
        fade.FadeOut(1f);
    }

    /*
    private void Update() 
    {
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            changeStage = ChangeStageSelect.stage2;
            ChangeStageScene();
        }
    }
    */


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            ChangeStageScene();
        }
    }

    public void ChangeStageScene()
    {
        switch (changeStage)
        {
            case ChangeStageSelect.stage1:
            StageJamp("Game1scene");
            break;
            case ChangeStageSelect.stage2:
            StageJamp("Game2");
            break;
            case ChangeStageSelect.stage3:
            StageJamp("Game3");
            break;
            default:
            break;
        }
    }


    
    public void StageJamp(string num)
    {
        fade.FadeIn(1f, () =>
        SceneManager.LoadSceneAsync(num) );
    }
    
}
