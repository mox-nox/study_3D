using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision Enter : {collision.gameObject.name}");
    }

    void OnCollisionStay(Collision collision)
    {
        Debug.Log($"Collision Stay : {collision.gameObject.name}");
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log($"Collision Exit : {collision.gameObject.name}");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger Enter : {other.gameObject.name}");
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log($"Trigger Stay : {other.gameObject.name}");
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log($"Trigger Exit : {other.gameObject.name}");
    }
}
