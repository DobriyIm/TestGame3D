using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLight : MonoBehaviour
{
    [SerializeField]
    private GameObject Shpere;
    [SerializeField]
    private Camera Cam;

    private Vector3 shift;

    void Start()
    {
        shift = this.transform.position - Shpere.transform.position;

    }

    void LateUpdate()
    {
        this.transform.position = Shpere.transform.position + shift;
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, Cam.transform.eulerAngles.y, this.transform.eulerAngles.z);
    }
}
