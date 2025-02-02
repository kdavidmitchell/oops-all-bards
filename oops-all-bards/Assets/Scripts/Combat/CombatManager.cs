﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;

public class CombatManager : MonoBehaviour
{

    private static CombatManager _instance;
    // References to player and enemy portraits.
    public GameObject partyPortraits;
    public GameObject enemyPortraits;
    // A reference to the portrait UI prefab.
    public GameObject portraitUI;
    // A reference to the combat queue.
    public CombatQueue combatQueue;
    // A reference to the combat action menu.
    public GameObject combatMenu;
    // A reference to the combat action menu button prefab.
    public GameObject actionButton;
    // A reference to the target button prefab.
    public GameObject targetButton;
    // A reference to the player party.
    public BasePlayer[] party;
    // A reference to the enemies.
    public List<BaseEnemy> enemies = new List<BaseEnemy>();
    // A counter for the number of rounds combat has lasted for.
    public int rounds = 1;
    // A reference to a target name, if any.
    public string target = null;
    public static CombatManager Instance => CombatManager._instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        } else if (_instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SubscribeToEvents();
        InitCombatQueue(PartyManager.Instance.currentParty.ToArray(), DemoManager.Instance.GenerateEnemies());
        RenderUI();
        DemoManager.Instance.CheckQueue();
        Debug.Log("I've finished starting up.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // A function that uses the event management system to subscribe to events used in this manager.
    private void SubscribeToEvents()
    {
        EventManager.Instance.SubscribeToEvent(EventType.EnemyAI, TakeEnemyAction);
        EventManager.Instance.SubscribeToEvent(EventType.AllyAI, TakeAllyAction);
    }

    // A function used to render all UI elements for the demo.
    public void RenderUI()
    {
        // Render the portrait section and combat menu.
        partyPortraits.SetActive(true);
        enemyPortraits.SetActive(true);
        combatMenu.SetActive(true);

        // Instantiate portrait UI for party and enemies.
        // Need: name, current health/flourish, max health/flourish, portrait
        foreach (BasePlayer p in party)
        {
            GameObject toInstantiate = Instantiate(portraitUI, partyPortraits.transform);
            // Set name text
            toInstantiate.transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).GetComponent<TMP_Text>().text = p.Name;
            // Set portraits
            toInstantiate.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = PartyManager.Instance.GetPortraitByName(p.Name);
            // Set health and flourish values and update them
            ValueBar healthBar = toInstantiate.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<ValueBar>();
            healthBar.maxValue = p.Health;
            healthBar.UpdateValueBar(p.Health);
            ValueBar flourishBar = toInstantiate.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<ValueBar>();
            flourishBar.maxValue = p.Flourish;
            flourishBar.UpdateValueBar(p.Flourish);
        }

        foreach (BaseEnemy e in enemies)
        {
            GameObject toInstantiate = Instantiate(portraitUI, enemyPortraits.transform);
            // Set name text
            toInstantiate.transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).GetComponent<TMP_Text>().text = e.Name;
            // Set portraits
            toInstantiate.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = PartyManager.Instance.GetPortraitByName("Piggy");
            // Set health and flourish values and update them
            ValueBar healthBar = toInstantiate.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<ValueBar>();
            healthBar.maxValue = e.Health;
            healthBar.UpdateValueBar(e.Health);
            ValueBar flourishBar = toInstantiate.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<ValueBar>();
            flourishBar.maxValue = e.Flourish;
            flourishBar.UpdateValueBar(e.Flourish);
        }
    }

    // A function used to render a particular character's combat menu.
    public void RenderInputMenu(BasePlayer actingCharacter)
    {
        // Render the UI for the input.
        Debug.Log("Rendering input menu for " + actingCharacter.Name + " now.");
        
        // For however many abilities the player has, create action button prefabs and place them as children of combat menu.
        for (int i = 0; i < actingCharacter.PlayerClass.Abilities.Count; i++)
        {
            BaseAbility currentAbility = actingCharacter.PlayerClass.Abilities[i];
            // Create prefab and update text of button.
            GameObject toInstantiate = Instantiate(actionButton, combatMenu.transform);
            toInstantiate.GetComponentInChildren<TMP_Text>().text = currentAbility.Name;
            // Assign the ability and acting character to the action button script.
            toInstantiate.GetComponent<ActionButton>().ability = currentAbility;
            toInstantiate.GetComponent<ActionButton>().actingCharacter = actingCharacter;
            // // Add on click functions to the buttons to create a PlayerAction queueable.
            toInstantiate.GetComponent<Button>().onClick.AddListener(() => 
                {StartCoroutine(SelectTarget(currentAbility, actingCharacter));});
            // If the ability assigned to the button cannot be executed due to lack of FP, disable it entirely.
            if (currentAbility.Cost > actingCharacter.Flourish)
            {
                toInstantiate.GetComponent<Button>().interactable = false;
            }
        }
    }

    // A function used to initialize the combat queue.
    public void InitCombatQueue(BasePlayer[] party, List<BaseEnemy> enemies)
    {
        // Set private references to party and enemies.
        this.party = party;
        this.enemies = enemies;
        // Set PartyManager variables.
        PartyManager.Instance.ToggleInCombat(true);
        // Clean up for new combat scenario.
        combatQueue = new CombatQueue();
        combatQueue.Clear();
        // Add standard functions for start, player input, enemy AI, and end.
        PushAndCreateCombatQueueable(new CombatStart());
        foreach (BasePlayer p in party)
        {
            if (p.ID == 0)
            {
                PushAndCreateCombatQueueable(new PlayerTurn(p));
            } else
            {
                PushAndCreateCombatQueueable(new AllyTurn(p));
            }
        }
        foreach (BaseEnemy e in enemies)
        {
            PushAndCreateCombatQueueable(new EnemyTurn(e));
        }
        // Flag the DemoManager to begin checking queue.
        EventManager.Instance.InvokeEvent(EventType.CheckQueue, null);
    }

    // A function used to push a CombatQueueable to the CombatQueue and create associated gameobject.
    public void PushAndCreateCombatQueueable(ICombatQueueable queueable)
    {
        // Push the queueable to the CombatQueue.
        combatQueue.Push(queueable);
    }

    // A coroutine used to open the target menu after clicking on an ability button. Waits until target
    // is selected, closes target menu, and then creates a PlayerAction.
    public IEnumerator SelectTarget(BaseAbility ability, BasePlayer actingCharacter)
    {
        ITargetable targetable = null;
        target = null;
        
        while (target == null)
        {
            yield return null;
        }

        foreach (BasePlayer p in party)
        {
            if (target == p.Name)
            {
                targetable = p;
            }
        }

        foreach (BaseEnemy e in enemies)
        {
            if (target == e.Name)
            {
                targetable = e;
            }
        }

        AddPlayerAction(ability, actingCharacter, targetable);
    }

    // A function used to destroy all buttons that are children of the combat menu.
    public void ClearCombatMenu()
    {
        for (int i = 0; i < combatMenu.transform.childCount; i++)
        {
            GameObject toDestroy = combatMenu.transform.GetChild(i).gameObject;
            Destroy(toDestroy);
        }
    }

    // A function used to render target buttons.
    public void RenderTargetButtons()
    {
        foreach (BasePlayer p in party)
        {
            GameObject toInstantiate = Instantiate(targetButton, combatMenu.transform);
            toInstantiate.GetComponentInChildren<TMP_Text>().text = p.Name;
            toInstantiate.GetComponent<TargetButton>().target = p.Name;
        }

        foreach (BaseEnemy e in enemies)
        {
            GameObject toInstantiate = Instantiate(targetButton, combatMenu.transform);
            toInstantiate.GetComponentInChildren<TMP_Text>().text = e.Name;
            toInstantiate.GetComponent<TargetButton>().target = e.Name;
        }
    }

    // A function used to create a PlayerAction queueable and push it to the front of the queue.
    public void AddPlayerAction(BaseAbility ability, BasePlayer actingCharacter, ITargetable target)
    {
        Debug.Log("Adding player action...");
        // Create the PlayerAction queueable object for each ability.
        PlayerAction action = new PlayerAction(ability, actingCharacter, target);
        // Push the PlayerAction to the front of the queue.
        combatQueue.PriorityPush(action);
        // Tell DemoManager to check the queue and complete the action.
        EventManager.Instance.InvokeEvent(EventType.CheckQueue, null);
    }

    // A function used to resolve/apply effects of a PlayerAction queueable.
    public void ResolvePlayerAction(PlayerAction action)
    {
        // Handle flourish cost and update UI to reflect new value.
        action.actingCharacter.Flourish -= action.ability.Cost;
        Tuple<ValueBar, ValueBar> relevantValueBars = FindValueBars(action.actingCharacter.Name);
        relevantValueBars.Item2.UpdateValueBar(action.actingCharacter.Flourish);

        if (action.ability.CombatType == BaseAbility.CombatAbilityTypes.ATTACK)
        {
            bool isStrengthened = IsStrengthened(action.actingCharacter);
            int modifiedDamage = isStrengthened ? (action.ability.Damage * 2) : action.ability.Damage;
            action.target.Health -= modifiedDamage;
            Debug.Log(action.actingCharacter.Name + " deals " + modifiedDamage + " damage to " + action.target.Name + ".");
            CheckCombatantsHealth(action.target);
        }
        if (action.ability.CombatType == BaseAbility.CombatAbilityTypes.HEAL)
        {
            bool requiresAssistance = action.target.CiFData.HasStatusType(Status.StatusTypes.REQUIRES_ASSISTANCE);
            action.target.Health += action.ability.Damage;
            Debug.Log(action.actingCharacter.Name + " heals " + action.target.Name + " for " + action.ability.Damage + " health!" );
            if (requiresAssistance) 
            { 
                action.target.CiFData.RemoveStatusByType(Status.StatusTypes.REQUIRES_ASSISTANCE);
                DemoManager.Instance.hasAssistedOnce = true; 
            }; 
        }
        if (action.ability.CombatType == BaseAbility.CombatAbilityTypes.DEFEND)
        {
            bool requiresAssistance = action.target.CiFData.HasStatusType(Status.StatusTypes.REQUIRES_ASSISTANCE);
            action.target.Shield += action.ability.Damage;
            Debug.Log(action.actingCharacter.Name + " is shielding " + action.target.Name + " for " + action.ability.Damage + " damage." );
            if (requiresAssistance) 
            { 
                action.target.CiFData.RemoveStatusByType(Status.StatusTypes.REQUIRES_ASSISTANCE);
                DemoManager.Instance.hasAssistedOnce = true; 
            };
        }
        if (action.ability.CombatType == BaseAbility.CombatAbilityTypes.SUPPORT)
        {
            if (action.ability.ID == 4)
            {
                action.target.CombatStatuses.Add(new CombatStatus(CombatStatus.CombatStatusTypes.STRENGTHENED));
                Debug.Log($"{action.actingCharacter.Name} has strengthened {action.target.Name}!");
            } else if (action.ability.ID == 5)
            {
                action.target.CombatStatuses.Add(new CombatStatus(CombatStatus.CombatStatusTypes.BLINDED));
                Debug.Log($"{action.actingCharacter.Name} has blinded {action.target.Name}!");
            }
        }
        // Handle damage/heal and update UI to reflect new value.
        relevantValueBars = FindValueBars(action.target.Name);
        relevantValueBars.Item1.UpdateValueBar(action.target.Health);

        // Tell DemoManager to check the queue and continue to next turn.
        EventManager.Instance.InvokeEvent(EventType.CheckQueue, null);
    }

    // A function that finds and returns a tuple of ValueBar objects (health, flourish) 
    // corresponding to a string name of a character.
    Tuple<ValueBar, ValueBar> FindValueBars(string name)
    {
        Tuple<ValueBar, ValueBar> relevantBars = new Tuple<ValueBar, ValueBar>(null, null);
        for (int i = 0; i < partyPortraits.transform.childCount; i++)
        {
            Transform currentChild = partyPortraits.transform.GetChild(i);
            Transform desiredChild = currentChild.transform.GetChild(0).transform.GetChild(0).transform.GetChild(3);
            ValueBar healthBar = currentChild.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<ValueBar>();
            ValueBar flourishBar = currentChild.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<ValueBar>();;
            if (desiredChild.GetComponent<TMP_Text>().text == name)
            {
                relevantBars = new Tuple<ValueBar, ValueBar>(healthBar, flourishBar);
            }
        }

        for (int i = 0; i < enemyPortraits.transform.childCount; i++)
        {
            Transform currentChild = enemyPortraits.transform.GetChild(i);
            Transform desiredChild = currentChild.transform.GetChild(0).transform.GetChild(0).transform.GetChild(3);
            ValueBar healthBar = currentChild.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<ValueBar>();
            ValueBar flourishBar = currentChild.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<ValueBar>();;
            if (desiredChild.GetComponent<TMP_Text>().text == name)
            {
                relevantBars = new Tuple<ValueBar, ValueBar>(healthBar, flourishBar);
            }
        }
        return relevantBars;
    }

    // A function used to calculate and push enemy actions to the queue.
    public void TakeEnemyAction()
    {
        Debug.Log("Calculating enemy action...");
        BaseEnemy actingCharacter = (BaseEnemy)EventManager.Instance.EventData;
        
        // TODO: This AI is very simple. Should change to be more interesting.
        // Choose random party member and use Attack ability.
        BasePlayer target = null;
        if ( party.Length > 0) { target = party[UnityEngine.Random.Range(0, party.Length)]; };
        BaseAbility ability = actingCharacter.EnemyClass.Abilities[0];
        if ( target == null ) { CheckForWinLoss(); return; } 
        bool isBlind = IsBlind(actingCharacter);
        if (!isBlind)
        {
            ApplyEffects(actingCharacter, target, ability);
        } else 
        {
            Debug.Log($"{actingCharacter.Name} tried to attack {target.Name}, but missed!");
            actingCharacter.RemoveCombatStatus(CombatStatus.CombatStatusTypes.BLINDED);
        }

        actingCharacter.OwnsTurn = false;
        // Tell DemoManager to check the queue and continue to next turn.
        EventManager.Instance.InvokeEvent(EventType.CheckQueue, null); 
    }

    // A function used to calculate and push ally actions to the queue.
    public void TakeAllyAction()
    {
        Debug.Log("Calculating ally action...");
        BasePlayer actingCharacter = (BasePlayer)EventManager.Instance.EventData;
        BaseEnemy target;
        BaseAbility ability;
        
        // TODO: This AI is very simple. Should change to be more interesting.
        // Choose random enemy and use Attack ability.
        if (enemies.Count > 0) {
            target = enemies[UnityEngine.Random.Range(0, enemies.Count)];
            ability = actingCharacter.PlayerClass.Abilities[0];
        } else {
            CheckForWinLoss();
            return;
        }

        // Apply effects of ability, log the outcome, update value bar of target.
        bool isStrengthened = IsStrengthened(actingCharacter);
        int modifiedDamage = isStrengthened ? (ability.Damage * 2) : ability.Damage;
        target.Health -= modifiedDamage;
        Debug.Log(actingCharacter.Name + " deals " + modifiedDamage + " damage to " + target.Name + "!");
        Tuple<ValueBar, ValueBar> relevantValueBars = FindValueBars(target.Name);
        relevantValueBars.Item1.UpdateValueBar(target.Health);
        CheckCombatantsHealth(target);

        if ( isStrengthened ) { actingCharacter.RemoveCombatStatus(CombatStatus.CombatStatusTypes.STRENGTHENED); };
        actingCharacter.OwnsTurn = false;
        // Tell DemoManager to check the queue and continue to next turn.
        EventManager.Instance.InvokeEvent(EventType.CheckQueue, null); 
    }

    // A function used to determine if any combatants are at 0 health; i.e., downed.
    public void CheckCombatantsHealth(ITargetable target)
    {
        if (target.Health <= 0)
        {
            Debug.Log(target.Name + " was downed in the gig!");
            RemoveCharacterFromCombat(target);
        }
    }

    // A function used to remove a downed character from combat.
    public void RemoveCharacterFromCombat(ITargetable character)
    {
        ICombatQueueable[] array = combatQueue.queue.ToArray();
        Tuple<BasePlayer, BaseEnemy> parsedCharacter = ParseTargetable(character);
        ApplyDeathAnimation(parsedCharacter);
        // Remove instance of character from appropriate list and ensure no turns in the queue belong to the downed character.
        if (parsedCharacter.Item1 != null)
        {
            BasePlayer[] partyCopy = party.Where(x => x != parsedCharacter.Item1).ToArray();
            party = partyCopy;
            // Update UI to no longer show character portrait.
            for (int i = 0; i < partyPortraits.transform.childCount; i++)
            {
                Transform currentChild = partyPortraits.transform.GetChild(i);
                Transform desiredChild = currentChild.transform.GetChild(0).transform.GetChild(0).transform.GetChild(3);
                if (desiredChild.GetComponent<TMP_Text>().text == character.Name)
                {
                    Destroy(currentChild.gameObject);
                }
            }
            if (parsedCharacter.Item1.ID == 0)
            {
                foreach (ICombatQueueable q in array)
                {
                    if (q.GetType() == typeof(PlayerTurn))
                    {
                        combatQueue.Remove(q);
                    }
                }
            } else
            {
                foreach (ICombatQueueable q in array)
                {
                    if (q.GetType() == typeof(AllyTurn))
                    {
                        combatQueue.Remove(q);
                    }
                }
            }
        }
        if (parsedCharacter.Item2 != null)
        {
            Debug.Log("Attempting to remove " + parsedCharacter.Item2.Name);
            enemies.Remove(parsedCharacter.Item2);
            // Update UI to no longer show character portrait.
            for (int i = 0; i < enemyPortraits.transform.childCount; i++)
            {
                Transform currentChild = enemyPortraits.transform.GetChild(i);
                Transform desiredChild = currentChild.transform.GetChild(0).transform.GetChild(0).transform.GetChild(3);
                if (desiredChild.GetComponent<TMP_Text>().text == character.Name)
                {
                    Destroy(currentChild.gameObject);
                }
            }
            // TODO: Fix this!
            foreach (ICombatQueueable q in array)
            {
                if (q.GetType() == typeof(EnemyTurn))
                {
                    EnemyTurn t = (EnemyTurn)q;
                    if (t.actingCharacter == parsedCharacter.Item2)
                    {
                        combatQueue.Remove(q);
                    }
                }
            }
        }
        // Check for win/loss.
        CheckForWinLoss();
    }

    // A function used to determine a win/loss of combat.
    public void CheckForWinLoss() 
    {
        if (party.Length == 0)
        {
            Debug.Log("Player has lost!");
            EventManager.Instance.InvokeEvent(EventType.CombatLoss, null);
        }
        if (enemies.Count == 0)
        {
            Debug.Log("Player has won!");
            EventManager.Instance.InvokeEvent(EventType.CombatWin, null);
        }
    }

    // A helper function used to return a Tuple<BasePlayer, BaseEnemy> from an ITargetable object.
    // TODO: This is pretty hacky. Find a better solution.
    public Tuple<BasePlayer, BaseEnemy> ParseTargetable(ITargetable character)
    {
        Tuple<BasePlayer, BaseEnemy> output = new Tuple<BasePlayer, BaseEnemy>(null, null);
        foreach (BasePlayer p in party)
        {
            if (p.Name == character.Name)
            {
                output = new Tuple<BasePlayer, BaseEnemy>(p, null);
            }
        }
        foreach (BaseEnemy e in enemies)
        {
            if (e.Name == character.Name)
            {
                output = new Tuple<BasePlayer, BaseEnemy>(null, e);
            }
        }
        return output;
    }

    public void ApplyEffects(BaseEnemy actingCharacter, BasePlayer target, BaseAbility ability)
    {
        bool targetIsProtected = IsProtected(target);
        if (targetIsProtected && party.Length > 1) 
        {
            Debug.Log(target.Name + " has a PROTECTED status effect"); 
            target.RemoveCombatStatus(CombatStatus.CombatStatusTypes.PROTECTED);
            ITargetable newTarget = AcquireProtectingTarget();
            Debug.Log(newTarget.Name + " has a PROTECTING status effect");
            newTarget.RemoveCombatStatus(CombatStatus.CombatStatusTypes.PROTECTING);
            Debug.Log(actingCharacter.Name + " tried attacking " + target.Name + ", but " + newTarget.Name + " protected them!");
            target = (BasePlayer)newTarget;
        }

        // Apply effects of ability, log the outcome, update value bar of target.
        if (target.Shield > 0)
        {
            if (target.Shield >= ability.Damage)
            {
                target.Shield -= ability.Damage;
                Debug.Log(actingCharacter.Name + " deals " + ability.Damage + " damage to " + target.Name + "'s shield!");
            } else 
            {
                int overflow = ability.Damage - target.Shield;
                target.Shield = 0;
                target.Health -= overflow;
                Debug.Log(actingCharacter.Name + " destroys " + target.Name + "'s shield, and deals " + overflow + " damage to " + target.Name + "!");
            }
        } else 
        {
            target.Health -= ability.Damage;
            Debug.Log(actingCharacter.Name + " deals " + ability.Damage + " damage to " + target.Name + "!");
        }

        Tuple<ValueBar, ValueBar> relevantValueBars = FindValueBars(target.Name);
        relevantValueBars.Item1.UpdateValueBar(target.Health);
        CheckCombatantsHealth(target);
    }

    private bool IsBlind(ITargetable actingCharacter)
    {
        foreach (CombatStatus cs in actingCharacter.CombatStatuses)
        {
            if (cs.Type == CombatStatus.CombatStatusTypes.BLINDED)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsStrengthened(ITargetable actingCharacter)
    {
        foreach (CombatStatus cs in actingCharacter.CombatStatuses)
        {
            if (cs.Type == CombatStatus.CombatStatusTypes.STRENGTHENED)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsProtected(ITargetable target)
    {
        foreach (CombatStatus cs in target.CombatStatuses)
        {
            if (cs.Type == CombatStatus.CombatStatusTypes.PROTECTED)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsProtecting(ITargetable target)
    {
        foreach (CombatStatus cs in target.CombatStatuses)
        {
            if (cs.Type == CombatStatus.CombatStatusTypes.PROTECTING)
            {
                return true;
            }
        }
        return false;
    }

    private ITargetable AcquireProtectingTarget()
    {
        var result = party.Where(x => x.HasCombatStatusType(CombatStatus.CombatStatusTypes.PROTECTING));
        return result.First();
    }

    private void ApplyDeathAnimation(Tuple<BasePlayer, BaseEnemy> parsedCharacter)
    {
        GameObject model = null;
        if (parsedCharacter.Item1 != null)
        {
            model = GetModelByID(parsedCharacter.Item1.ID);
        }
        if (parsedCharacter.Item2 != null)
        {
            model = GetModelByName(parsedCharacter.Item2.Name);
        }

        Animator animator = model.GetComponent<Animator>();
        animator.SetBool("isDefeated", true);
    }

    public GameObject GetModelByID(int id)
    {
        if (id == 0)
        {
            return GameObject.Find("PlayerModel");
        } else
        {
            return GameObject.Find("AllyModel");
        }
    }

    private GameObject GetModelByName(string name)
    {
        return GameObject.Find($"{name}Model");
    }  
}
