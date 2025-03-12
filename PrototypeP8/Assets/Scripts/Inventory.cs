using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ObjectPrefab
{
    public string name;
    public GameObject prefab;

    public string GetName() { return name; }
    public GameObject GetPrefab() { return prefab; }
}

[System.Serializable]
public class Item
{
    public enum Type { Item, Consummable, Projectile };
    public string name;
    public Type type;
    public Texture2D image;

    public string GetName() { return name; }
    public new Type GetType() { return type; }
    public Texture2D GetImage() { return image; }
    public void SetName(string value) { name = value; }
    public void SetType(Type value) { type = value; }
    public void SetImage(Texture2D value) { image = value; }
}

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<Item> absorbedObjectList = new List<Item>(); // pour opti pas besoin de stocker le gameobject
    private List<GameObject> absorbedObjectListOnInventory = new List<GameObject>(); // pour opti pas besoin de stocker le gameobject

    [SerializeField]
    private List<ObjectPrefab> prefabsList = new List<ObjectPrefab>();

    [SerializeField]
    private RawImage inventoryScreen;
    private bool inventoryIsOpen = false;

    [SerializeField]
    private GameObject panelInventory;

    [SerializeField]
    private GameObject itemFramePrefab;

    [SerializeField]
    private float sizeUp = 0.1f;
    [SerializeField]
    private float dropForce = 5f;

    public List<Item> GetInventoryPlayer() { return absorbedObjectList; }

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

            //newInventoryFrame.GetComponentInChildren<RawImage>().texture = obj.GetImage();
            RawImage imageFrame = newInventoryFrame.transform.Find("ObjectIMG").GetComponent<RawImage>();
            imageFrame.texture = obj.GetImage();
            
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

    public void DropItem(Item item)
    {
        foreach (ObjectPrefab prefabs in prefabsList)
        {
            if (item.GetName() == prefabs.GetName())
            {
                Vector3 newPos = transform.position;
                newPos.z += 3 + transform.localScale.x;
                GameObject newDroppedObject = Instantiate(prefabs.GetPrefab(), newPos, Quaternion.identity);
                newDroppedObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * dropForce, ForceMode.Impulse);

                GetInventoryPlayer().Remove(item);
            }
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
                newItem.SetImage(newObjectProperty.GetObjectImage());

                absorbedObjectList.Add(newItem);
                GameObject.Destroy(other.GameObject());
                GameObject.Destroy(other.transform.parent.GameObject());

                transform.localScale += new Vector3(sizeUp, sizeUp, sizeUp);
            }
        }
    }
}
