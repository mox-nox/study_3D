using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pico : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField] InputScript player;

    public void Attack()
    {
        player.AttackMonster();
    }
}
