using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DistillationStation : MonoBehaviour
{
    [SerializeField]
    private GameObject fuelCellSpawner;

    [SerializeField]
    private GameObject fuelCellPrefab;

    private GameObject activeFuelCell;

    private void Start()
    {
        activeFuelCell = Instantiate(fuelCellPrefab, fuelCellSpawner.transform);
        activeFuelCell.GetComponent<Fuel>().returnPosition = fuelCellSpawner.transform.position;
        activeFuelCell.GetComponent<Fuel>().FuelCellInserted += HandleBatteryPluggedIn;
    }

    private void HandleBatteryPluggedIn(object sender, EventArgs args)
    {
        activeFuelCell.GetComponent<Fuel>().FuelCellInserted -= HandleBatteryPluggedIn;
        activeFuelCell = Instantiate(fuelCellPrefab, fuelCellSpawner.transform);
        activeFuelCell.GetComponent<Fuel>().returnPosition = fuelCellSpawner.transform.position;
        activeFuelCell.GetComponent<Fuel>().FuelCellInserted += HandleBatteryPluggedIn;
    }
}
