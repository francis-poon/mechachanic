using System;
using UnityEngine;

public class AmmoStation : MonoBehaviour
{
    [SerializeField]
    private GameObject ammoBoxSpawner;

    [SerializeField]
    private GameObject ammoBoxPrefab;

    private Ammo.AmmoType selectedAmmoType;
    private AmmoSelector ammoSelector;
    private GameObject activeAmmoBox;

    private void Start()
    {
        SpawnNewAmmoBox();
    }

    public void SelectAmmo(Ammo.AmmoType ammoType, AmmoSelector ammoSelector, Color ammoColor)
    {
        selectedAmmoType = ammoType;
        Debug.Log($"{selectedAmmoType} selected");

        if (this.ammoSelector != null)
        {
            this.ammoSelector.Deselect();
        }
        this.ammoSelector = ammoSelector;
        this.activeAmmoBox.GetComponent<AmmoBox>().SelectAmmoType(ammoColor);
    }

    private void SpawnNewAmmoBox()
    {
        activeAmmoBox = Instantiate(ammoBoxPrefab, ammoBoxSpawner.transform);
        activeAmmoBox.GetComponent<AmmoBox>().returnPosition =
            new Vector3(ammoBoxSpawner.transform.position.x, ammoBoxSpawner.transform.position.y, GameManager.SUPPLY_LAYER);
        activeAmmoBox.GetComponent<AmmoBox>().AmmoBoxInserted += HandleAmmoBoxInserted;
    }

    private void HandleAmmoBoxInserted(object sender, EventArgs args)
    {
        activeAmmoBox.GetComponent<AmmoBox>().AmmoBoxInserted -= HandleAmmoBoxInserted;
        SpawnNewAmmoBox();
    }
}
