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
    private Texture2D image;
    [SerializeField]
    private float size = 1f;

    public Item.Type GetObjectType() { return type; }
    public string GetObjectName() { return name; }
    public Texture2D GetObjectImage() { return image; }
    public float GetObjectSize() { return size; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
