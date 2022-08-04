using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ParamsSO : ScriptableObject 
{
    [Header("移動速度")]public float speed;
    [Header("ジャンプ速度")]public float jumpSpeed;
    [Header("ジャンプする高さ")]public float jumpHeight;
    [Header("ジャンプ制限時間")]public float jumpLimitTime;
    [Header("重力")]public float gravity;

  //MyScriptableObjectが保存してある場所のパス
  public const string PATH = "ParamsSO";

  //MyScriptableObjectの実体
  private static ParamsSO _entity;
  public  static ParamsSO Entity{
    get{
      //初アクセス時にロードする
      if(_entity == null){
        _entity = Resources.Load<ParamsSO>(PATH);

        //ロード出来なかった場合はエラーログを表示
        if(_entity == null){
          Debug.LogError(PATH + " not found");
        }
      }

      return _entity;
    }
  }
}
