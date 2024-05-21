using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject[] ammoSlots;

    private Dictionary<Guid, int> ammoTracker;
    private bool[] isSlotOccupied;

    private void Awake()
    {
        ammoTracker = new Dictionary<Guid, int>();
        isSlotOccupied = new bool[ammoSlots.Length];
        for (int c = 0; c < ammoSlots.Length; c++)
        {
            isSlotOccupied[c] = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<AmmoBox>() != null && HasUnoccupiedSlot())
        {
            AmmoBox ammoBox = collision.gameObject.GetComponent<AmmoBox>();
            ammoBox.AmmoBoxInserted += HandleAmmoBoxInserted;
            ammoBox.SetAmmoBoxHolder(this);
            Debug.Log($"Collision with {collision}");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<AmmoBox>() != null)
        {
            AmmoBox ammoBox = collision.gameObject.GetComponent<AmmoBox>();
            ammoBox.AmmoBoxInserted -= HandleAmmoBoxInserted;
            ammoBox.ClearAmmoBoxHolder();

            if (ammoTracker.ContainsKey(ammoBox.guid))
            {
                isSlotOccupied[ammoTracker[ammoBox.guid]] = false;
                ammoTracker.Remove(ammoBox.guid);
            }
        }
    }

    public void HandleAmmoBoxInserted(object sender, EventArgs args)
    {
        AddAmmoBox((AmmoBox)sender);
        Debug.Log($"{ammoTracker.Count}/{ammoSlots.Length} occupied");
    }

    public Vector3 GetAmmoSlot(Guid guid)
    {
        if (this.ammoTracker.ContainsKey(guid))
        {
            return this.ammoSlots[ammoTracker[guid]].transform.position;
        }

        Debug.LogError($"No ammo slot for {guid}");
        return Vector3.zero;
    }

    private void AddAmmoBox(AmmoBox ammoBox)
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
            ammoBox.AmmoBoxInserted -= HandleAmmoBoxInserted;
            ammoBox.ClearAmmoBoxHolder();
            return;
        }

        Debug.Log($"{ammoBox.guid} added to ammo slots");

        ammoTracker.Add(ammoBox.guid, unoccupiedSlot);
        isSlotOccupied[unoccupiedSlot] = true;
    }

    private bool HasUnoccupiedSlot()
    {
        return ammoTracker.Count < ammoSlots.Length;
    }
}
