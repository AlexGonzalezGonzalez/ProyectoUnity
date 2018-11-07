using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour { 
public GameObject Nave;

private Vector3 v3;

void Start()
{
    v3 = transform.position - Nave.transform.position;
}

void LateUpdate()
{
    transform.position = Nave.transform.position + v3;
}
}