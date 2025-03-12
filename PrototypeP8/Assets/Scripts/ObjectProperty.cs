using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ObjectProperty : MonoBehaviour
{   
    [SerializeField]
    private Item.Type type = Item.Type.Item;
    [SerializeField]
    private new string name = "noName";
    [SerializeField]
    private RawImage image;

    public Item.Type GetObjectType() { return type; }
    public string GetObjectName() { return name; }
    public RawImage GetObjectImage() { return image; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
