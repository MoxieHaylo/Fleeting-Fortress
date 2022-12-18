using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFollowMouse : MonoBehaviour
{
    PlaceObjects placeObjects;
    public bool isOnGrid;

    void Start()
    {
        placeObjects = FindObjectOfType<PlaceObjects>();
    }

    void Update()
    {
        if(!isOnGrid)
        {
            transform.position = placeObjects.smoothMousePos + new Vector3(0, 0.5f, 0);
        }

    }
}
