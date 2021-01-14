using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoadChar : MonoBehaviour
{
    //===========personnage==============
    private string[] lines;

    public string charname;
    public string description;

    public int charhp;
    public int max_charhp;

    public List<String> cards_str;


    public string CHOSEN_CHAR; //personnage choisi, cette variable est uniquement assignée quand le joueur clique sur "commencer la partie"


    //=================GUI===============
    public Text DescriptionUI;
    public Image big_char_image_UI;
    public Sprite big_char_image;
    public GameObject StartButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject); //sauvegarde une fois la BG_Texture, pour garder le choix du personnage en mémoire
    }

    public void loadchar(string filepath)
    {
        lines = System.IO.File.ReadAllLines(filepath); //Gets all lines

        //Utilise ce qu'on a récupéré et les sépares dans les différentes variables
        charname = lines[0].Substring(lines[0].IndexOf('=') + 1);

        //découpage et parsing de la description
        description = lines[1].Substring(lines[1].IndexOf('=') + 1);
        description = description.Replace('|', '\n');


        max_charhp = Int32.Parse(lines[2].Substring(lines[2].IndexOf('=') + 1));
        charhp = max_charhp;

        //Chargement des cartes
        string startcards_raw = lines[3].Substring(lines[3].IndexOf('=') + 1);
        cards_str = startcards_raw.Split(',').ToList<string>();

        string imagefilepath = "Sprites/" + charname;
        big_char_image = Resources.Load<Sprite>(imagefilepath); //charge l'image du perso selectionné
    }

    public void ShowInPresentationMenu() //utilisé dans le choix de personnages
    {
        //change la description affichée
        DescriptionUI.text = description;
        big_char_image_UI.sprite = big_char_image;
        big_char_image_UI.GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f); // on rétablit l'opacité pour que le sprite soit visible.
    }

    public void EnableStartButton() //affiche le bouton pour commencer la partie
    {
        StartButton.SetActive(true);
    }

    public void ConfirmCharacter(string name) //confirme le personnage choisi
    {
        CHOSEN_CHAR = name;
    }
}
