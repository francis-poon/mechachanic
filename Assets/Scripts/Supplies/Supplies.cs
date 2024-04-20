using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplies : MonoBehaviour
{
    private bool dragging;

    protected void Awake()
    {
        dragging = false;
    }

    public void OnMouseDown()
    {
        dragging = true;
    }

    public void OnMouseUp()
    {
        dragging = false;
    }

    public void OnMouseDrag()
    {
        if (dragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }
    }
}
