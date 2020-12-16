using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //===================================
    public float HP;
    public float max_HP;
    //===================================

    Player(float hp)
    {
        max_HP = hp;
        HP = max_HP;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
