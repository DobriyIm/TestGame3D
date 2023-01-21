using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates1 : MonoBehaviour
{
    private const float Timeout = 20;
    private float timeout;
    void Start()
    {
        timeout = Timeout;
    }

    void Update()
    {
        timeout -= Time.deltaTime;
        if(timeout < 0){
            this.gameObject.SetActive(false);
        }else{
            this.transform.localScale = new Vector3(this.transform.localScale.x, timeout/Timeout, this.transform.localScale.z);
        }
    }
}
