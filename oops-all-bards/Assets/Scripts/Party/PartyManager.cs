using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PartyManager : MonoBehaviour
{
    private static PartyManager _instance;
    public static PartyManager Instance => PartyManager._instance;
    public List<BasePlayer> currentParty = new List<BasePlayer>();
    public bool inCombat = false;
    public GameObject partyUI;
    public GameObject partyMemberPrefab;
    public GameObject tsaText;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        } else if (_instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && !DialogueManager.Instance.dialogueUI.activeInHierarchy)
        {
            RenderPartyUI();
            TogglePartyUI();
        }
    }

    public void AddCharacterToParty(BasePlayer character)
    {
        currentParty.Add(character);
    }

    public void RemoveCharacterFromParty(BasePlayer character)
    {
        currentParty.Remove(character);
    }

    public void ToggleInCombat(bool value)
    {
        inCombat = value;
    }

    public BasePlayer FindPartyMemberById(int id)
    {
        foreach (BasePlayer p in currentParty) 
        {
            if (p.ID == id)
            {
                return p;
            }
        }
        return null;
    }

    public BasePlayer FindPartyMemberByName(string name)
    {
        foreach (BasePlayer p in currentParty) 
        {
            if (p.Name == name)
            {
                return p;
            }
        }
        return null;
    }

    public void TogglePartyUI()
    {
        partyUI.SetActive(!partyUI.activeSelf);
        GameManager.Instance.TogglePlayerControls();
    }

    private void RenderPartyUI()
    {
        ClearPartyUI();

        Transform container = partyUI.transform.GetChild(0).Find("PartyMembers");
        
        foreach (BasePlayer p in currentParty)
        {
            GameObject toInstantiate = Instantiate(partyMemberPrefab, container.position, Quaternion.identity);
            toInstantiate.transform.SetParent(container);

            // Set scale to one
            toInstantiate.transform.localScale = Vector3.one;

            Transform partyMember = toInstantiate.transform.Find("PartyMember");
            partyMember.GetChild(0).Find("Portrait").gameObject.GetComponent<Image>().sprite = GetPortraitByName(p.Name);
            partyMember.Find("Name").gameObject.GetComponent<TMP_Text>().text = p.Name;


            Transform traits = toInstantiate.transform.Find("Traits");
            foreach (Trait t in p.CiFData.Traits)
            {
                GameObject text = Instantiate(tsaText, traits.position, Quaternion.identity);
                text.transform.SetParent(traits);
                text.transform.localScale = Vector3.one;
                text.GetComponent<TMP_Text>().text = t.Type.ToString();
            }

            Transform statuses = toInstantiate.transform.Find("Statuses");
            foreach (Status s in p.CiFData.Statuses)
            {
                GameObject text = Instantiate(tsaText, statuses.position, Quaternion.identity);
                text.transform.SetParent(statuses);
                text.transform.localScale = Vector3.one;
                text.GetComponent<TMP_Text>().text = s.Type.ToString();
            }

            Transform affinities = toInstantiate.transform.Find("Affinities");
            foreach (Affinity a in p.CiFData.Affinities)
            {
                GameObject text = Instantiate(tsaText, affinities.position, Quaternion.identity);
                text.transform.SetParent(affinities);
                text.transform.localScale = Vector3.one;
                BasePlayer towards = FindPartyMemberById(a.CharacterID);
                text.GetComponent<TMP_Text>().text = $"{towards.Name}: {a.Value}";
            }
        }
    }

    private void ClearPartyUI()
    {
        Transform container = partyUI.transform.GetChild(0).Find("PartyMembers");
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }

    public Sprite GetPortraitByName(string name)
    {
        Sprite sprite = Resources.Load<Sprite>($"Portraits/{name}");
        return sprite == null ? Resources.Load<Sprite>($"Portraits/Player") : sprite;
    }

    public GameObject GetModelByID(int id)
    {
        if (id == 0)
        {
            return TavernManager.PlayerModel;
        }

        if (id == 1)
        {
            return TavernManager.QuintonModel;
        }
        return null;
    }
}
