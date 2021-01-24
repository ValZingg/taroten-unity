using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CombatManager : MonoBehaviour
{
    public bool player_turn;
    public bool initialized_turn;

    int current_turn = 0;

    public GameObject DataTracker;
    public GameObject TopHUD;

    public List<string> playerdeck;

    public List<string> inpioche;
    public List<string> defausse;
    public List<string> InHand;

    public int ChosenCards_amount;
    public List<string> ChosenCards;

    public List<GameObject> cardslots = new List<GameObject>();

    public int LastEnemyDamage;

    // Start is called before the first frame update
    void Start()
    {
        DataTracker = GameObject.Find("DataTracker");
        TopHUD = GameObject.Find("TopHUD");

        //Cartes
        playerdeck = DataTracker.GetComponent<Player>().Cards;
        inpioche = playerdeck;

        //Slots de cartes
        cardslots.Add(GameObject.Find("CardSlot_1"));
        cardslots.Add(GameObject.Find("CardSlot_2"));
        cardslots.Add(GameObject.Find("CardSlot_3"));
        cardslots.Add(GameObject.Find("CardSlot_4"));


        player_turn = true; // le 1er tour est toujours au joueur
        initialized_turn = false;

        current_turn = 1;

    }

    // Update is called once per frame
    void Update()
    {
        //if(DataTracker.GetComponent<Player>().HP <= 0)

        if(player_turn) //TOUR DU JOUEUR 
        {
            if(!initialized_turn) //initialiser = avopir pioché les cartes etc
            {
                ChosenCards_amount = 0; //aucune cartes choisies au départ

                //piochage de carte
                if(inpioche.Count < 4) //vérifier si il y a assez de carte à piocher
                {
                    for(int i = 0;i < defausse.Count;i++) //remet la défausse dans la pioche
                    {
                        inpioche.Add(defausse[i]);
                    }
                    defausse.Clear(); //vide la défausse et remet tout dans le 1er paquet, mélange, et distribue
                    inpioche = ShuffleList(inpioche); //Mélange
                }
                
                for(int i = 0; i < 4;i++) //pioche
                {
                    int randnumb = Random.Range(0, inpioche.Count);
                    cardslots[i].GetComponent<Card>().raw_Name = inpioche[randnumb]; //obtient le nom brut ex : 'FOO'
                    cardslots[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Cards/" + inpioche[randnumb]);
                    cardslots[i].GetComponent<Card>().LoadDesc();
                    InHand.Add(inpioche[randnumb]); // ajoute dans la main

                    inpioche.RemoveAt(randnumb);
                }


                initialized_turn = true;
            }
        }
    }

    public List<string> ShuffleList(List<string> Listcard)
    {
        for (int i = 0; i < Listcard.Count; i++)
        {
            string temp = Listcard[i];
            int randomIndex = Random.Range(i, Listcard.Count);
            Listcard[i] = Listcard[randomIndex];
            Listcard[randomIndex] = temp;
        }

        return Listcard;
    }

    public void EndTurn()
    {
        player_turn = false;
        initialized_turn = false;

        int NET_HP_GAIN = 0;
        int NET_ATTACK_DONE = 0;

        bool lifesteal = false;
        bool armorconversion = false;
        bool doubledamage_DEA = false;
        bool tripledamage_HAN = false;
        bool doubleheal_LOV = false;
        bool quadrupledamage_STR = false;
        bool cinqtupledamage_STR = false;

        int STR_TO_REMOVE = 0;

        bool noenemyattack = false;

        //eteignage des lumières
        for(int i = 0; i < cardslots.Count;i++)
            cardslots[i].GetComponent<Card>().TurnOffLights();
        
        //===============================EFFETS DE CARTE
        for(int i = 0;i < ChosenCards.Count;i++)
        {
            //Probablement la manière la plus dégueulasse de faire ça, mais il est presque minuit, et j'ai pas le temps.

            // LE MONDE
            if(ChosenCards[i] == "WOR")
            {
                switch(i)
                {
                    case 0:
                        NET_HP_GAIN += inpioche.Count();
                        break;

                    case 1:
                        for(int y = 0;y < inpioche.Count;y++)
                        {
                            defausse.Add(inpioche[y]);
                            NET_HP_GAIN++;
                        }
                        break;

                    case 2:
                        NET_ATTACK_DONE = NET_ATTACK_DONE + inpioche.Count + InHand.Count + defausse.Count;
                        break;
                }
            }

            // LA MORT
            if (ChosenCards[i] == "DEA")
            {
                switch (i)
                {
                    case 0:
                        NET_ATTACK_DONE += 5;
                        break;

                    case 1:
                        lifesteal = true;
                        break;

                    case 2:
                        doubledamage_DEA = true;
                        break;
                }
            }

            //LE CHARIOT
            if (ChosenCards[i] == "CHA")
            {
                switch (i)
                {
                    case 0:
                        if (current_turn == 1) NET_ATTACK_DONE += 10;
                        else NET_ATTACK_DONE++;
                        break;

                    case 1:
                        if (current_turn == 1) NET_HP_GAIN += 10;
                        else NET_HP_GAIN++;
                        break;

                    case 2:
                        if(current_turn == 1)
                        {
                            NET_ATTACK_DONE += 10;
                            NET_HP_GAIN += 10;
                        }
                        else
                        {
                            NET_ATTACK_DONE += 1;
                            NET_HP_GAIN += 1;
                        }
                        break;
                }
            }

            // LE DIABLE
            if (ChosenCards[i] == "DEV")
            {
                switch (i)
                {
                    case 0:
                        NET_ATTACK_DONE += 8;
                        NET_HP_GAIN -= 3;
                        break;

                    case 1:
                        if(TopHUD.GetComponent<Enemy>().SHIELD > 0) TopHUD.GetComponent<Enemy>().SHIELD = 0;
                        break;

                    case 2:
                        if (CheckIfCardIsIn("DEA")) NET_ATTACK_DONE += 10;
                        break;
                }
            }

            // L'EMPRESSE
            if (ChosenCards[i] == "EMF")
            {
                switch (i)
                {
                    case 0:
                        DataTracker.GetComponent<Player>().SHIELD += 5;
                        break;

                    case 1:
                        NET_HP_GAIN += 3;
                        break;

                    case 2:
                        if (CheckIfCardIsIn("EMP")) NET_ATTACK_DONE += 10;
                        else NET_ATTACK_DONE += 3;
                        break;
                }
            }

            // L'EMPEREUR
            if (ChosenCards[i] == "EMP")
            {
                switch (i)
                {
                    case 0:
                        NET_ATTACK_DONE += 3;
                        break;

                    case 1:
                        armorconversion = true;
                        break;

                    case 2:
                        if (CheckIfCardIsIn("EMF")) NET_HP_GAIN += 6;
                        else NET_HP_GAIN += 3;
                        break;
                }
            }

            // LE FOU
            if (ChosenCards[i] == "FOO")
            {
                switch (i)
                {
                    case 0:
                        TopHUD.GetComponent<Enemy>().ATK--;
                        break;

                    case 1:
                        if (TopHUD.GetComponent<Enemy>().SHIELD > 0)
                        {
                            DataTracker.GetComponent<Player>().SHIELD = TopHUD.GetComponent<Enemy>().SHIELD;
                            TopHUD.GetComponent<Enemy>().SHIELD = 0;
                        }
                        break;

                    case 2:
                        int temphp = DataTracker.GetComponent<Player>().HP;
                        DataTracker.GetComponent<Player>().HP = TopHUD.GetComponent<Enemy>().HP;
                        TopHUD.GetComponent<Enemy>().HP = temphp;
                        break;
                }
            }

            // Le PEndu
            if (ChosenCards[i] == "HAN")
            {
                switch (i)
                {
                    case 0:
                        NET_HP_GAIN -= 7;
                        tripledamage_HAN = true;
                        break;

                    case 1:
                        if (CheckIfCardIsIn("DEV")) NET_ATTACK_DONE += 8;
                        break;

                    case 2:
                        int randomnumber = Random.Range(0, 2);
                        if (randomnumber == 1) TopHUD.GetComponent<Enemy>().HP = 0;
                        else DataTracker.GetComponent<Player>().HP = 0;
                        break;
                }
            }

            // L'HERMITE
            if (ChosenCards[i] == "HER")
            {
                switch (i)
                {
                    case 0:
                        NET_HP_GAIN = NET_HP_GAIN += DataTracker.GetComponent<Player>().current_level;
                        break;

                    case 1:
                        DataTracker.GetComponent<Player>().SHIELD += 3;
                        break;

                    case 2:
                        NET_ATTACK_DONE = NET_ATTACK_DONE + DataTracker.GetComponent<Player>().SHIELD;
                        break;
                }
            }

            // HIERO
            if (ChosenCards[i] == "HIE")
            {
                switch (i)
                {
                    case 0:
                        NET_HP_GAIN += 2;
                        break;

                    case 1:
                        NET_HP_GAIN -= 5;
                        DataTracker.GetComponent<Player>().max_HP++;
                        break;

                    case 2:
                        DataTracker.GetComponent<Player>().HP = DataTracker.GetComponent<Player>().HP * 2;
                        if (DataTracker.GetComponent<Player>().HP > DataTracker.GetComponent<Player>().max_HP) DataTracker.GetComponent<Player>().HP = DataTracker.GetComponent<Player>().max_HP;
                        break;
                }
            }

            // GRANDE PRETRESSE
            if (ChosenCards[i] == "HPS")
            {
                switch (i)
                {
                    case 0:
                        if (CheckIfCardIsIn("EMF")) NET_ATTACK_DONE += 4;
                        else NET_ATTACK_DONE += 2;
                        break;

                    case 1:
                        NET_HP_GAIN += 3;
                        break;

                    case 2:
                        if (CheckIfCardIsIn("EMP") && CheckIfCardIsIn("EMF")) NET_ATTACK_DONE += 30;
                        break;
                }
            }

            // JUGEMENT
            if (ChosenCards[i] == "JUD")
            {
                switch (i)
                {
                    case 0:
                        if (TopHUD.GetComponent<Enemy>().HP < 5) TopHUD.GetComponent<Enemy>().HP = 0;
                        break;

                    case 1:
                        if (TopHUD.GetComponent<Enemy>().ATK < 7) TopHUD.GetComponent<Enemy>().HP = 0;
                        else DataTracker.GetComponent<Player>().HP = 0;
                        break;

                    case 2:
                        TopHUD.GetComponent<Enemy>().HP = 0;
                        DataTracker.GetComponent<Player>().max_HP -= 5;
                        if (DataTracker.GetComponent<Player>().HP > DataTracker.GetComponent<Player>().max_HP) DataTracker.GetComponent<Player>().HP = DataTracker.GetComponent<Player>().max_HP;
                        break;
                }
            }

            // JUSTICE
            if (ChosenCards[i] == "JUS")
            {
                switch (i)
                {
                    case 0:
                        NET_ATTACK_DONE += LastEnemyDamage;
                        break;

                    case 1:
                        DataTracker.GetComponent<Player>().HP = 1;
                        DataTracker.GetComponent<Player>().SHIELD += 40;
                        break;

                    case 2:
                        if (DataTracker.GetComponent<Player>().HP == 1) DataTracker.GetComponent<Player>().HP = DataTracker.GetComponent<Player>().max_HP;
                        break;
                }
            }

            // AMANTS
            if (ChosenCards[i] == "LOV")
            {
                switch (i)
                {
                    case 0:
                        NET_ATTACK_DONE += 2;
                        NET_HP_GAIN += 2;
                        break;

                    case 1:
                        doubleheal_LOV = true;
                        break;

                    case 2:
                        noenemyattack = true;
                        break;
                }
            }

            // MAGICIEN
            if (ChosenCards[i] == "MAG")
            {
                switch (i)
                {
                    case 0:
                        if (CheckIfCardIsIn("DEV")) NET_ATTACK_DONE += 10;
                        else NET_ATTACK_DONE += 4;
                        break;

                    case 1:
                        NET_HP_GAIN += 3;
                        break;

                    case 2:
                        DataTracker.GetComponent<Player>().max_HP -= 1;
                        DataTracker.GetComponent<Player>().ATK += 4;
                        break;
                }
            }

            // LA LUNE
            if (ChosenCards[i] == "MOO")
            {
                switch (i)
                {
                    case 0:
                        DataTracker.GetComponent<Player>().SHIELD += 2;
                        break;

                    case 1:
                        DataTracker.GetComponent<Player>().SHIELD += 5;
                        break;

                    case 2:
                        if(CheckIfCardIsIn("SUN") && CheckIfCardIsIn("STA")) DataTracker.GetComponent<Player>().SHIELD += 30;
                        break;
                }
            }

            //L'ETOILE
            if (ChosenCards[i] == "STA")
            {
                switch (i)
                {
                    case 0:
                        NET_HP_GAIN += 3;
                        break;

                    case 1:
                        NET_HP_GAIN += 6;
                        break;

                    case 2:
                        if (CheckIfCardIsIn("SUN") && CheckIfCardIsIn("MOO")) DataTracker.GetComponent<Player>().HP = DataTracker.GetComponent<Player>().max_HP;
                        break;
                }
            }

            //FORCE
            if (ChosenCards[i] == "STR")
            {
                switch (i)
                {
                    case 0:
                        NET_ATTACK_DONE += 3;
                        break;

                    case 1:
                        STR_TO_REMOVE += 2;
                        DataTracker.GetComponent<Player>().ATK += 2;
                        break;

                    case 2:
                        if (CheckIfCardIsIn("DEV")) cinqtupledamage_STR = true;
                        else quadrupledamage_STR = true;
                        break;
                }
            }

            //SOLEIL
            if (ChosenCards[i] == "SUN")
            {
                switch (i)
                {
                    case 0:
                        NET_ATTACK_DONE += 3;
                        break;

                    case 1:
                        NET_ATTACK_DONE += 6;
                        break;

                    case 2:
                        if (CheckIfCardIsIn("MOO") && CheckIfCardIsIn("STA")) NET_ATTACK_DONE += 20;
                        break;
                }
            }

            //TEMPERANCE
            if (ChosenCards[i] == "TEM")
            {
                switch (i)
                {
                    case 0:
                        NET_HP_GAIN -= 2;
                        STR_TO_REMOVE++;
                        DataTracker.GetComponent<Player>().ATK++;
                        break;

                    case 1:
                        NET_ATTACK_DONE = inpioche.Count();
                        break;

                    case 2:
                        STR_TO_REMOVE += 2;
                        TopHUD.GetComponent<Enemy>().ATK -= 2;
                        DataTracker.GetComponent<Player>().ATK += 2;
                        break;
                }
            }

            //LA TOUR
            if (ChosenCards[i] == "TOW")
            {
                switch (i)
                {
                    case 0:
                        NET_HP_GAIN -= 1;
                        DataTracker.GetComponent<Player>().SHIELD += 6;
                        break;

                    case 1:
                        if(DataTracker.GetComponent<Player>().SHIELD > 0) DataTracker.GetComponent<Player>().SHIELD = DataTracker.GetComponent<Player>().SHIELD * 2;
                        break;

                    case 2:
                        if(DataTracker.GetComponent<Player>().SHIELD > 0) NET_ATTACK_DONE = DataTracker.GetComponent<Player>().SHIELD * 2;
                        break;
                }
            }

            //ROUE DE FORTUNE
            if (ChosenCards[i] == "STR")
            {
                switch (i)
                {
                    case 0:
                        //TODO
                        break;

                    case 1:
                        //TODO
                        break;

                    case 2:
                        //TODO
                        break;
                }
            }


        }
        ChosenCards.Clear();
       
        //Effets additionels
        if (doubledamage_DEA) NET_ATTACK_DONE = NET_ATTACK_DONE * 2;
        if (tripledamage_HAN) NET_ATTACK_DONE = NET_ATTACK_DONE * 3;
        if (lifesteal) NET_HP_GAIN += NET_ATTACK_DONE;
        if (armorconversion) DataTracker.GetComponent<Player>().SHIELD += NET_ATTACK_DONE;
        if (doubleheal_LOV) NET_HP_GAIN = NET_HP_GAIN * 2;
        if (quadrupledamage_STR) NET_ATTACK_DONE = NET_ATTACK_DONE * 4;
        if (cinqtupledamage_STR) NET_ATTACK_DONE = NET_ATTACK_DONE * 5;
        //==========dégats et soins=========================
        DataTracker.GetComponent<Player>().HP += NET_HP_GAIN;
        if (DataTracker.GetComponent<Player>().HP > DataTracker.GetComponent<Player>().max_HP) DataTracker.GetComponent<Player>().HP = DataTracker.GetComponent<Player>().max_HP;
        TopHUD.GetComponent<Enemy>().HP -= NET_ATTACK_DONE + DataTracker.GetComponent<Player>().ATK;

        Debug.Log("Inflige " + NET_ATTACK_DONE + " de dégats !!!");
        Debug.Log(" Restore " + NET_HP_GAIN + "de vie !!!");
        UpdateUI();
        //==============================================

        //VIDAGE DE LA MAIN
        for(int i = 0; i < InHand.Count;i++) //ajoute toutes les cartes en main à la défausse
        {
            defausse.Add(InHand[i]);
        }
        InHand.Clear();



        if(TopHUD.GetComponent<Enemy>().HP <= 0) //L'ennemi est mort donc on quitte
        {
            //TODO : VICTORY
            DataTracker.GetComponent<Player>().ATK -= STR_TO_REMOVE;
        }
        else
        {
            //TOUR DE L'ENNEMI
            int enemyaction = Random.Range(0, 2);
            switch (enemyaction)
            {
                default:
                    throw new MissingComponentException();

                case 0: //ATTAQUE
                    Debug.Log("ATTAQUE!");
                    if (DataTracker.GetComponent<Player>().SHIELD > 0) DataTracker.GetComponent<Player>().SHIELD -= TopHUD.GetComponent<Enemy>().ATK;
                    if(DataTracker.GetComponent<Player>().SHIELD < 0)
                    {
                        DataTracker.GetComponent<Player>().HP += DataTracker.GetComponent<Player>().SHIELD;
                        DataTracker.GetComponent<Player>().SHIELD = 0;
                    }
                    break;

                case 1: //DEFENSE
                    Debug.Log("DEFENSE!");
                    TopHUD.GetComponent<Enemy>().SHIELD += TopHUD.GetComponent<Enemy>().DEF;
                    break;

            }

            current_turn++;
            player_turn = true;
        }
        UpdateUI();
    }

    public bool CheckIfCardIsIn(string card)
    {
        bool to_return = false;
        for (int y = 0; y < ChosenCards.Count; y++)
        {
            if (ChosenCards[y] == card) to_return = true;
        }
        return to_return;
    }

    public void UpdateUI()
    {
        //JOUEUR
        TopHUD.GetComponent<FightLoader>().PlayerHP.GetComponent<Text>().text = "PV : " + DataTracker.GetComponent<Player>().HP.ToString() + " / " + DataTracker.GetComponent<Player>().max_HP;
        TopHUD.GetComponent<FightLoader>().PlayerSHIELD.transform.GetChild(0).GetComponent<Text>().text = DataTracker.GetComponent<Player>().SHIELD.ToString();

        //ENNEMI
        TopHUD.GetComponent<FightLoader>().EnemyHP.GetComponent<Text>().text = "PV :" + TopHUD.GetComponent<Enemy>().HP.ToString() + " / " + TopHUD.GetComponent<Enemy>().max_HP;
        TopHUD.GetComponent<FightLoader>().EnemySHIELD.transform.GetChild(0).GetComponent<Text>().text = TopHUD.GetComponent<Enemy>().SHIELD.ToString();
    }

}
