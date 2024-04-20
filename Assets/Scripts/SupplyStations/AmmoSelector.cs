using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSelector : MonoBehaviour
{
    [SerializeField]
    private Ammo.AmmoType ammoType;

    [SerializeField]
    private GameObject ammoStation;

    [SerializeField]
    private Color buttonColor;

    [SerializeField]
    private Color selectedTint;

    private void Awake()
    {
        this.transform.GetComponent<SpriteRenderer>().color = buttonColor;
    }

    public void OnMouseDown()
    {
        this.transform.GetComponent<SpriteRenderer>().color = Color.Lerp(buttonColor, selectedTint, 0.5f);
        ammoStation.GetComponent<AmmoStation>().SelectAmmo(ammoType, this, buttonColor);
    }

    public void Deselect()
    {
        this.transform.GetComponent<SpriteRenderer>().color = buttonColor;
    }
}
