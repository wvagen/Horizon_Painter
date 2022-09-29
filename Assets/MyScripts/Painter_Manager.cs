using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Painter_Manager : MonoBehaviour
{

    public Game_Over_Manager gameOverMan;
    public GameObject point, ereaserPoint;
    public Transform pointsContainer, colorsPaletteContainer, pensContainer;

    public GameObject dashedLineVertical, dashedLineHorizontal;

    public Image[] switchBtnsImgs;
    public Color selectedBtnCol, nonSelectedBtnCol;

    private List<Painter_ColorBtn> colorBtns = new List<Painter_ColorBtn>();
    private List<Painter_Pen> pens = new List<Painter_Pen>();

    private Painter_ColorBtn currentColorBtn;
    private Painter_Pen currentPen;

    private GameObject currentPoint, currentPoint1;
    private GameObject[] currentPoints = new GameObject[4];

    private bool canDraw = false;

    public AudioSource myAudioSource;
    public AudioClip selectionClip;

    public Animator myAnim;

    public bool isPenSelected = true;

    Image selectedBtnImg;

    bool isMirrorVertical = false;
    bool isMirrorHorizontal = false;
    bool isMirrorHorizontalAndVertical = false;

    int orderInLayerIndex = 10;

    private void Start()
    {
        Level_Manager();
        Set_Colors_Btn_List();
        Set_Pens_List();

        Select_Random_Color();
        pens[Random.Range(0, pens.Count - 1)].Pen_Selection();
    }

    void Level_Manager()
    {
        if (selectedBtnImg != null)
            selectedBtnImg.color = nonSelectedBtnCol;

        dashedLineVertical.SetActive(false);
        dashedLineHorizontal.SetActive(false);

        isMirrorVertical = false;
        isMirrorHorizontal = false;
        isMirrorHorizontalAndVertical = false;

        switch (Game_Over_Manager.levelIndex)
        {
            case 1:
                isMirrorVertical = true;
                dashedLineVertical.SetActive(true);
                break;
            case 2:
                isMirrorHorizontal = true;
                dashedLineHorizontal.SetActive(true);
                break;
            case 3:
                dashedLineVertical.SetActive(true);
                dashedLineHorizontal.SetActive(true);
                isMirrorHorizontalAndVertical = true;
                break;
        }
        selectedBtnImg = switchBtnsImgs[Game_Over_Manager.levelIndex];
        selectedBtnImg.color = selectedBtnCol;
    }

    public void Switch_Layouts(int layoutIndex)
    {
        Game_Over_Manager.levelIndex = layoutIndex;
        Level_Manager();
    }

    void Set_Colors_Btn_List()
    {
        for (int i = 0; i < colorsPaletteContainer.childCount; i++)
        {
            colorBtns.Add(colorsPaletteContainer.GetChild(i).GetComponent<Painter_ColorBtn>());
        }
    }

    public void Select_Random_Color()
    {
        colorBtns[Random.Range(0, colorBtns.Count)].Color_Selection();
    }

    void Set_Pens_List()
    {
        for (int i = 0; i < pensContainer.childCount; i++)
        {
            pens.Add(pensContainer.GetChild(i).GetComponent<Painter_Pen>());
        }
    }

    public void Show_Are_You_Sure_Panel_Btn()
    {
        myAnim.Play("Display_Are_You_Sure_Panel");
    }

    public void Hide_Are_You_Sure_Panel_Btn()
    {
        myAnim.Play("Hide_Are_You_Sure_Panel");
    }

    public void New_ArtBoard_Btn()
    {
        gameOverMan.RetryLevel();
    }

    private void Update()
    {
        if (canDraw)
        {
            currentPoint.transform.position = Get_Mouse_Pos();
            if (isMirrorVertical) currentPoint1.transform.position = Mirror_Effect_Vertical_Pos(Get_Mouse_Pos());
            else if (isMirrorHorizontal) currentPoint1.transform.position = Mirror_Effect_Horizontal_Pos(Get_Mouse_Pos());
            else if (isMirrorHorizontalAndVertical)
            {
                for (int i = 0; i < 4; i++)
                {
                    currentPoints[i].transform.position = Mirror_Effect_Horizontal_Vertical_Pos(Get_Mouse_Pos())[i];
                }
            }
        }
    }


    public void Set_Current_Col(Painter_ColorBtn currentColorBtn)
    {

        if (Game_Over_Manager.sfxOn) myAudioSource.PlayOneShot(selectionClip);

        if (this.currentColorBtn != null)
        {
            this.currentColorBtn.myFrame.enabled = false;
        }

        this.currentColorBtn = currentColorBtn;
        this.currentColorBtn.myFrame.enabled = true;
    }

    public void Set_Current_Pen(Painter_Pen currentPen)
    {
        if (Game_Over_Manager.sfxOn) myAudioSource.PlayOneShot(selectionClip);

        if (this.currentPen != null)
        {
            this.currentPen.Pen_Selection_Sprite_Change(false);
        }

        this.currentPen = currentPen;
        this.currentPen.Pen_Selection_Sprite_Change(true);
    }


    public void Can_Draw()
    {
        canDraw = true;

        if (isPenSelected)
            currentPoint = Instantiate(point, Vector2.zero, Quaternion.identity, pointsContainer);
        else
            currentPoint = Instantiate(ereaserPoint, Vector2.zero, Quaternion.identity, pointsContainer);

        currentPoint.transform.position = Get_Mouse_Pos();
        currentPoint.GetComponent<TrailRenderer>().startColor = this.currentColorBtn.Get_Color();
        currentPoint.GetComponent<TrailRenderer>().endColor = this.currentColorBtn.Get_Color();

        currentPoint.GetComponent<TrailRenderer>().numCapVertices = 10;
        currentPoint.GetComponent<TrailRenderer>().sortingOrder = orderInLayerIndex;

        currentPoint.GetComponent<TrailRenderer>().startWidth = this.currentPen.penSize;
        currentPoint.GetComponent<TrailRenderer>().endWidth = this.currentPen.penSize;

        if (isMirrorVertical)
        {
            if (isPenSelected)
                currentPoint1 = Instantiate(point, Vector2.zero, Quaternion.identity, pointsContainer);
            else
                currentPoint1 = Instantiate(ereaserPoint, Vector2.zero, Quaternion.identity, pointsContainer);

            currentPoint1.transform.position = Mirror_Effect_Vertical_Pos(Get_Mouse_Pos());
            currentPoint1.GetComponent<TrailRenderer>().startColor = this.currentColorBtn.Get_Color();
            currentPoint1.GetComponent<TrailRenderer>().endColor = this.currentColorBtn.Get_Color();

            currentPoint1.GetComponent<TrailRenderer>().numCapVertices = 10;
            currentPoint1.GetComponent<TrailRenderer>().sortingOrder = orderInLayerIndex;

            currentPoint1.GetComponent<TrailRenderer>().startWidth = this.currentPen.penSize;
            currentPoint1.GetComponent<TrailRenderer>().endWidth = this.currentPen.penSize;
        }

        if (isMirrorHorizontal)
        {
            if (isPenSelected)
                currentPoint1 = Instantiate(point, Vector2.zero, Quaternion.identity, pointsContainer);
            else
                currentPoint1 = Instantiate(ereaserPoint, Vector2.zero, Quaternion.identity, pointsContainer);
            currentPoint1.transform.position = Mirror_Effect_Horizontal_Pos(Get_Mouse_Pos());
            currentPoint1.GetComponent<TrailRenderer>().startColor = this.currentColorBtn.Get_Color();
            currentPoint1.GetComponent<TrailRenderer>().endColor = this.currentColorBtn.Get_Color();

            currentPoint1.GetComponent<TrailRenderer>().numCapVertices = 10;
            currentPoint1.GetComponent<TrailRenderer>().sortingOrder = orderInLayerIndex;

            currentPoint1.GetComponent<TrailRenderer>().startWidth = this.currentPen.penSize;
            currentPoint1.GetComponent<TrailRenderer>().endWidth = this.currentPen.penSize;
        }

        if (isMirrorHorizontalAndVertical)
        {
            for (int i = 0; i < 4; i++)
            {
                if (isPenSelected)
                    currentPoints[i] = Instantiate(point, Vector2.zero, Quaternion.identity, pointsContainer);
                else
                    currentPoints[i] = Instantiate(ereaserPoint, Vector2.zero, Quaternion.identity, pointsContainer);
                currentPoints[i].transform.position = Mirror_Effect_Horizontal_Vertical_Pos(Get_Mouse_Pos())[i];
                currentPoints[i].GetComponent<TrailRenderer>().startColor = this.currentColorBtn.Get_Color();
                currentPoints[i].GetComponent<TrailRenderer>().endColor = this.currentColorBtn.Get_Color();

                currentPoints[i].GetComponent<TrailRenderer>().numCapVertices = 10;
                currentPoints[i].GetComponent<TrailRenderer>().sortingOrder = orderInLayerIndex;

                currentPoints[i].GetComponent<TrailRenderer>().startWidth = this.currentPen.penSize;
                currentPoints[i].GetComponent<TrailRenderer>().endWidth = this.currentPen.penSize;
            }
        }
        orderInLayerIndex++;
    }

    Vector2 Mirror_Effect_Vertical_Pos(Vector2 pointPos)
    {
        return new Vector2(pointPos.x * -1, pointPos.y);
    }

    Vector2 Mirror_Effect_Horizontal_Pos(Vector2 pointPos)
    {
        return new Vector2(pointPos.x, pointPos.y * -1);
    }

    Vector2[] Mirror_Effect_Horizontal_Vertical_Pos(Vector2 pointPos)
    {
        Vector2[] positions = new Vector2[4];

        positions[0] = new Vector2(pointPos.x, pointPos.y);
        positions[1] = new Vector2(pointPos.x * -1, pointPos.y);
        positions[2] = new Vector2(pointPos.x, pointPos.y * -1);
        positions[3] = new Vector2(pointPos.x * -1, pointPos.y * -1);

        return positions;
    }

    Vector2 Get_Mouse_Pos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void Cannot_Draw()
    {
        canDraw = false;
    }

}
