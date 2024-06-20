using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InteractHandler : MonoBehaviour
{
    public GameObject UIPrefab; 
    [FormerlySerializedAs("InteractableIcone")]
    public Sprite InteractablePointer;
    public Sprite NormalPointer;
    public LayerMask layerMask;
    [SerializeField] InventorySys inv;
    [SerializeField] GameObject uiBar;
    [SerializeField] GameObject selectedBar;
    Image m_PointerImage;
    private Vector3 m_OriginalPointerSize;
    private Ray ray;
    private float rayLength = 2.0f;
    private float distFromPlayer = 2f;
    private bool interactableHit = false;
    private OnInteract onInteract;
    public int selectedInvSlot = 0;
    public bool invSlotFilled;
    private RectTransform uiBarRT;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        Instantiate(UIPrefab);

        var mainCam = Camera.main;
        var cinemachineBrain = mainCam.GetComponent<CinemachineBrain>();
        if (cinemachineBrain == null)
            mainCam.gameObject.AddComponent<CinemachineBrain>();

        var centerPoint = GameObject.Find("CenterPoint");
        if (centerPoint != null)
        {
            m_PointerImage = centerPoint.GetComponent<Image>();
            m_OriginalPointerSize = centerPoint.transform.localScale;
        }
        inv.InvInit();
        uiBarRT = selectedBar.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the player is pointing at an object in range to be interacted with the object gets added to a list of targets that can be interacted with.
        //
        ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        //OnInteract[] targets = null;
        RaycastHit hit;

        interactableHit = false;
        if (Physics.Raycast(ray, out hit, rayLength, layerMask))
        {
            interactableHit = true;
            onInteract = hit.collider.gameObject.GetComponentInParent<OnInteract>();
            //oninteract will need different versions for herbs and the tools

            m_PointerImage.sprite = InteractablePointer;
            m_PointerImage.transform.localScale = m_OriginalPointerSize * 2.0f;

            /*if (interacts.Length > 0)
            {
                displayInteractable = true;
                targets = interacts;
                m_PointerImage.color = Color.white;
                
                foreach (var target in targets)
                {
                    if (!target.isActiveAndEnabled)
                    {
                        m_PointerImage.color = Color.grey;
                        break;
                    }
                }
            }*/
        }

        else
        {
            m_PointerImage.sprite = NormalPointer;
            m_PointerImage.color = Color.white;
            m_PointerImage.transform.localScale = m_OriginalPointerSize;
            onInteract = null;
        }
    }

    void OnInteraction()
    {
        /*the logic for detecting whether the raycast has hit an interactable object is already handled with the crosshair changing. Would
        it be a better idea to figure out how to reuse the logic rather than rewriting it. More time will have to be spent on it but perhaps
        it will make it easier in the future to implement something like this well. Whereas I would just continuously be using a less efficient
        method to save time now.*/
    
        if (interactableHit)
        {
            onInteract.Interact();
        }
    }

    void OnDropItem()
    {
        if (selectedInvSlot != 0)
        {
            DropItem(selectedInvSlot, gameObject.transform.position);
        }
    }

    public bool AddItem(ItemData item)
    {
        //find an empty inventory spot that does not go over max item count and add
        for (int i = 0; i < inv.items.Count; i++)
        {
            if (inv.items[i] == null)
            {
                inv.items[i] = item;
                uiBar.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = item.icon;
                uiBar.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                return true;
            }
        }

        Debug.Log("Inv Full");
        return false;  
    }

    public ItemData RemoveItem(int index)
    {
        //selected item from inv/hotbar is removed. This is used when using an item like using an item in a recipe. The item should be transferred to 
        //wherever it is used like into the cauldron.
        int ind = index - 1;
        ItemData remItem = inv.items[ind];
        if (remItem != null)
        {
            inv.items[ind] = null;
            uiBar.transform.GetChild(ind).GetChild(0).gameObject.SetActive(false);
        }
        return remItem;
    }

    public void DropItem(int index, Vector3 playerPos)
    {
        //selected item is spawned in a suitable area near player, ideally in front and dropped. This item should have the same properties as it did in the inv
        //and before it was put into the inv.
        Vector3 rayFwd = new(ray.direction.x, 0.5f, ray.direction.z);
        Vector3 dropLoc = playerPos + rayFwd * distFromPlayer;
        ItemData droppedItem = RemoveItem(index);

        if(droppedItem != null)
        { 
            Instantiate(droppedItem.itemPrefab, dropLoc, Quaternion.identity);
        }    
    }

    private void InvBarNav()
    {
        if (selectedInvSlot == 0)
        {
            selectedBar.SetActive(false);
            invSlotFilled = false;
        }

        if (selectedInvSlot == 1)
        {
            selectedBar.SetActive(true);
            
            uiBarRT.anchoredPosition3D = new Vector3(-160f, uiBarRT.anchoredPosition3D.y, uiBarRT.anchoredPosition3D.z);
        }

        if (selectedInvSlot == 2)
        {
            selectedBar.SetActive(true);
            uiBarRT.anchoredPosition3D = new Vector3(-80f, uiBarRT.anchoredPosition3D.y, uiBarRT.anchoredPosition3D.z);
        }

        if (selectedInvSlot == 3)
        {
            selectedBar.SetActive(true);
            uiBarRT.anchoredPosition3D = new Vector3(0f, uiBarRT.anchoredPosition3D.y, uiBarRT.anchoredPosition3D.z);
        }

        if (selectedInvSlot == 4)
        {
            selectedBar.SetActive(true);
            uiBarRT.anchoredPosition3D = new Vector3(80f, uiBarRT.anchoredPosition3D.y, uiBarRT.anchoredPosition3D.z);
        }

        if (selectedInvSlot == 5)
        {
            selectedBar.SetActive(true);
            uiBarRT.anchoredPosition3D = new Vector3(160f, uiBarRT.anchoredPosition3D.y, uiBarRT.anchoredPosition3D.z);
        }
        if (selectedInvSlot != 0)
        {
            if (inv.items[selectedInvSlot-1] != null) {invSlotFilled = true;}
            else {invSlotFilled = false;}
        }
    }

    private void OnBarSlot1()
    {
        if (selectedInvSlot != 1) { selectedInvSlot = 1; }
        else{ selectedInvSlot = 0;}
        InvBarNav();
    } 

    private void OnBarSlot2()
    {
        if (selectedInvSlot != 2) { selectedInvSlot = 2; }
        else{ selectedInvSlot = 0;}
        InvBarNav();
    }

    private void OnBarSlot3()
    {
        if (selectedInvSlot != 3) { selectedInvSlot = 3; }
        else{ selectedInvSlot = 0;}
        InvBarNav();
    }

    private void OnBarSlot4()
    {
        if (selectedInvSlot != 4) { selectedInvSlot = 4; }
        else{ selectedInvSlot = 0;}
        InvBarNav();
    }

    private void OnBarSlot5()
    {
        if (selectedInvSlot != 5) { selectedInvSlot = 5; }
        else{ selectedInvSlot = 0;}
        InvBarNav();
    }    

    private void OnApplicationQuit()
    {
        inv.items.Clear();
    }
}
