using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DistillationStation : MonoBehaviour
{
    private int counter;

    [SerializeField]
    private float distillationRate = 0.5f;

    [SerializeField]
    private int distillationProgressAmount = 10;

    private void Awake()
    {
        counter = 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Fuel>() != null)
        {
            counter++;
            collision.gameObject.GetComponent<Fuel>().StartDistillation(distillationRate, distillationProgressAmount);
            Debug.Log($"Counter increased to {counter}");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Fuel>() != null)
        {
            counter--;
            collision.gameObject.GetComponent<Fuel>().StopDistillation();
            Debug.Log($"Counter decreased to {counter}");
        }
        
    }
}
