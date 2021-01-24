using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEditor.Events;

public class ExploreSpot : MonoBehaviour
{
    /*
        Ce fichier est la classe des cases sur lesquelles
        vous pouvez cliquer pour explorer lors du jeu.
        
     */

    public int ID;
    public int Type;
    public bool Explored;
    public bool Player_Is_Here;
    public bool Can_Move_Here = false;

    public Image icon;
    public Image Done_Image; //le V vert
    public Image Glow_Border;

    public Button BUTTON;

    public GameObject HUD_stuff;

    private UnityEngine.Events.UnityAction buttonCallback;



    // Start is called before the first frame update
    void Start()
    {
        HUD_stuff = GameObject.Find("TopHUD");
        icon = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        Done_Image = gameObject.transform.GetChild(1).gameObject.GetComponent<Image>();

        string portraitname = GameObject.Find("LevelLoader").GetComponent<LoadLevel>().PlayerName + "_portrait";
        if (Player_Is_Here) icon.sprite = Resources.Load<Sprite>("Sprites/" + portraitname);

        Done_Image.sprite = Resources.Load<Sprite>("Sprites/Vu");
        if (Explored) Done_Image.enabled = true;

        Glow_Border = gameObject.transform.GetChild(2).gameObject.GetComponent<Image>();
        if (Can_Move_Here) Glow_Border.enabled = true;

        switch(Type)
        {
            case 2:
                BUTTON.onClick.AddListener(delegate {
                    if(Can_Move_Here) HUD_stuff.GetComponent<SwitchScene>().SwitchSceneNow("Fight");
                    GameObject.Find("DataTracker").GetComponent<EnemyTracker>().SetTypeOfEnemy("Enemy"); //sauve le fait qu'on va combattre un ennemi de ce type

                    if (GameObject.Find("DataTracker").GetComponent<Player>().PathChosen == "Aucun")// si le joueur n'a pas encore choisi de chemin
                    {
                        if (ID == 1) //chemin du haut
                        {
                            GameObject.Find("DataTracker").GetComponent<Player>().PathChosen = "Haut";
                        }
                        else //chemin du bas
                        {
                            GameObject.Find("DataTracker").GetComponent<Player>().PathChosen = "Bas";
                        }
                    }
                });
                break;

            case 3:
                BUTTON.onClick.AddListener(delegate { 
                    if(Can_Move_Here) HUD_stuff.GetComponent<SwitchScene>().SwitchSceneNow("Treasure"); //crée le lien avec la scène trésor

                    if (GameObject.Find("DataTracker").GetComponent<Player>().PathChosen == "Aucun")// si le joueur n'a pas encore choisi de chemin
                    {
                        if (ID == 1) //chemin du haut
                        {
                            GameObject.Find("DataTracker").GetComponent<Player>().PathChosen = "Haut";
                        }
                        else //chemin du bas
                        {
                            GameObject.Find("DataTracker").GetComponent<Player>().PathChosen = "Bas";
                        }
                    }
                });
                break;

            case 4:
                BUTTON.onClick.AddListener(delegate { 
                    if(Can_Move_Here) HUD_stuff.GetComponent<SwitchScene>().SwitchSceneNow("Fight");
                    GameObject.Find("DataTracker").GetComponent<EnemyTracker>().SetTypeOfEnemy("Elite"); //sauve le fait qu'on va combattre un ennemi de ce type

                    if (GameObject.Find("DataTracker").GetComponent<Player>().PathChosen == "Aucun")// si le joueur n'a pas encore choisi de chemin
                    {
                        if (ID == 1) //chemin du haut
                        {
                            GameObject.Find("DataTracker").GetComponent<Player>().PathChosen = "Haut";
                        }
                        else //chemin du bas
                        {
                            GameObject.Find("DataTracker").GetComponent<Player>().PathChosen = "Bas";
                        }
                    }
                });

                
                break;

            case 5:
                BUTTON.onClick.AddListener(delegate { 
                    if(Can_Move_Here) HUD_stuff.GetComponent<SwitchScene>().SwitchSceneNow("Fight");
                    GameObject.Find("DataTracker").GetComponent<EnemyTracker>().SetTypeOfEnemy("Boss"); //sauve le fait qu'on va combattre un ennemi de ce type

                    if (GameObject.Find("DataTracker").GetComponent<Player>().PathChosen == "Aucun")// si le joueur n'a pas encore choisi de chemin
                    {
                        if (ID == 1) //chemin du haut
                        {
                            GameObject.Find("DataTracker").GetComponent<Player>().PathChosen = "Haut";
                        }
                        else //chemin du bas
                        {
                            GameObject.Find("DataTracker").GetComponent<Player>().PathChosen = "Bas";
                        }
                    }
                });

                
                break;
        }
    }

    

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchSceneNow(string scene) => SceneManager.LoadScene(scene);
}
