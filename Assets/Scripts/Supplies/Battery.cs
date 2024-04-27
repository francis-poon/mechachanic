using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Battery : Supplies
{
    public event EventHandler<EventArgs> BatteryPluggedIn;

    [SerializeField]
    private Color unchargedColor;

    [SerializeField]
    private Color chargedColor;


    public int chargeLevel { private set; get; }

    private bool charging;
    private float waitTime;

    public Vector3 returnPosition { set; private get; }
    private Charger charger;
    private bool draggable;


    protected void Awake()
    {
        chargeLevel = 0;
        charging = false;
        waitTime = 0f;
        draggable = true;

        this.transform.GetComponent<SpriteRenderer>().color = unchargedColor;
    }

    private void Update()
    {
        if (!charging || this.charger == null)
        {
            return;
        }

        if (charger.GetChargeTime() > 0f)
        {
            waitTime += Time.deltaTime;
            while (waitTime >= charger.GetChargeTime())
            {
                waitTime -= charger.GetChargeTime();
                chargeLevel += charger.GetChargeProgressAmount();
            }
        }

        if (chargeLevel >= 100)
        {
            chargeLevel = 100;
            draggable = true;
            StopCharge();
        }

        this.transform.GetComponent<SpriteRenderer>().color = Color.Lerp(unchargedColor, chargedColor, chargeLevel / 100f);
    }

    public void OnMouseUp()
    {
        if (chargeLevel >= 100 || charging)
        {
            return;
        }

        if (charger != null)
        {
            this.transform.position = charger.transform.position;
            draggable = false;
            BatteryPluggedIn?.Invoke(this, null);
            StartCharge();
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
            transform.position = mousePosition;
        }
    }

    public void SetCharger(Charger charger)
    {
        this.charger = charger;
    }

    public void ClearCharger()
    {
        this.charger = null;
    }

    public void StartCharge()
    {
        Debug.Log("Charging started");
        charging = true;
        waitTime = 0f;
    }

    public void StopCharge()
    {
        Debug.Log("Charging stopped");
        charging = false;
    }
}
