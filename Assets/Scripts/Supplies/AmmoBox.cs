using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AmmoBox : Supplies
{
    [SerializeField]
    private GameObject ammoBelt;

    [SerializeField]
    private GameObject ammoGroup;

    [SerializeField]
    private Color unfilledColor;

    [SerializeField]
    private Color filledColor;

    private Transform startingPosition;
    private bool filled;

    private void Awake()
    {
        startingPosition = this.transform;
        this.transform.GetComponent<SpriteRenderer>().color = unfilledColor;
        filled = false;
    }

    private void Start()
    {
        ammoBelt.SetActive(false);
        ammoGroup.SetActive(false);
    }

    private void Update()
    {
        if (filled)
        {
            return;
        }

        filled = true;
        foreach (Ammo ammo in ammoGroup.GetComponentsInChildren<Ammo>())
        {
            filled = filled && ammo.filled;
        }

        if (filled)
        {
            Fill();
        }
    }

    public void OnMouseDown()
    {
        if (!filled)
        {
            ammoBelt.SetActive(!ammoBelt.activeSelf);
        }
    }

    public void OnMouseDrag()
    {
        if (filled)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }
    }

    public void Fill()
    {
        Debug.Log("Ammo Filled");
        ammoBelt.SetActive(false);
        this.transform.GetComponent<SpriteRenderer>().color = filledColor;
        this.transform.GetComponent<SpriteRenderer>().sortingOrder = 1;
        this.transform.position = this.transform.position + new Vector3(0, 0, -1);
        this.transform.GetComponentInParent<AmmoStation>().OnBoxFilled();
    }

    public void SelectAmmoType(Color ammoColor)
    {
        foreach (Ammo ammo in ammoGroup.GetComponentsInChildren<Ammo>())
        {
            ammo.UpdateColors(new Color(ammoColor.r, ammoColor.g, ammoColor.b, 0.5f), ammoColor);
        }

        ammoGroup.SetActive(true);
    }
}
