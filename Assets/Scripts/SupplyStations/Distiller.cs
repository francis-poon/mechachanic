using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Distiller : MonoBehaviour
{
    [SerializeField]
    private GameObject[] fuelSlots;

    private Dictionary<Guid, int> fuelTracker;
    private bool[] isSlotOccupied;

    private void Awake()
    {
        fuelTracker = new Dictionary<Guid, int>();
        isSlotOccupied = new bool[fuelSlots.Length];
        for (int c = 0; c < fuelSlots.Length; c++)
        {
            isSlotOccupied[c] = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Fuel>() != null && HasUnoccupiedSlot())
        {
            Fuel fuel = collision.gameObject.GetComponent<Fuel>();
            fuel.FuelCellInserted += HandleFuelCellInserted;
            fuel.SetDistiller(this);
            Debug.Log($"Collision with {collision}");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Fuel>() != null)
        {
            Fuel fuel = collision.gameObject.GetComponent<Fuel>();
            fuel.FuelCellInserted -= HandleFuelCellInserted;
            fuel.ClearDistiller();

            if (fuelTracker.ContainsKey(fuel.guid))
            {
                fuelTracker.Remove(fuel.guid);
            }
        }
    }

    public void HandleFuelCellInserted(object sender, EventArgs args)
    {
        AddFuelCell((Fuel)sender);
        Debug.Log($"{fuelTracker.Count}/{fuelSlots.Length} occupied");
    }

    public float GetDistillationTime()
    {
        return 0f;
    }

    public int GetDistillationProgressAmount()
    {
        return 0;
    }

    private void AddFuelCell(Fuel fuel)
    {
        int unoccupiedSlot = -1;
        for (int c = 0; c < isSlotOccupied.Length; c ++)
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
            fuel.FuelCellInserted -= HandleFuelCellInserted;
            fuel.ClearDistiller();
            return;
        }

        fuelTracker.Add(fuel.guid, unoccupiedSlot);
        isSlotOccupied[unoccupiedSlot] = true;
    }

    private bool HasUnoccupiedSlot()
    {
        return fuelTracker.Count < fuelSlots.Length;
    }
}
