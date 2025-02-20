using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Linq;
using TMPro;
using static UnityEngine.EventSystems.PointerEventData;

public class item : MonoBehaviour , IDragHandler,IPointerDownHandler,IPointerUpHandler,IBeginDragHandler,IEndDragHandler , IDropHandler 
    ,UnityEngine.EventSystems.IPointerEnterHandler,IPointerExitHandler
{
    
    [SerializeField] Sprite LaserTurret;
    public int invslot;
    public string itemtype;
    public int amount;
    public vritem corespondingvritem;

    private bool pointerclickedWithoutdrag = false;
    [NonSerialized] public bool droped = false;
    RectTransform rectTransform;
    Vector2 posBeforeDrag = new Vector2();
    CanvasGroup canvasGroup;
    Canvas canvas;

    // Start is called before the first frame update
    void Awake()
    {
        if (inventory.inventorylist == null) inventory.inventorylist = new List<item>();
        inventory.inventorylist.Add(this);
        rectTransform = gameObject.GetComponent<RectTransform>();
        canvas = uiinventory.instance.GetComponentInParent<Canvas>();
        canvasGroup = transform.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    //public void spawned()
    // {
    //  Debug.Log("spawned");


    // }
    public void setproperties(int invslot,string type, int amount)
    {
       
        this.invslot = invslot+1;
        this.itemtype = type;
        this.amount = amount;
        float x = math.fmod(this.invslot-1,3)+1;
        Debug.Log(x);
        int y = ((this.invslot-1) / 3);
        Debug.Log(y);
        gameObject.AddComponent<CanvasRenderer>();
        Image spriterenderer  =  gameObject.AddComponent<Image>();
        Debug.Log(LaserTurret.name);
        //compares name of the sprite with type
        if(LaserTurret.ToString().Replace(" ","") == type.ToLower().Replace(" ", "") + "(UnityEngine.Sprite)")
        {

            spriterenderer.sprite = LaserTurret;
        }
        
       
        
        
        rectTransform.anchoredPosition = new Vector2(-180f-180f+(180*(x)), 250-(250*(y)));

    }

    public void OnDrag(PointerEventData eventData)
    {

        if(eventData.button == InputButton.Left)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == InputButton.Left)
        {
            Debug.Log("OnPointerDown");
            pointerclickedWithoutdrag = true;
            Debug.Log(eventData.button);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == InputButton.Left)
        {
            Debug.Log("onPointerUp");
            if (pointerclickedWithoutdrag)
            {/*item  click action*/
                if (itemtype == "LaserTurret")
                {
                    BuildingManager.Instance.itemToSpawn = "LaserTurret";

                }
                else if (itemtype == "MissileTurret")
                {
                    BuildingManager.Instance.itemToSpawn = "MissileTurret";

                }

            }
            else
            {
                pointerclickedWithoutdrag = true;

            };
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == InputButton.Left)
        {
            posBeforeDrag = rectTransform.anchoredPosition;
            Debug.Log("OnBeginDrag");
            pointerclickedWithoutdrag = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == InputButton.Left)
        {
            Debug.Log("OnEndDrag");
            canvasGroup.blocksRaycasts = true;
            //if(!dropped) return to prev poz
            if (!droped) { rectTransform.anchoredPosition = posBeforeDrag; }
            else droped = false;
        }
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button == InputButton.Left)
        {
            Debug.Log("OnDrop");
            //swapitems
            // eventData.pointerDrag;

            item otheritem = eventData.pointerDrag.GetComponent<item>();
            vritem tempvritem = inventory.vrinventorylist[otheritem.invslot - 1];
            inventory.vrinventorylist[otheritem.invslot - 1] = inventory.vrinventorylist[invslot - 1];
            inventory.vrinventorylist[invslot - 1] = tempvritem;


            int tempinvslot = otheritem.invslot;
            otheritem.invslot = invslot;
            invslot = tempinvslot;

            otheritem.droped = true;
            otheritem.rectTransform.anchoredPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = otheritem.posBeforeDrag;
        }
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        //instanciate prefab
        Debug.Log("pointer enter");
        inventory.ItemInfoPopUpInstance = Instantiate(inventory.ItemInfoPopUpPrefab, this.transform, false);
        inventory.ItemInfoPopUpInstance.GetComponent<RectTransform>().anchoredPosition += new Vector2(50f,150f);
        inventory.ItemInfoPopUpInstance.GetComponentInChildren<Image>().sprite = LaserTurret;
        inventory.ItemInfoPopUpInstance.GetComponentsInChildren<Transform>().FirstOrDefault(x=> x.gameObject.name == "ItemName").GetComponent<TextMeshProUGUI>().text = itemtype;
        inventory.ItemInfoPopUpInstance.GetComponentsInChildren<Transform>().FirstOrDefault(x => x.gameObject.name == "ItemInfo").GetComponent<TextMeshProUGUI>().text = "some info";
        inventory.ItemInfoPopUpInstance.GetComponentsInChildren<Transform>().FirstOrDefault(x => x.gameObject.name == "Amount").GetComponent<TextMeshProUGUI>().text =  System.Convert.ToString(amount) ;
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("poinerexit");
        if (inventory.ItemInfoPopUpInstance != null) Destroy(inventory.ItemInfoPopUpInstance);
        
    }
}
