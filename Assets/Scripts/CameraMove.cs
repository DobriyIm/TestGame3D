using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private GameObject Sphere;

    private Vector3 cam_sphere;
    private float zoom;

    private const float MIN_ZOOM = 0.5f;
    private const float MAX_ZOOM = 2f;
    private const float ZOOM_SENS = 10f;
   


    private float camAngleVertical;
     private const float MIN_ANG_VERTICAL = 35f;
    private const float MAX_ANG_VERTICAL = 70f;
    private const float VERTICAL_SENS = 2;

    private float camAngleHorizontal;
    private const float HORIZONTAL_SENS = 2;

    void Start()
    {
        cam_sphere = this.transform.position - Sphere.transform.position;
        zoom = 1;
        camAngleVertical = this.transform.eulerAngles.x;
        camAngleHorizontal = this.transform.eulerAngles.y;
    }

    void Update()
    {
        if(Input.mouseScrollDelta.y != 0){
            zoom -= Input.mouseScrollDelta.y / ZOOM_SENS * Time.timeScale;
            if(zoom < MIN_ZOOM) zoom = MIN_ZOOM;
            if(zoom > MAX_ZOOM) zoom = MAX_ZOOM;
        }

        float mouseY = Input.GetAxis("Mouse Y");
        camAngleVertical -= mouseY * VERTICAL_SENS * Time.timeScale;
        if(camAngleVertical < MIN_ANG_VERTICAL) camAngleVertical = MIN_ANG_VERTICAL;
        if(camAngleVertical > MAX_ANG_VERTICAL) camAngleVertical = MAX_ANG_VERTICAL;

        float mouseX = Input.GetAxis("Mouse X");
        camAngleHorizontal += mouseX * HORIZONTAL_SENS * Time.timeScale;
    }

    private void LateUpdate() {
        
        this.transform.position = Sphere.transform.position +  Quaternion.Euler(0, camAngleHorizontal, 0) * cam_sphere * zoom;

        this.transform.eulerAngles = new Vector3(camAngleVertical, camAngleHorizontal, 0);
    }
}
