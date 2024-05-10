using System;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] chargingSlots;

    [SerializeField]
    private float chargeTime;

    [SerializeField]
    private int chargeProgressAmount;

    private Dictionary<Guid, int> batteryTracker;
    private bool[] isSlotOccupied;

    private void Awake()
    {
        batteryTracker = new Dictionary<Guid, int>();
        isSlotOccupied = new bool[chargingSlots.Length];
        for (int c = 0; c < chargingSlots.Length; c ++)
        {
            isSlotOccupied[c] = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Battery>() != null && HasUnoccupiedSlot())
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
            if (batteryTracker.ContainsKey(battery.guid))
            {
                isSlotOccupied[batteryTracker[battery.guid]] = false;
                batteryTracker.Remove(battery.guid);
            }
        }
    }

    public void HandleBatteryPluggedIn(object sender, EventArgs args)
    {
        Debug.Log($"Charger now occupied");
        AddBattery((Battery)sender);
    }

    public float GetChargeTime()
    {
        return this.chargeTime;
    }

    public int GetChargeProgressAmount()
    {
        return this.chargeProgressAmount;
    }

    public Vector3 GetChargingSlot(Guid guid)
    {
        if (this.batteryTracker.ContainsKey(guid))
        {
            return this.chargingSlots[batteryTracker[guid]].transform.position;
        }

        Debug.LogError($"No charging slot for {guid}");
        return Vector3.zero;
    }

    private void AddBattery(Battery battery)
    {
        int unoccupiedSlot = -1;
        for (int c = 0; c < isSlotOccupied.Length; c++)
        {
            if (!isSlotOccupied[c])
            {
                unoccupiedSlot = c;
                break;
            }
        }
        if (unoccupiedSlot == -1)
        {
            Debug.LogError("DistillationStation full but attempt was made to add fuel cell.");
            battery.BatteryPluggedIn -= HandleBatteryPluggedIn;
            battery.ClearCharger();
            return;
        }

        batteryTracker.Add(battery.guid, unoccupiedSlot);
        isSlotOccupied[unoccupiedSlot] = true;
    }

    private bool HasUnoccupiedSlot()
    {
        return batteryTracker.Count < chargingSlots.Length;
    }
}
