using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Items/item")]
public class Item : ScriptableObject 
{

    [SerializeField] private string itemName; // 名前
    [SerializeField] private Sprite itemImage; // アイコン

    [SerializeField] private int itemId;
    private int hpChange; // ヘルスPOINTチェンジ

    [SerializeField] ItemObject itemObjPrefab;
    //[SerializeField] Transform itemDropPosition;


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
