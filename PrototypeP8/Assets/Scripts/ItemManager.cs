using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private GameObject player;
    private Inventory playerInventory;

    [SerializeField]
    private Item item;

    public void SetItem(Item value) { item = value; }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerInventory = player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseItem()
    {
        playerInventory.DropItem(item);
        CloseInventory();
    }

    public void CloseInventory()
    {
        if (player != null)
        {
            playerInventory.InventoryUIManager();
        }
    }
}
