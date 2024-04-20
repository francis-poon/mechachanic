using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryStation : MonoBehaviour
{
    [SerializeField]
    private GameObject batterySpawner;

    [SerializeField]
    private GameObject batteryPackPrefab;

    private GameObject activeBattery;

    private void Start()
    {
        activeBattery = Instantiate(batteryPackPrefab, batterySpawner.transform);
        activeBattery.GetComponent<Battery>().returnPosition = batterySpawner.transform.position;
        activeBattery.GetComponent<Battery>().BatteryPluggedIn += HandleBatteryPluggedIn;
    }

    private void HandleBatteryPluggedIn(object sender, EventArgs args)
    {
        activeBattery.GetComponent<Battery>().BatteryPluggedIn -= HandleBatteryPluggedIn;
        activeBattery = Instantiate(batteryPackPrefab, batterySpawner.transform);
        activeBattery.GetComponent<Battery>().returnPosition = batterySpawner.transform.position;
        activeBattery.GetComponent<Battery>().BatteryPluggedIn += HandleBatteryPluggedIn;
    }
}
