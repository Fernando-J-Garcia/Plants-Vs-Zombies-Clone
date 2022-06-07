using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPosition
{
    public Transform transform;
    public bool IsOccupied;
}
public class TowerManager : MonoBehaviour
{
    
    public GameObject PeaShooter;
    public GameObject Sunflower;
    public GameObject Walnut;
    GameObject bluePrintGameobject;
    GameObject SelectedTower;    
    public Material BlueprintGhostMaterial;
    public Material BlueprintGhostErrorMaterial;
    RaycastHit hit;

    private List<BuildPosition> BuildPositions = new List<BuildPosition>();

    private bool shovelMode = false;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            BuildPosition bp = new BuildPosition();
            bp.transform = transform.GetChild(i);
            bp.IsOccupied = false;
            bp.transform.gameObject.SetActive(true);
            BuildPositions.Add(bp);
            
        }
    }
    public void SelectShovel()
    {
        UI.instance.SetShovelCursor();
        ClearBluePrint();
        shovelMode = true;
    }
    public void SelectPeaShooter()
    {
        SetBluePrintGameObject(PeaShooter);
    }
    public void SelectSunflower()
    {
        SetBluePrintGameObject(Sunflower);
    }
    public void SelectWalnut()
    {
        SetBluePrintGameObject(Walnut);
    }

    /// <summary>
    /// Build Positions are toggled between on and off, just in case in the future
    /// we want to have models repesenting places you can place plants
    /// </summary>
    /// <param name="b"></param>
    void SetBuildPositionsActive(bool b)
    {
        for (int i = 0; i < BuildPositions.Count; i++)
        {
            BuildPositions[i].transform.gameObject.SetActive(b);
        }
    }
    void SetBluePrintGameObject(GameObject g)
    {
        shovelMode = false;//If we are in shovel mode disable it.

        //If the selected tower is still the same then we can just save memory and not do any more calculations
        if (g == SelectedTower) { return; }

        //If the blueprint already has an object then we can clear it.
        if (bluePrintGameobject != null) { ClearBluePrint();}

        bluePrintGameobject = Instantiate(g, transform.position, Quaternion.identity);

        //If we cannot afford the tower return
        if (PlayerStats.Instance.Money < bluePrintGameobject.GetComponent<Tower>().value)
        {
            //Send Message to UI
            UI.instance.CreateNewPopUp("Need more sun!");
            ClearBluePrint();
            return;
        }

        SetBuildPositionsActive(true);

        
        SelectedTower = Instantiate(bluePrintGameobject);
        SetBlueprintMaterial(BlueprintGhostMaterial);
        //SetLayerMask("Blueprint Ghost");//Set render layer to blueprint layer

        bluePrintGameobject.GetComponent<Tower>().isActive = false;
        SelectedTower.GetComponent<Tower>().isActive = false;
        SelectedTower.GetComponent<Tower>().DeathEvent += OnTowerDeath;//subscribe the tower's death event to this script.
        SelectedTower.SetActive(false);
    }
    void SetBlueprintMaterial(Material m)
    {
        //Search for all the renderers in children then assign each material the desired blueprint material.
        Material[] materials;
        foreach (Renderer r in bluePrintGameobject.GetComponentsInChildren<Renderer>())
        {
            materials = r.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = m;
            }
            r.materials = materials;
        }
    }
    void ReleaseBluePrintGameobject(int buildIndex)
    {
        MouseRaycast.SetLayerMask(MouseRaycast.standardLayerMask);
        Tower towerScript = SelectedTower.GetComponent<Tower>();

        PlayerStats.Instance.Money -= towerScript.value;

        towerScript.isActive = true;
        towerScript.buildPos = buildIndex;
        SelectedTower.SetActive(true);
        SelectedTower.transform.SetPositionAndRotation(bluePrintGameobject.transform.position, bluePrintGameobject.transform.rotation);

        //Set the parent of the tower to the build position. 
        SelectedTower.transform.parent = BuildPositions[buildIndex].transform;

        //SetLayerMask("Default");//Set render layer to blueprint layer
        Destroy(bluePrintGameobject);

        SelectedTower = null;
        AudioManager.instance.Play("PlaceTile");
    }
    void ShowBlueprintMode()
    {
        MouseRaycast.SetLayerMask(MouseRaycast.blueprintLayerMask);

        Vector3 wordPos;
        if (MouseRaycast.GotHit)
        {
            hit = MouseRaycast.HitInfo;
            wordPos = hit.point;
            SetBlueprintMaterial(BlueprintGhostErrorMaterial);
            //If We hit a Build Position... Snap to it.
            for (int i = 0; i < BuildPositions.Count; i++)
            {
                if (hit.collider.gameObject == BuildPositions[i].transform.gameObject)
                {
                    wordPos = hit.collider.gameObject.transform.position;
                    SetBlueprintMaterial(BlueprintGhostMaterial);
                    //Let go of the object at the current position
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (BuildPositions[i].IsOccupied == false)
                        {
                            BuildPositions[i].IsOccupied = true;
                            ReleaseBluePrintGameobject(i);
                            return;
                        }
                        else
                        {
                            SetBlueprintMaterial(BlueprintGhostErrorMaterial);
                            //BlueprintGhostMaterial.SetColor("Color",Color.red);
                        }
                    }
                }
            }
            
        }
        else
        {
            wordPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        bluePrintGameobject.transform.position = wordPos;

        if (Input.GetMouseButtonDown(1))
        {
            ClearBluePrint();
            return;
        }

    }

    void ShowShovelMode()
    {
        SetBuildPositionsActive(true);
        MouseRaycast.SetLayerMask(MouseRaycast.blueprintLayerMask);

        if (MouseRaycast.GotHit)
        {
            hit = MouseRaycast.HitInfo;
            //If We hit a Build Position... Snap to it.
            for (int i = 0; i < BuildPositions.Count; i++)
            {
                if (hit.collider.gameObject == BuildPositions[i].transform.gameObject)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (BuildPositions[i].IsOccupied == true)
                        {
                            foreach (Tower tower in BuildPositions[i].transform.GetComponentsInChildren<Tower>())
                            {
                                tower.Kill();
                            }
                            BuildPositions[i].IsOccupied = false;
                            ReleaseShovelMode();
                            return;
                        }
                    }
                }
            }
        }
            if (Input.GetMouseButton(1))
        {
            ReleaseShovelMode();
            return;
        }
    }
    void ReleaseShovelMode()
    {
        MouseRaycast.SetLayerMask(MouseRaycast.standardLayerMask);
        UI.instance.SetDefaultCursor();
        shovelMode = false;
    }

    void ClearBluePrint()
    {
        Debug.Log("Blueprint Cleared");
        if(SelectedTower != null) Destroy(SelectedTower);

        if (bluePrintGameobject != null) Destroy(bluePrintGameobject);
    }


    /// <summary>
    /// When a tower dies it will send an event and let us know its build index
    /// </summary>
    public void OnTowerDeath(int Buildpos)
    {
        Debug.Log("Tower died at build index " + Buildpos);
        BuildPositions[Buildpos].IsOccupied = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(bluePrintGameobject!= null)
        {
            ShowBlueprintMode();
        }
        if(shovelMode == true)
        {
            ShowShovelMode();
        }
    }
}
