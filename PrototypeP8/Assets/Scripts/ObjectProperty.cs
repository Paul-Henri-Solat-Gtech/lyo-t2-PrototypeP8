using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectProperty : MonoBehaviour
{
    public enum Type { Item, Consummable, Projectile };

    [SerializeField]
    private Type type = Type.Item;

    public Type GetObjectType() { return type; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
