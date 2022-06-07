using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycast : MonoBehaviour
{
    [SerializeField]
    private LayerMask _bluePrintLayerMask;
    [SerializeField]
    private LayerMask _standardLayerMask;

    public static LayerMask blueprintLayerMask { get; private set; }
    public static LayerMask standardLayerMask { get; private set; }
    public static bool GotHit;
    public static RaycastHit HitInfo;

    private static LayerMask layerMask;

    // Start is called before the first frame update
    void Awake()
    {
        blueprintLayerMask = _bluePrintLayerMask;
        standardLayerMask = _standardLayerMask;

        layerMask = _bluePrintLayerMask;
    }

    // Update is called once per frame
    void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        GotHit = Physics.Raycast(ray, out HitInfo, 100000f,layerMask);
        if (GotHit)
        {
            //Do things
        }
    }

    public static void SetLayerMask(LayerMask _layerMask)
    {
        layerMask = _layerMask;
    }
}
