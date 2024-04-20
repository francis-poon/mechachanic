using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    [SerializeField]
    private float chargeTime;

    [SerializeField]
    private int chargeStep;

    private bool occupied;

    private void Awake()
    {
        occupied = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collision with {collision}");
        if (collision.gameObject.GetComponent<Battery>() != null && !occupied)
        {
            Battery battery = collision.gameObject.GetComponent<Battery>();
            battery.BatteryPluggedIn += HandleBatteryPluggedIn;
            battery.SetCharger(this);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Battery>() != null)
        {
            Battery battery = collision.gameObject.GetComponent<Battery>();
            battery.BatteryPluggedIn -= HandleBatteryPluggedIn;
            battery.ClearCharger();
            occupied = false;
        }
    }

    public void HandleBatteryPluggedIn(object sender, EventArgs args)
    {
        occupied = true;
    }

    public float GetChargeTime()
    {
        return this.chargeTime;
    }

    public int GetChargeStep()
    {
        return this.chargeStep;
    }
}
