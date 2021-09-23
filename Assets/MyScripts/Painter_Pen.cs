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

    Sprite initSprite;

    private void Start()
    {
        initSprite = myImg.sprite;
    }

    public void Pen_Selection()
    {
        painterMan.Set_Current_Pen(this);

    }

    public void Pen_Selection_Sprite_Change(bool isSelected)
    {
        if (isSelected) myImg.sprite = selectedSprite;
        else myImg.sprite = initSprite;
    }
    

}
