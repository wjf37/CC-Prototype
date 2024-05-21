using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
    [SerializeField] InventorySys inv;
    [SerializeField] GameObject uiBar;
    Image m_PointerImage;
    private Vector3 m_OriginalPointerSize;
    private Ray ray;
    private float rayLength = 2.0f;
    bool displayInteractable = false;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        //If the player is pointing at an object in range to be interacted with the object gets added to a list of targets that can be interacted with.
        //
        ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        OnInteract[] targets = null;
        RaycastHit hit;

        displayInteractable = false;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            var interacts = hit.collider.gameObject.GetComponentsInChildren<OnInteract>();
            //oninteract will need different versions for herbs and the tools

            if (interacts.Length > 0)
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
            }
        }
        
        if (displayInteractable)
        {
            m_PointerImage.sprite = InteractablePointer;
            m_PointerImage.transform.localScale = m_OriginalPointerSize * 2.0f;
        }
        else
        {
            m_PointerImage.sprite = NormalPointer;
            m_PointerImage.color = Color.white;
            m_PointerImage.transform.localScale = m_OriginalPointerSize;
        }
    }

    void OnInteraction()
    {
        /*the logic for detecting whether the raycast has hit an interactable object is already handled with the crosshair changing. Would
        it be a better idea to figure out how to reuse the logic rather than rewriting it. More time will have to be spent on it but perhaps
        it will make it easier in the future to implement something like this well. Whereas I would just continuously be using a less efficient
        method to save time now.*/
    
        OnInteract[] targets = null;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            var interacts = hit.collider.gameObject.GetComponentsInChildren<OnInteract>();
            targets = interacts;
            
            if (targets != null && targets.Length > 0)
            {
                foreach (var target in targets)
                {
                    if(target.isActiveAndEnabled)
                        target.Interact();
                }
            }
        }
    }
}
