using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
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

        string imagefilepath = "Sprites/" + charname;
        big_char_image = Resources.Load<Sprite>(imagefilepath); //charge l'image du perso selectionné
    }

    public void ShowInPresentationMenu() //utilisé dans le choix de personnages
    {
        //change la description affichée
        DescriptionUI.text = description;
        big_char_image_UI.sprite = big_char_image;
    }

    public void EnableStartButton() //affiche le bouton pour commencer la partie
    {
        StartButton.SetActive(true);
    }
}
