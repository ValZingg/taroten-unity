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

    public int Type;
    public bool Explored;
    public bool Player_Is_Here;

    public Image icon;
    public Image Done_Image; //le V vert

    // Start is called before the first frame update
    void Start()
    {
        icon = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        Done_Image = gameObject.transform.GetChild(1).gameObject.GetComponent<Image>();

        string portraitname = GameObject.Find("DataTracker").GetComponent<LoadLevel>().PlayerName + "_portrait";
        if (Player_Is_Here) icon.sprite = Resources.Load<Sprite>("Sprites/" + portraitname);

        Done_Image.sprite = Resources.Load<Sprite>("Sprites/Vu");
        if (Explored) Done_Image.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
