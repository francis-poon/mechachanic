using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObject;


    public void OnMouseDown()
    {
        GameObject spawned = Instantiate(spawnObject);
        spawned.transform.position = transform.position + new Vector3(0,0,-1);
        spawned.GetComponent<SpawnObject>().Drag();
    }
}
