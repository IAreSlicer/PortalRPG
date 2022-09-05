using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private void onTriggerEnter(Collider other)
    {
        print("Hit" + other.name);
        Destroy(gameObject);
    }
}
