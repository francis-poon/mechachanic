using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    private bool dragging;

    private void Awake()
    {
        dragging = false;
    }

    private void Update()
    {
        if (dragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;

            dragging = Input.GetMouseButton(0);
        }
    }
    // TODO:  IPointerClickHandler
    public void OnMouseDown()
    {
        dragging = true;

        if (Input.GetMouseButton(1))
        {
            Debug.Log("RMB pressed");
            Destroy(this);
        }
    }

    public void Drag()
    {
        dragging = true;
    }
}
