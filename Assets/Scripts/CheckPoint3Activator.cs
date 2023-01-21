using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint3Activator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        CheckPoint3.isActivated = true;
    }
}
