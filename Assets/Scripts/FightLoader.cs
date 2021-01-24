using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightLoader : MonoBehaviour
{
    public GameObject DataTracker;

    //GUI
    public GameObject CharName_UI;
    public GameObject CharImage_UI;
    public GameObject PlayerHP;
    public GameObject PlayerSHIELD;

    public GameObject EnemyName_UI;
    public GameObject EnemyImage_UI;
    public GameObject EnemyHP;
    public GameObject EnemySHIELD;

    //STATS
    public string enemy_name;

    public string[] lines;

    // Start is called before the first frame update
    void Start()
    {
        DataTracker = GameObject.Find("DataTracker");

        CharImage_UI = GameObject.Find("CharImage");
        CharName_UI = GameObject.Find("CharText");
        PlayerHP = GameObject.Find("PlayerHP");
        PlayerSHIELD = GameObject.Find("PlayerSHIELD");

        EnemyImage_UI = GameObject.Find("EnemyImage");
        EnemyName_UI = GameObject.Find("EnemyText");
        EnemyHP = GameObject.Find("EnemyHP");
        EnemySHIELD = GameObject.Find("EnemySHIELD");

        //PV
        PlayerHP.GetComponent<Text>().text = "PV : " + DataTracker.GetComponent<Player>().HP.ToString() + " / " + DataTracker.GetComponent<Player>().max_HP.ToString();

        //assignation des sprites

            //joueur
        string imagefilepath = "Sprites/" + DataTracker.GetComponent<Player>().char_name;
        GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(imagefilepath); //charge l'image du perso selectionné
        imagefilepath = imagefilepath + "_portrait";
        CharImage_UI.GetComponent<Image>().sprite = Resources.Load<Sprite>(imagefilepath);
        CharName_UI.GetComponent<Text>().text = DataTracker.GetComponent<Player>().char_name;

            //ennemi
        enemy_name = ChooseRandomEnemy();
        //Chargment des données
        gameObject.GetComponent<Enemy>().LoadEnemy(enemy_name);

        //assignation au GUI
        EnemyName_UI.GetComponent<Text>().text = gameObject.GetComponent<Enemy>().name;
        imagefilepath = "Sprites/Baddies/" + enemy_name;
        GameObject.Find("EnemySprite").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(imagefilepath);
        //portrait
        imagefilepath = "Sprites/Baddies/" + enemy_name + "_portrait";
        EnemyImage_UI.GetComponent<Image>().sprite = Resources.Load<Sprite>(imagefilepath);
        //stats
        EnemyHP.GetComponent<Text>().text = "PV : " + gameObject.GetComponent<Enemy>().HP.ToString() + " / " + gameObject.GetComponent<Enemy>().max_HP.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string ChooseRandomEnemy()
    {
        string ChosenEnemy = "";

        string type = DataTracker.GetComponent<EnemyTracker>().TypeOfEnemyToFight;
        string filepath;

        if(type == "Enemy")
        {
            filepath = "Assets/Text/" + "Ennemies_" + DataTracker.GetComponent<Player>().current_level.ToString() + ".txt";
            lines = System.IO.File.ReadAllLines(filepath);

            int RandomChoice = Random.Range(0, lines.Length); //Choisi un ennemi aléatoire parmi ceux de la liste
            ChosenEnemy = lines[RandomChoice];
        }

        else if(type == "Elite")
        {
            filepath = "Assets/Text/" + "Elites_" + DataTracker.GetComponent<Player>().current_level.ToString() + ".txt";
            lines = System.IO.File.ReadAllLines(filepath);

            int RandomChoice = Random.Range(0, lines.Length);
            ChosenEnemy = lines[RandomChoice];
        }

        else if(type == "Boss")
        {
            filepath = "Assets/Text/" + "Boss_" + DataTracker.GetComponent<Player>().current_level.ToString() + ".txt";
            lines = System.IO.File.ReadAllLines(filepath);

            //TODO
        }

        return ChosenEnemy;
    }
}
