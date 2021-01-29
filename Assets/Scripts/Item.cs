using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string raw_name;
    public string Name;
    public string Desc;

    public GameObject TopHUD;

    public Image DescBG;
    public Text DescUI;

    public Image ItemIcon;

    public GameObject Canvass;

    public bool ZoomOnHover = true;
    Vector3 cachedScale;

    // Start is called before the first frame update
    void Start()
    {
        TopHUD = GameObject.Find("TopHUD");
        cachedScale = transform.localScale;

        DescBG = GameObject.Find("DescriptionPanel").GetComponent<Image>();
        DescUI = GameObject.Find("DescriptionText").GetComponent<Text>();
        Canvass = GameObject.Find("Canvas");
        ItemIcon = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ZoomOnHover) transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        DescBG.enabled = true;
        DescUI.enabled = true;
        DescUI.text = Name + "\n\n" + Desc;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ZoomOnHover) transform.localScale = cachedScale;
        DescBG.enabled = false;
        DescUI.enabled = false;
    }

    public void LoadItem(string rawname)
    {
        string filepath = "Assets/Text/items/" + rawname + ".txt";
        string[] lines = System.IO.File.ReadAllLines(filepath);

        Name = lines[0];
        Desc = lines[1];
        Desc = Desc.Replace('|', '\n');

        ItemIcon.sprite = Resources.Load<Sprite>("Sprites/" + raw_name);
    }
}
