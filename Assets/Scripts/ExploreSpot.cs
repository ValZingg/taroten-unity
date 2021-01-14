using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExploreSpot : MonoBehaviour
{
    /*
        Ce fichier est la classe des cases sur lesquelles
        vous pouvez cliquer pour explorer lors du jeu.
        
     */

    public string Type;
    public bool Explored;
    public bool Player_Is_Here;

    public Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = gameObject.GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
