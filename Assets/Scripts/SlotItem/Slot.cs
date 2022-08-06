using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    private Item item;
    [SerializeField] private Image itemImage;

    public Item MyItem { get => item; private set => item = value; }

    protected virtual void Start() 
    {
        if(MyItem==null) itemImage.color = new Color(0, 0, 0, 0);    
    }

    public void SetItem(Item item)
    {
        MyItem = item;

        if(item != null)
        {
            itemImage.color = new Color(1, 1, 1, 1);
            itemImage.sprite = item.MyItemImage;
        }
        else
        {
            itemImage.color = new Color(0, 0, 0, 0);
        }
    }

    private Vector3 SetPlayerPosition() // 削除アイテム落下ポジション
    {
        // TODO: プレイヤーの頭上から落下するようにしたい
        Vector3 num = new Vector3(5f, 5f, 0f);

        return num;
    }

    public void OnPointerClick(PointerEventData eventData) // クリックしたとき呼ばれる
    {
        // MyItemの値がnullなら、メソッドを切り上げる
        if(MyItem == null)
        {
            return;
        }
        
        // アイテムをマップ上に配置
        GameObject itemObj = MyItem.GetItemObj();
        itemObj.transform.SetPositionAndRotation(SetPlayerPosition(), Quaternion.identity); 

        itemObj.transform.SetParent(GameObject.Find("Items").transform, false);

        // アイテムを捨てる
        SetItem(null);
    }
}
