using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public string TypeOfEnemyToFight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTypeOfEnemy(string type)
    {
        TypeOfEnemyToFight = type;
    }
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject); //Empèche de se faire détruire au changement de scène
    }
}
