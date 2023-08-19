using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBluePrint turretBluePrint;
    [HideInInspector]
    public bool isUpgraded = false;
    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; 
        
        if(turret != null)
        {
            buildManager.SelectedNode(this);
            return; 
        }
        if (!buildManager.CanBuild)
        return;
        BuildTurret(buildManager.GetTurretToBuild());
    }    
    void BuildTurret (TurretBluePrint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not Enough");
            return;
        }
        PlayerStats.Money -= blueprint.cost;
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        turretBluePrint = blueprint;
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(),Quaternion.identity);
        Destroy(effect,5f);
        Debug.Log("turret build");
    }
    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBluePrint.upgradeCost)
        {
            Debug.Log("Not Enough");
            return;
        }
        PlayerStats.Money -= turretBluePrint.upgradeCost;
        Destroy(turret);

        // Build cai moi
        GameObject _turret = (GameObject)Instantiate(turretBluePrint.upgradePrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(),Quaternion.identity);
        Destroy(effect,5f);

        isUpgraded = true;
        Debug.Log("turret Upgraded");
    }
    public void SellTurret()
    {
        PlayerStats.Money += turretBluePrint.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(),Quaternion.identity);
        Destroy(effect,5f);

        Destroy(turret);
        turretBluePrint = null;
    }
    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        return;
        if (!buildManager.CanBuild)
        return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        } else
        {
            rend.material.color = notEnoughMoneyColor;
        }

        
    }
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
