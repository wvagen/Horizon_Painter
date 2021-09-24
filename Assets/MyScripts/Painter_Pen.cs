using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Painter_Pen : MonoBehaviour
{
    public float penSize;
    public Sprite selectedSprite;

    public Painter_Manager painterMan;
    public Image myImg;

    public bool isPen = true;

    Sprite initSprite;

    private void Start()
    {
        initSprite = myImg.sprite;
    }

    public void Pen_Selection()
    {
        painterMan.Set_Current_Pen(this);
        if (isPen && !painterMan.isPenSelected)
        {
            painterMan.isPenSelected = true;
            painterMan.Select_Random_Color();
        }
        
        else if (!isPen)
        {
            painterMan.isPenSelected = false;
        }
    }

    public void Pen_Selection_Sprite_Change(bool isSelected)
    {
        if (isSelected) myImg.sprite = selectedSprite;
        else myImg.sprite = initSprite;
    }
    

}
