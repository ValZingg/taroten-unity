using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    public GameObject HUD_stuff;

    // Start is called before the first frame update
    void Start()
    {
        HUD_stuff = GameObject.Find("TopHUD");
        icon = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        Done_Image = gameObject.transform.GetChild(1).gameObject.GetComponent<Image>();

        string portraitname = GameObject.Find("DataTracker").GetComponent<LoadLevel>().PlayerName + "_portrait";
        if (Player_Is_Here) icon.sprite = Resources.Load<Sprite>("Sprites/" + portraitname);

        Done_Image.sprite = Resources.Load<Sprite>("Sprites/Vu");
        if (Explored) Done_Image.enabled = true;

        switch(Type)
        {
            case 2:
                gameObject.GetComponent<Button>().onClick.AddListener(delegate { HUD_stuff.GetComponent<SwitchScene>().SwitchSceneNow("Enemy"); }); //crée le bouton
                break;

            case 3:
                gameObject.GetComponent<Button>().onClick.AddListener(delegate { HUD_stuff.GetComponent<SwitchScene>().SwitchSceneNow("Treasure"); });
                break;

            case 4:
                gameObject.GetComponent<Button>().onClick.AddListener(delegate { HUD_stuff.GetComponent<SwitchScene>().SwitchSceneNow("Elite"); });
                break;

            case 5:
                gameObject.GetComponent<Button>().onClick.AddListener(delegate { HUD_stuff.GetComponent<SwitchScene>().SwitchSceneNow("Boss"); });
                break;
        }
    }

    void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("test");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SizeUp()
    {
        this.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 0);
    }

    public void SizeDown()
    {
        this.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 0);
    }
}
