using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemObject : MonoBehaviour, IPointerClickHandler
{

    private Item Item { get; set; }

    // スロット上で「捨てる」コマンドを選択され、地面にオブジェクトが生成されたときの機能
    public void OnMakeObject(Item item)
    {
        Item = item;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TakeItem();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            TakeItem();
        }    
    }


    // 拾うメソッド(拾えない時のため、返り値はboolだと便利)
    public bool TakeItem()
    {
        if(SlotGrid.Instance.AddItem(Item))
        {
            Destroy(this.gameObject);
            return true;
        }

        // スロットがいっぱいならfalse
        return false;
    }
}
