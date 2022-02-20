using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tutorial_slot_inventario : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    private RectTransform icon;
    private Vector2 initialPos;
    [System.NonSerialized] public bool slotEmpty = true;
    public GameObject productInThisSlot;
    [SerializeField] private Texture emptyTexture;
    [SerializeField] private Canvas canvas;
    public RawImage ics;


    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<RectTransform>();
        //transform.GetComponent<RawImage>().texture = emptyTexture;
        icon.position = new Vector3(icon.position.x, icon.position.y, 1f);
        initialPos = transform.position;
        //number.text = "0";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.GetComponentInParent<tutorial_inventario>().isDragging = true;
        if (!slotEmpty)
        {
            if (icon.anchoredPosition.x < 20 || icon.anchoredPosition.x > 450)
            {
                ics.gameObject.SetActive(true);
                ics.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            else ics.gameObject.SetActive(false);

            icon.anchoredPosition += eventData.delta / canvas.scaleFactor;
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            icon.position = new Vector3(icon.position.x, icon.position.y, -1f);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponentInParent<tutorial_inventario>().isDragging = false;
        icon.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        if (!slotEmpty)
        {
            if (icon.anchoredPosition.x > 20 && icon.anchoredPosition.x < 450)
            {
                transform.position = initialPos;
            }
            else
            {
                ics.gameObject.SetActive(false);
                transform.position = initialPos;
                RemoveProduct();
            }
        }
    }

    public void AddProductInSlot(GameObject product)
    {
        productInThisSlot = product;
        transform.GetComponent<RawImage>().texture = product.GetComponent<icon>().myIcon;
        slotEmpty = false;
    }

    public void RemoveProduct()
    {
        //number.text = (int.Parse(number.text) - 1).ToString();

            transform.GetComponent<RawImage>().texture = emptyTexture;
            slotEmpty = true;
            Camera.main.gameObject.transform.parent.GetChild(1).GetComponent<tutorial_carrello_controller>().RemoveProductFromChart(productInThisSlot);
            transform.parent.parent.parent.GetComponent<tutorial_UI>().RemoveProductFromInventario(productInThisSlot);
            productInThisSlot = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!transform.GetComponentInParent<tutorial_inventario>().isDragging)
        {
            icon.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!transform.GetComponentInParent<tutorial_inventario>().isDragging)
        {
            icon.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
    }
}
