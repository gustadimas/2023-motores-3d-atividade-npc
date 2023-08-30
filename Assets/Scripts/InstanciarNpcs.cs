using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciarNpcs : MonoBehaviour
{
    [SerializeField] GameObject prefabNpc;
    void Start()
    {
        InvokeRepeating("InstanciarNpc", 0f, 1.5f);
    }

    void InstanciarNpc()
    {
        var positionX = Random.Range(-7, 7);
        var positionZ = Random.Range(-15, 15);
        Instantiate(prefabNpc, new Vector3(positionX, 1.2f, positionZ), Quaternion.identity);
    }
}
