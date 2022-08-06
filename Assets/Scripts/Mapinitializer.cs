using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapinitializer : MonoBehaviour
{
    // 初めから設置したいアイテムの登録
    [SerializeField] private Item item;
    //[SerializeField] private Transform setGameObject;

    private void Start() 
    {
        for (int i = 0; i < 3; i++)
        {
            item.GetItemObj().transform.SetParent(this.transform, false); 
        }
           
    }

}
