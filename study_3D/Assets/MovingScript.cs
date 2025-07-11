using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScript : MonoBehaviour
{
    [SerializeField] GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        cube.transform.position = new Vector3(1, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
