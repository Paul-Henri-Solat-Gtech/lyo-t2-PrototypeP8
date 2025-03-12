using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Item
{
    public enum Type { Item, Consummable, Projectile };
    public string name;
    public Type type;
    public RawImage image;

    public string GetName() { return name; }
    public new Type GetType() { return type; }
    public void SetName(string value) { name = value; }
    public void SetType(Type value) { type = value; }
}

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<Item> absorbedObjectList = new List<Item>(); // pour opti pas besoin de stocker le gameobject
    private List<GameObject> absorbedObjectListOnInventory = new List<GameObject>(); // pour opti pas besoin de stocker le gameobject

    [SerializeField]
    private RawImage inventoryScreen;
    private bool inventoryIsOpen = false;

    [SerializeField]
    private GameObject panelInventory;

    [SerializeField]
    private GameObject itemFramePrefab;

    // Start is called before the first frame update
    void Start()
    {
        inventoryScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InventoryUIManager();
        }
    }

    public void InventoryUIManager()
    {
        if (!inventoryIsOpen)
        {
            inventoryScreen.gameObject.SetActive(true);
            InventoryOpen();
            inventoryIsOpen = true;
  
        }
        else
        {
            InventoryClose();
            inventoryScreen.gameObject.SetActive(false);
            inventoryIsOpen = false;
        }
    }

    public void InventoryOpen()
    {
        foreach (Item obj in absorbedObjectList)
        {
            GameObject newInventoryFrame = Instantiate(itemFramePrefab, panelInventory.transform);
            newInventoryFrame.GetComponentInChildren<TMP_Text>().text = obj.GetName();
            newInventoryFrame.GetComponent<ItemManager>().SetItem(obj);
            absorbedObjectListOnInventory.Add(newInventoryFrame);
        }
    }
    public void InventoryClose()
    {
        foreach (GameObject obj in absorbedObjectListOnInventory)
        {
            GameObject.Destroy(obj);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjectToAbsorb"))
        {
            if (other.transform.localScale.x <= transform.localScale.x)
            {
                ObjectProperty newObjectProperty = other.GetComponent<ObjectProperty>();

                Item newItem = new Item();
                newItem.SetName(newObjectProperty.GetObjectName());
                newItem.SetType(newObjectProperty.GetObjectType());

                absorbedObjectList.Add(newItem);
                GameObject.Destroy(other.GameObject());
            }
        }
    }
}
