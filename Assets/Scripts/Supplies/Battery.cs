using System;
using UnityEngine;

public class Battery : Supplies
{
    public event EventHandler<EventArgs> BatteryPluggedIn;

    [SerializeField]
    private Animator animator;

    public int chargeLevel { private set; get; }

    private bool charging;
    private float waitTime;

    public Vector3 returnPosition { set; private get; }
    private Charger charger;
    private bool draggable;

    public Guid guid { private set; get; }


    protected void Awake()
    {
        chargeLevel = 0;
        charging = false;
        waitTime = 0f;
        draggable = true;

        guid = Guid.NewGuid();
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

        if (animator.GetInteger("ChargingState") < (int)((chargeLevel * 3 + 100) / 100))
        {
            animator.SetInteger("ChargingState", (int)((chargeLevel * 3 + 100) / 100));
        }
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
            Vector2 chargingSlot = charger.GetChargingSlot(this.guid);
            this.transform.position = new Vector3(chargingSlot.x, chargingSlot.y, GameManager.SUPPLY_LAYER);
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
            transform.position = new Vector3(mousePosition.x, mousePosition.y, GameManager.SUPPLY_LAYER);
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
