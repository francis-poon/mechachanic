using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AmmoBox : Supplies
{
    public event EventHandler<EventArgs> AmmoBoxInserted;

    [SerializeField]
    private GameObject ammoBelt;

    [SerializeField]
    private GameObject ammoGroup;

    [SerializeField]
    private Color unfilledColor;

    [SerializeField]
    private Color filledColor;

    [SerializeField]
    private Animator ammoBeltAnimator;

    private AmmoBoxHolder ammoBoxHolder;
    public Vector3 returnPosition;
    private bool filled;
    private bool draggable;
    private bool filling;
    public Guid guid { private set; get; }

    private void Awake()
    {
        //this.transform.GetComponent<SpriteRenderer>().color = unfilledColor;
        filled = false;
        draggable = true;
        filling = false;
        guid = Guid.NewGuid();
    }

    private void Start()
    {
        ammoBeltAnimator.SetBool("Open", false);
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

    private void OnMouseUp()
    {
        if (filling)
        {
            return;
        }

        if (ammoBoxHolder != null)
        {
            draggable = false;
            ammoBeltAnimator.SetBool("Open", true);
            ammoGroup.SetActive(true);
            AmmoBoxInserted?.Invoke(this, null);
            Vector2 ammoSlot = ammoBoxHolder.GetComponent<AmmoBoxHolder>().GetAmmoSlot(this.guid);
            this.transform.position = new Vector3(ammoSlot.x, ammoSlot.y, GameManager.SUPPLY_LAYER);
        }
        else
        {
            this.transform.position = returnPosition;
        }
    }

    public void OnMouseDrag()
    {
        if (draggable)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, GameManager.SUPPLY_LAYER);
        }
    }

    public void SetAmmoBoxHolder(AmmoBoxHolder ammoBoxHolder)
    {
        this.ammoBoxHolder = ammoBoxHolder;
    }

    public void ClearAmmoBoxHolder()
    {
        this.ammoBoxHolder = null;
    }

    public void Fill()
    {
        Debug.Log("Ammo Filled");
        ammoGroup.SetActive(false);
        ammoBeltAnimator.SetBool("Open", false);
        this.transform.GetComponent<SpriteRenderer>().sortingOrder = 1;
        this.transform.position = this.transform.position + new Vector3(0, 0, -1);
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
