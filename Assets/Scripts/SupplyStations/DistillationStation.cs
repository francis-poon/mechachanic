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
        activeFuelCell.GetComponent<Fuel>().returnPosition =
            new Vector3(fuelCellSpawner.transform.position.x, fuelCellSpawner.transform.position.y, GameManager.SUPPLY_LAYER);
        activeFuelCell.GetComponent<Fuel>().FuelCellInserted += HandleFuelCellInserted;
    }

    private void HandleFuelCellInserted(object sender, EventArgs args)
    {
        activeFuelCell.GetComponent<Fuel>().FuelCellInserted -= HandleFuelCellInserted;
        activeFuelCell = Instantiate(fuelCellPrefab, fuelCellSpawner.transform);
        activeFuelCell.GetComponent<Fuel>().returnPosition =
            new Vector3(fuelCellSpawner.transform.position.x, fuelCellSpawner.transform.position.y, GameManager.SUPPLY_LAYER);
        activeFuelCell.GetComponent<Fuel>().FuelCellInserted += HandleFuelCellInserted;
    }
}
