using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public enum AmmoType
    {
        Regular,
        Piercing,
        Heat,
        Explosive
    }


    [SerializeField]
    private AmmoType ammoType;

    private Color emptyColor;
    private Color refilledColor;

    public bool filled { get; private set; } 

    private void Awake()
    {
        emptyColor = Color.black;
        refilledColor = Color.black;
        transform.GetComponent<SpriteRenderer>().color = emptyColor;
        filled = false;
    }

    public void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            transform.GetComponent<SpriteRenderer>().color = refilledColor;
            filled = true;
        }
    }

    public void OnMouseDown()
    {
        transform.GetComponent<SpriteRenderer>().color = refilledColor;
        filled = true;
    }

    public void UpdateColors(Color emptyColor, Color refilledColor)
    {
        this.emptyColor = emptyColor;
        this.refilledColor = refilledColor;
        transform.GetComponent<SpriteRenderer>().color = this.emptyColor;
    }
}
