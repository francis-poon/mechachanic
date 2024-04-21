using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    [SerializeField]
    private float chargeTime;

    [SerializeField]
    private int chargeProgressAmount;

    private bool occupied;
    private Battery occupiedBattery;

    private void Awake()
    {
        occupied = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Battery>() != null && !occupied)
        {
            Battery battery = collision.gameObject.GetComponent<Battery>();
            battery.BatteryPluggedIn += HandleBatteryPluggedIn;
            battery.SetCharger(this);
            Debug.Log($"Collision with {collision}");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Battery>() != null)
        {
            Battery battery = collision.gameObject.GetComponent<Battery>();
            battery.BatteryPluggedIn -= HandleBatteryPluggedIn;
            battery.ClearCharger();
            if (battery.Equals(occupiedBattery))
            {
                occupied = false;
            }
        }
    }

    public void HandleBatteryPluggedIn(object sender, EventArgs args)
    {
        Debug.Log($"Charger now occupied");
        occupied = true;
        occupiedBattery = (Battery)sender;
    }

    public float GetChargeTime()
    {
        return this.chargeTime;
    }

    public int GetChargeProgressAmount()
    {
        return this.chargeProgressAmount;
    }
}
