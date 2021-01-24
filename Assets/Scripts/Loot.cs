using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loot : MonoBehaviour
{
    public int RandomLoot; //0 == card , 1 == Item

    public GameObject Card_obj;
    public GameObject Item_obj;

    // Start is called before the first frame update
    void Start()
    {
        Card_obj = GameObject.Find("CardSlot_1");
        Item_obj = GameObject.Find("ItemIMG");
        RandomLoot = Random.Range(0, 1);
        

        switch(RandomLoot)
        {
            case 1: //CARTE
                string filepath = "Assets/card_data/ALL_CARDS.txt";
                string[] lines = System.IO.File.ReadAllLines(filepath);
                int randomcard = Random.Range(0, lines.Length);
                filepath = lines[randomcard];
                lines = System.IO.File.ReadAllLines(filepath);
                string raw_name = filepath.Remove(filepath.Length - 4); //enlève le .txt

                Card_obj.GetComponent<Card>().raw_Name = raw_name;
                Card_obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Cards/" + raw_name);
                Card_obj.GetComponent<Card>().LoadDesc();
                break;

            case 2:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
