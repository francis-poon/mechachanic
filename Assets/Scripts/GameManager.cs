using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int SUPPLY_LAYER = -1;

    [SerializeField]
    private GameObject resupplyRequestDisplay;

    public Dictionary<string, int> resupplyRequest { get; private set; }
    public string resupplyRequestInfo;
    private System.Random rand;
    public bool isRequestFulfilled;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        resupplyRequest = new Dictionary<string, int>()
        {
            { "Fuel", 1 },
            { "Ammo", 1 },
            { "Battery", 1 }
        };
        resupplyRequestInfo = "";
        rand = new System.Random();
        isRequestFulfilled = false;
        GenerateNewResupplyRequest();
    }

    private void Start()
    {
        resupplyRequestDisplay.GetComponent<TextMeshProUGUI>().text = $"{resupplyRequest.ToCommaSeparatedString()}\n{resupplyRequestInfo}";
    }

    public void UpdateResupplyRequestDisplay(string info)
    {
        this.resupplyRequestInfo = info;
        resupplyRequestDisplay.GetComponent<TextMeshProUGUI>().text = $"{resupplyRequest.ToCommaSeparatedString()}\n{resupplyRequestInfo}";
    }

    public void UpdateResupplyRequestDisplay()
    {
        resupplyRequestDisplay.GetComponent<TextMeshProUGUI>().text = $"{resupplyRequest.ToCommaSeparatedString()}\n{resupplyRequestInfo}";
    }

    public void NextResupplyRequest()
    {
        GenerateNewResupplyRequest();
        UpdateResupplyRequestDisplay("");
    }

    private void GenerateNewResupplyRequest()
    {
        if (!isRequestFulfilled)
        {
            return;
        }
        isRequestFulfilled = false;
        int totalSupplyCount = 0;
        List<string> keys = resupplyRequest.Keys.ToList();
        foreach (string supplyType in keys)
        {
            int supplyCount = rand.Next(0, 4);
            totalSupplyCount += supplyCount;
            resupplyRequest[supplyType] = supplyCount;
        }

        if (totalSupplyCount == 0)
        {
            resupplyRequest["Fuel"] = 1;
        }
    }
}
