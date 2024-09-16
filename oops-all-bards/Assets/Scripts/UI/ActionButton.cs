using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    public BaseAbility ability;
    public BasePlayer actingCharacter;

    public void Execute()
    {
        CombatManager.Instance.UI.GetComponent<CombatUI>().ClearCombatMenu();
        CombatManager.Instance.UI.GetComponent<CombatUI>().RenderTargetButtons(ability);
        Debug.Log("action button created");
    }
}
