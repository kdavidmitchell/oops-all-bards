using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTracker
{
    private BasePlayer player;
    private string name;
    public PositionTracker(string Name)
    {
        name = Name;
        player = PartyManager.Instance.FindPartyMemberByName(name);
    }
 
    public void updatePosition(Vector3 pos)
    {
        if(player == null){
            //Todo: Now only tracks NPC that has been recruited, but the system are supposed to track every NPC.
            //Need to create an NPC manager that tracks all NPCs which use WanderNPCAI.cs
            player = PartyManager.Instance.FindPartyMemberByName(name);
        }
        if(player != null){
            player.LocationX = pos.x;
            player.LocationY = pos.y;
            player.LocationZ = pos.z;
        }
    }

}
