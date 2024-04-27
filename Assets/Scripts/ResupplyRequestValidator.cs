using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResupplyRequestValidator
{
    public static bool Validate(Dictionary<string, int> resupplyRequest, Dictionary<Type, List<GameObject>> supplies)
    {
        bool isValid = true;
        foreach (string supplyType in resupplyRequest.Keys)
        {
            switch (supplyType)
            {
                case "Fuel":
                    isValid &= ValidateSupply(resupplyRequest["Fuel"], supplies.GetValueOrDefault(typeof(Fuel), null));
                    break;
                case "Ammo":
                    isValid &= ValidateSupply(resupplyRequest["Ammo"], supplies.GetValueOrDefault(typeof(AmmoBox), null));
                    break;
                case "Battery":
                    isValid &= ValidateSupply(resupplyRequest["Battery"], supplies.GetValueOrDefault(typeof(Battery), null));
                    break;
                default:
                    isValid = false;
                    Debug.LogError($"{typeof(ResupplyRequestValidator)}: Trying to validate unkown supply type \"{supplyType}\"");
                    break;
            }
        }

        return isValid;
    }

    private static bool ValidateSupply(int requestedSupplyCount, List<GameObject> supplies)
    {
        bool isValid = true;
        if (supplies != null)
        {
            isValid = supplies.Count >= requestedSupplyCount;
        }
        else
        {
            isValid = requestedSupplyCount == 0;
        }

        GameManager.instance.isRequestFulfilled = isValid;
        return isValid;
    }
}
