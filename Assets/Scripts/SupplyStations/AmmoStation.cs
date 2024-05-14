using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoStation : MonoBehaviour
{
    [SerializeField]
    private GameObject ammoBoxSpawner;

    [SerializeField]
    private GameObject ammoBoxPrefab;

    private Ammo.AmmoType selectedAmmoType;
    private AmmoSelector ammoSelector;
    private GameObject ammoBox;

    private void Start()
    {
        ammoBox = Instantiate(ammoBoxPrefab, this.transform);
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
        this.ammoBox.GetComponent<AmmoBox>().SelectAmmoType(ammoColor);
    }

    public void OnBoxFilled()
    {
        ammoBox = Instantiate(ammoBoxPrefab, this.transform);
    }
}
