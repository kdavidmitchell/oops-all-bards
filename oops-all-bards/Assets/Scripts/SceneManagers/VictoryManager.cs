using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryManager : MonoBehaviour
{

    private Reward rewardToRender;

    public GameObject victoryScreenTextPrefab;
    public GameObject contentOrganizer;

    // Start is called before the first frame update
    void Start()
    {
        rewardToRender = GenerateReward();
        RenderReward(rewardToRender);
        ApplyRewardToPlayer(rewardToRender);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Reward GenerateReward()
    {
        Reward reward = new Reward(10, 30, 25);
        return reward;
    }

    private void RenderReward(Reward toRender)
    {
        GameObject affinityText = Instantiate(victoryScreenTextPrefab, contentOrganizer.transform.position, Quaternion.identity, contentOrganizer.transform);
        affinityText.GetComponent<TMP_Text>().text = $"Your affinity with Quinton has increased by {toRender.AffinityIncrease}.";
        GameObject fameText = Instantiate(victoryScreenTextPrefab, contentOrganizer.transform.position, Quaternion.identity, contentOrganizer.transform);
        fameText.GetComponent<TMP_Text>().text = $"You have gained {toRender.Fame} fame.";
        GameObject goldText = Instantiate(victoryScreenTextPrefab, contentOrganizer.transform.position, Quaternion.identity, contentOrganizer.transform);
        goldText.GetComponent<TMP_Text>().text = $"You have gained {toRender.Gold} gold.";
    }

    private void ApplyRewardToPlayer(Reward reward)
    {
        BasePlayer player = PartyManager.Instance.FindPartyMemberById(0);
        player.Fame += reward.Fame;
        player.Gold += reward.Gold;
        player.CiFData = new CiFData();
        Affinity quinton = new Affinity(1, reward.AffinityIncrease);
        player.CiFData.AddAffinity(quinton);
    }

    public void RedirectToTavern()
    {
        DemoManager.Instance.LoadScene("TavernDemo");
    }

    public void IncrementTavernVisits()
    {
        GameManager.Instance.IncrementTavernVisits();
    }

    public void ToggleCombat()
    {
        PartyManager.Instance.ToggleInCombat(false);
    }
}

public class Reward
{
    private int affinityIncrease;
    private int fame;
    private int gold;

    public int AffinityIncrease
    {
        get { return this.affinityIncrease; }
        set { this.affinityIncrease = value; }
    }

    public int Fame
    {
        get { return this.fame; }
        set { this.fame = value; }
    }

    public int Gold
    {
        get { return this.gold; }
        set { this.gold = value; }
    }

    public Reward(int affinityIncrease, int fame, int gold)
    {
        this.affinityIncrease = affinityIncrease;
        this.fame = fame;
        this.gold = gold;
    }
}
