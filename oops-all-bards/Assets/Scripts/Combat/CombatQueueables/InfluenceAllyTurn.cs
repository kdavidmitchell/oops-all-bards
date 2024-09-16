using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  InfluenceAllyTurn is used when an acting character (presumably the player)
 *  wants to influence a character's turn. If the acting character succeeds in the test,
 *  the influencedCharacter will follow the command.
 */ 
public class InfluenceAllyTurn : MonoBehaviour, ICombatQueueable
{
    public bool done { get; set; }
    
    public BasePlayer actingCharacter { get; set; }
    public BasePlayer influencedCharacter { get; set; }


    public void Execute()
    {
        Debug.Log(actingCharacter.Name + "is attempting to influence " + influencedCharacter.Name + "'s turn.");

        TCPTestClient.Instance.RefreshWMEs();

        if(ShouldInfluence())
        {
            // If check succeeds,
            actingCharacter.OwnsTurn = true;
            if(actingCharacter.OwnsTurn)
            {
                //Virtuoso test: should increase virtuoso value when influence succeeds
                CombatUI.Instance.V += 1;

            }

            // Possibly might need to add a new event type here 
            EventManager.Instance.InvokeEvent(EventType.AwaitPlayerInput, influencedCharacter);
            CombatManager.Instance.UI.GetComponent<CombatUI>().RenderInputMenu(influencedCharacter);

            
        }
        else
        {
            // If check fails, the ally will simply follow their own will
            influencedCharacter.OwnsTurn = true;
            EventManager.Instance.InvokeEvent(EventType.AllyAI, influencedCharacter);
        }

        done = true;
    }

    public bool ShouldInfluence()
    {
        // This is just an example of a possible test that could be performed
        return true;
    }

    public InfluenceAllyTurn(BasePlayer actingCharacter, BasePlayer influencedCharacter)
    {
        this.actingCharacter = actingCharacter;
        this.influencedCharacter = influencedCharacter;
    }
}
