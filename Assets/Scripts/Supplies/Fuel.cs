using System;
using UnityEngine;

public class Fuel : Supplies
{

    [SerializeField]
    private Color undistilledColor;

    [SerializeField]
    private Color distilledColor;
    
    int distillationLevel { set; get; }
    private float distillationTime;
    private int distillationProgressAmount;
    private Boolean distilling;
    private float waitTime;

    private new void Awake()
    {
        base.Awake();
        distillationTime = 0f;
        distillationProgressAmount = 0;
        distilling = false;
        waitTime = 0f;
    }

    private void Update()
    {
        transform.GetComponent<SpriteRenderer>().color = Color.Lerp(undistilledColor, distilledColor, distillationLevel / 100f);
        if (!distilling)
        {
            return;
        }

        if (distillationTime > 0f)
        {
            waitTime += Time.deltaTime;
            while (waitTime >= distillationTime)
            {
                waitTime -= distillationTime;
                distillationLevel += distillationProgressAmount;

                Debug.Log($"Distilling level at {distillationLevel}");
            }
        }

        if (distillationLevel >= 100)
        {
            distillationLevel = 100;
            StopDistillation();
        }
    }

    public void StartDistillation(float distillationTime, int distillationProgressAmount)
    {
        Debug.Log("Distilling started");
        this.distillationTime = distillationTime;
        this.distillationProgressAmount = distillationProgressAmount;
        distilling = true;
        waitTime = 0f;
    }

    public void StopDistillation()
    {
        Debug.Log("Distilling stopped");
        this.distillationTime = 0f;
        this.distillationProgressAmount = 0;
        distilling = false;
    }

}
