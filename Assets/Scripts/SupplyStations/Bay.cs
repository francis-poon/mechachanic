using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bay : MonoBehaviour
{
    private List<GameObject> supplies;

    private void Awake()
    {
        supplies = new List<GameObject>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        supplies.Add(collision.gameObject);
        collision.GetComponent<Collider2D>().enabled = false;
    }

    public void UnloadBay()
    {
        foreach (GameObject supply in supplies)
        {
            Destroy(supply);
        }
    }
}
