using System;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Teleporter Other;
    public AudioSource audio;

    private void Start()
    {

    }
    
    private void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        float zPos = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z;

        if (zPos < 0) Teleport(other.transform);
        
    }

    private void Teleport(Transform obj)
    {

        audio.Play(0);
        
        // Position
        Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
        localPos = new Vector3(-localPos.x, localPos.y, -localPos.z);
        obj.position = Other.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);

        float velo = obj.GetComponent<Rigidbody>().velocity.magnitude < 1 ? 1 : obj.GetComponent<Rigidbody>().velocity.magnitude;
        print(velo);


        Quaternion difference = Other.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180, 0));
        obj.rotation = difference * obj.rotation;
        obj.GetComponent<Rigidbody>().AddForce(obj.transform.forward * obj.GetComponent<Rigidbody>().velocity.magnitude, ForceMode.Impulse);
    
    }

    private void OnTriggerEnter(Collider other)
    {

        other.gameObject.layer = 9;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.layer = 8;
    }
}
