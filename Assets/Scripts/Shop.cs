using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standardTurret;
    public TurretBluePrint missileLauncher;
    public TurretBluePrint laserBeamer;
    BuildManager buildManager;
    void Start()
    {
        buildManager =  BuildManager.instance;
    }
  public void SelectStandardTurret()
  {
    Debug.Log("StandardTurret");
    buildManager.SelectTurretToBuild(standardTurret);
  }
  public void SelectMissileLauncher()
  {
    Debug.Log("MissileLauncher");
    buildManager.SelectTurretToBuild(missileLauncher);
  }
  public void SelectLaserBeamer()
  {
    Debug.Log("LaserBeamer");
    buildManager.SelectTurretToBuild(laserBeamer);
  }
}
