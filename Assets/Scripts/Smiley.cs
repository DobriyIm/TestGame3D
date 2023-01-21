using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smiley : MonoBehaviour
{
    [SerializeField]
    private GameObject Sphere;
    private Vector3 shift;
    void Start()
    {
        shift = this.transform.position - Sphere.transform.position;
    }

    void Update()
    {
        this.transform.position = Sphere.transform.position + shift;
    }
}
