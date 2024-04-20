using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    private int counter;

    [SerializeField]
    private float chargeRate = 0.5f;

    private void Awake()
    {
        counter = 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        counter++;
        collision.gameObject.GetComponent<Battery>().StartCharge();
        Debug.Log($"Counter increased to {counter}");
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        counter--;
        collision.gameObject.GetComponent<Battery>().StopCharge();
        Debug.Log($"Counter decreased to {counter}");
    }
}
