using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Items/item")]
public class Item : ScriptableObject 
{

    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemImage;

    [SerializeField] private int itemId;

    [SerializeField] ItemObject itemObjPrefab;


    public string MyItemName { get => itemName;}
    public Sprite MyItemImage { get => itemImage;}

    public int MyItemId { get => itemId;}

    public GameObject GetItemObj() // アイテムのオブジェクトを手に入れる機能
    {
        GameObject go = Instantiate(itemObjPrefab.gameObject);
        go.GetComponent<ItemObject>().OnMakeObject(this);

        return go; // インスタンス化したオブジェクトを返す
    }
}
