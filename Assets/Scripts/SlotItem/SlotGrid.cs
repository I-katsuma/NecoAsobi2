using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGrid : MonoBehaviour
{
    private static SlotGrid instance = null;


    [SerializeField]
    private GameObject slotPrefab;

    private int slotNumber = 3;

    [SerializeField]
    Item[] allItems; // テスト用

    [SerializeField]
    private List<Slot> allSlots;

    #region Singleton
    public static SlotGrid Instance
    {
        get
        {
            SlotGrid[] instances = null;
            if(instance == null)
            {
                instances = FindObjectsOfType<SlotGrid>();
                if(instances.Length == 0)
                {
                    Debug.Log("SlotGridのインスタンスが存在しません");
                    return null;
                }
                else if(instances.Length > 1)
                {
                    Debug.Log("SlotGridのインスタンスが複数存在します");
                    return null;
                }
                else
                {
                    instance = instances[0];
                }
            }
            return instance;
        }
    }
    #endregion

    void Start()
    {
        allSlots = new List<Slot>(); // いったんリストを初期化

        for (int i = 0; i < slotNumber; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, this.transform);

            Slot slot = slotObj.GetComponent<Slot>();
            // 生成のたびにallSrotsに追加
            allSlots.Add(slot);
        }

        // TEST 全てのitemに対してforeachループを廻す
        foreach (var item in allItems)
        {
            AddItem(item);
        }
    }

    // allSlotsを順番に見ていき、空っぽのスロットにitemを追加する
    public bool AddItem(Item item)
    {
        foreach (var slot in allSlots) // 要素を確認
        {
            // もし空っぽならアイテム追加
            if (slot.MyItem == null)
            {
                slot.SetItem(item);
                return true;
            }
        }
        // 全てのスロットにアイテムが入っていたならばforeachを抜ける
        return false;
    }
}
