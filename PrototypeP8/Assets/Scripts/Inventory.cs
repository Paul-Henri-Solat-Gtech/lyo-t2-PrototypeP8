using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> absorbedObjectList; // pour opti pas besoin de stocker le gameobject

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
            inventoryIsOpen = true;
        }
        else
        {
            inventoryScreen.gameObject.SetActive(false);
            inventoryIsOpen = false;
        }
    }

    public void InventoryOpen()
    {
        foreach (GameObject obj in absorbedObjectList)
        {
            //GameObject newObj = Instantiate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjectToAbsorb"))
        {
            absorbedObjectList.Add(other.GameObject());
        }
    }
}
