using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class TargetButton : MonoBehaviour
{
    public string target;
    public void Execute()
    {
        CombatManager.Instance.target = target;
        CombatManager.Instance.UI.GetComponent<CombatUI>().ClearCombatMenu();
       
    }

    public void OnPointerEnter() // look at a targets model when hovering over their target button
    {
        Debug.Log("look at " + target);
        CombatUI combatUI = CombatManager.Instance.combatUI;
        foreach (BasePlayer p in CombatManager.Instance.party)
        {
            if (p.Name == target)
            {
                combatUI.SetActiveCamera(combatUI.BandCamera);
                combatUI.BandCamera.m_LookAt = p.BattleModel.transform;
            }
        }
        foreach (BaseEnemy e in CombatManager.Instance.enemies)
        {
            if (e.Name == target)
            {
                combatUI.SetActiveCamera(combatUI.EnemyCamera);
                combatUI.EnemyCamera.m_LookAt = e.BattleModel.transform;
            }
        }
        //GameObject targetModel = GameObject.Find($"{target}Model");
        //CombatManager.Instance.combatUI.OverviewCamera.GetComponent<CinemachineVirtualCamera>().m_LookAt = targetModel.transform;
    }

}
