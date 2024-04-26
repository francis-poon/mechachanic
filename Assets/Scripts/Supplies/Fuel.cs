using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public event EventHandler<EventArgs> FuelCellInserted;

    [SerializeField]
    private Color undistilledColor;

    [SerializeField]
    private Color distilledColor;

    [SerializeField]
    private GameObject tankBody;
    
    int distillationLevel { set; get; }
    private Boolean distilling;
    private float waitTime;

    public Vector3 returnPosition { set; private get; }
    private Distiller distiller;
    private bool draggable;
    public Guid guid { private set; get; }

    private void Awake()
    {
        distilling = false;
        waitTime = 0f;
        draggable = true;
        tankBody.GetComponent<SpriteRenderer>().color = undistilledColor;
        guid = Guid.NewGuid();
    }

    private void Update()
    {
        if (!distilling || distiller == null)
        {
            return;
        }

        if (distiller.GetDistillationTime() > 0f)
        {
            waitTime += Time.deltaTime;
            while (waitTime >= distiller.GetDistillationTime())
            {
                waitTime -= distiller.GetDistillationTime();
                distillationLevel += distiller.GetDistillationProgressAmount();

                Debug.Log($"Distilling level at {distillationLevel}");
            }
        }

        if (distillationLevel >= 100)
        {
            distillationLevel = 100;
            draggable = true;
            StopDistillation();
        }

        tankBody.GetComponent<SpriteRenderer>().color = Color.Lerp(undistilledColor, distilledColor, distillationLevel / 100f);
    }

    public void OnMouseUp()
    {
        if (distillationLevel >= 100 || distilling)
        {
            return;
        }

        if (distiller != null)
        {
            this.transform.position = distiller.transform.position;
            draggable = false;
            FuelCellInserted?.Invoke(this, null);
            Vector2 fuelSlot = distiller.GetFuelSlot(this.guid);
            this.transform.position = new Vector3(fuelSlot.x, fuelSlot.y, GameManager.SUPPLY_LAYER);
            StartDistillation();
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

    public void SetDistiller(Distiller distiller)
    {
        this.distiller = distiller;
    }

    public void ClearDistiller()
    {
        this.distiller = null;
    }

    public void StartDistillation()
    {
        Debug.Log("Distilling started");
        distilling = true;
        waitTime = 0f;
    }

    public void StopDistillation()
    {
        Debug.Log("Distilling stopped");
        distilling = false;
    }

}
