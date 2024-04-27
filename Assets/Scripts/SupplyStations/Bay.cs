using System;
using System.Collections.Generic;
using UnityEngine;

public class Bay : MonoBehaviour
{
    private Dictionary<Type, List<GameObject>> supplies;

    private void Awake()
    {
        supplies = new Dictionary<Type, List<GameObject>>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!supplies.ContainsKey(collision.gameObject.GetComponent<Supplies>().GetType()))
        {
            supplies.Add(collision.gameObject.GetComponent<Supplies>().GetType(), new List<GameObject>());
        }
        supplies[collision.gameObject.GetComponent<Supplies>().GetType()].Add(collision.gameObject);
        collision.GetComponent<Collider2D>().enabled = false;
    }

    public void UnloadBay()
    {
        if (ResupplyRequestValidator.Validate(GameManager.instance.resupplyRequest, supplies))
        {
            GameManager.instance.UpdateResupplyRequestDisplay("Resupply request fulfilled!");
            foreach (Type supplyType in supplies.Keys)
            {
                while (supplies[supplyType].Count > 0)
                {
                    GameObject supply = supplies[supplyType][0];
                    supplies[supplyType].RemoveAt(0);
                    Destroy(supply);
                }
            }
        }
        else
        {
            GameManager.instance.UpdateResupplyRequestDisplay("Items missing from resupply request!");
        }

        
    }
}
