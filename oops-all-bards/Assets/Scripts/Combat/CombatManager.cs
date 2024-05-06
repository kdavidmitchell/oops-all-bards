using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;

public class CombatManager : MonoBehaviour
{

    private static CombatManager _instance;

    //public reference to ui gameobject
    public CombatUI combatUI;
    public GameObject UI;  
    // A reference to the combat queue.
    public CombatQueue combatQueue;
    // A reference to the player party.
    public List<BasePlayer> party = new List<BasePlayer>();
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
        InitCombatQueue(PartyManager.Instance.currentParty, EnemyFactory.Instance.GenerateRandomEnemies(2));
        GameManager.Instance.CheckQueue();
        combatUI = CombatUI.Instance;
        combatUI.SetActiveCamera(combatUI.OverviewCamera);
        CombatUI.Instance.UpdateCombatLog("I've finished starting up.");
        Debug.Log("I've finished starting up.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // A function that uses the event management system to subscribe to events used in this manager.
    private void SubscribeToEvents()
    {
        EventManager.Instance.SubscribeToEvent(EventType.AllyAI, DoAllyAction);

        EventManager.Instance.SubscribeToEvent(EventType.EnemyAI, DoEnemyAction);

    }


    // A function used to initialize the combat queue.
    public void InitCombatQueue(List<BasePlayer> party, List<BaseEnemy> enemies)
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
            p.BattleModel = GetModelByID(p.ID);
            p.Anim = p.BattleModel.GetComponent<Animator>();
            p.Animan = p.BattleModel.GetComponent<CombatAnimationManager>();
        }
        foreach (BaseEnemy e in enemies)
        {
            e.BattleModel = GetModelByName(e.Name);
            e.Anim = e.BattleModel.GetComponent<Animator>();
            e.Animan = e.BattleModel.GetComponent<CombatAnimationManager>();
            PushAndCreateCombatQueueable(new EnemyTurn(e));
        }
        // Set up combat ui when combat queue is initialized for the first time
        if (rounds == 1)
        {
            CombatUI.Instance.RenderUI();
        }
        CombatUI.Instance.RenderQueue();
        CombatUI.Instance.ResetCombatLog();
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

        if (target == "back")
        {
            Debug.Log("Go back one screen");
            //reset
            combatUI.SetActiveCamera(combatUI.OverviewCamera);
            combatUI.RenderInputMenu(actingCharacter);
        }
        else
        {
            combatUI.SetActiveCamera(combatUI.OverviewCamera);
            AddPlayerAction(ability, actingCharacter, targetable);
        }
    }

    // A function used to create a PlayerAction queueable and push it to the front of the queue.
    public void AddPlayerAction(BaseAbility ability, BasePlayer actingCharacter, ITargetable target)
    {
        if (ability.ID == 99)
        {
            Debug.Log("Adding player action...");
            combatQueue.PriorityPush(new InfluenceAllyTurn(actingCharacter, (BasePlayer)target));
            EventManager.Instance.InvokeEvent(EventType.CheckQueue, null);
        }

        else
        {
            Debug.Log("Adding player action...");
            // Create the PlayerAction queueable object for each ability.
            PlayerAction action = new PlayerAction(ability, actingCharacter, target);
            // Push the PlayerAction to the front of the queue.
            combatQueue.PriorityPush(action);
            // Tell DemoManager to check the queue and complete the action.
            EventManager.Instance.InvokeEvent(EventType.CheckQueue, null);
        }
    }

    // A function used to resolve/apply effects of a PlayerAction queueable.
    public void DoPlayerAction(PlayerAction action)
    {
        StartCoroutine(ResolvePlayerAction(action));
    }
    public IEnumerator ResolvePlayerAction(PlayerAction action)
    {
        // Adjust camera, pause for animations, update game state 
        combatUI.SetActiveCamera(combatUI.BandCamera);
        if (action.target is BasePlayer)
        {
            combatUI.SetActiveCamera(combatUI.BandCamera);
            combatUI.BandCamera.m_LookAt = ((BasePlayer)action.target).BattleModel.transform;
        }
        else if (action.target is BaseEnemy)
        {
            combatUI.SetActiveCamera(combatUI.EnemyCamera);
            combatUI.EnemyCamera.m_LookAt = ((BaseEnemy)action.target).BattleModel.transform;
        }

        // play animations if action has one
        if (action.ability.AbilityAnimationClip != null)
        {
            action.actingCharacter.Animan.ReceiveAnimationRequirements(action.ability.AbilityAnimationParticles, action.ability.AbilitySFX);
            action.actingCharacter.Anim.Play(action.ability.AbilityAnimationClip);
            yield return new WaitForSeconds(action.actingCharacter.Anim.GetCurrentAnimatorClipInfo(0).Length);
        }
        else yield return new WaitForSeconds(3f);

        // If the attack misses, skip this turn
        if(!AttackHits(action.target))
        {
            Debug.Log(action.target.Name + " dodges!");
            CombatUI.Instance.UpdateCombatLog(action.target.Name + " dodges!");
            // Tell GameManager to check the queue and continue to next turn.
            EventManager.Instance.InvokeEvent(EventType.CheckQueue, null);
            yield return new WaitForSeconds(2);
        }

        // Handle flourish cost and update UI to reflect new value.
        action.actingCharacter.Flourish -= action.ability.Cost;
        CombatUI.FindPortrait(action.actingCharacter.Name).flourishBar.UpdateValueBar(action.actingCharacter.Flourish);

        if (action.ability.CombatType == BaseAbility.CombatAbilityTypes.ATTACK)
        {
            bool isStrengthened = IsStrengthened(action.actingCharacter);
            int modifiedDamage = isStrengthened ? (action.ability.Damage * 2) : action.ability.Damage;
            action.target.Health -= modifiedDamage;
            Debug.Log(action.actingCharacter.Name + " deals " + modifiedDamage + " damage to " + action.target.Name + "."); 
            CombatUI.Instance.UpdateCombatLog(action.actingCharacter.Name + " deals " + modifiedDamage + " damage to " + action.target.Name + ".");
            CheckCombatantsHealth(action.target);
        }
        if (action.ability.CombatType == BaseAbility.CombatAbilityTypes.HEAL)
        {
            bool requiresAssistance = action.target.CiFData.HasStatusType(Status.StatusTypes.REQUIRES_ASSISTANCE);
            action.target.Health += action.ability.Damage;
            Debug.Log(action.actingCharacter.Name + " heals " + action.target.Name + " for " + action.ability.Damage + " health!");
            CombatUI.Instance.UpdateCombatLog(action.actingCharacter.Name + " heals " + action.target.Name + " for " + action.ability.Damage + " health!");
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
            Debug.Log(action.actingCharacter.Name + " is shielding " + action.target.Name + " for " + action.ability.Damage + " damage.");
            CombatUI.Instance.UpdateCombatLog(action.actingCharacter.Name + " is shielding " + action.target.Name + " for " + action.ability.Damage + " damage.");
            if (requiresAssistance)
            {
                action.target.CiFData.RemoveStatusByType(Status.StatusTypes.REQUIRES_ASSISTANCE);
                DemoManager.Instance.hasAssistedOnce = true;
            };
        }
        if (action.ability.CombatType == BaseAbility.CombatAbilityTypes.SUPPORT)
        {
            switch (action.ability.ID)
            {
                case 4:
                    action.target.CombatStatuses.Add(new CombatStatus(CombatStatus.CombatStatusTypes.STRENGTHENED));
                    Debug.Log($"{action.actingCharacter.Name} has strengthened {action.target.Name}!");
                    CombatUI.Instance.UpdateCombatLog($"{action.actingCharacter.Name} has strengthened {action.target.Name}!");
                    break;
                case 5:
                    action.target.CombatStatuses.Add(new CombatStatus(CombatStatus.CombatStatusTypes.BLINDED));
                    Debug.Log($"{action.actingCharacter.Name} has blinded {action.target.Name}!");
                    CombatUI.Instance.UpdateCombatLog($"{action.actingCharacter.Name} has blinded {action.target.Name}!");
                    break;
            }
        }
        // Handle damage/heal and update UI to reflect new value.
        
        PortraitData targetPortrait = CombatUI.FindPortrait(action.target.Name);
        targetPortrait.anim.SetTrigger("takeDamage");
        targetPortrait.healthBar.UpdateValueBar(action.target.Health);

        //Update UI to reflect current Virtuoso value
        if (combatUI.V < CombatUI.Instance.virtData.healthBar.maxValue + 1)
        {
            CombatUI.Instance.virtData.healthBar.UpdateValueBar(combatUI.V);
        }

        if (action.ability.AbilityAnimationClip != null && action.target.Health > 0)
        {
            action.target.Anim.Play(action.ability.AbilityReactionAnimationClip);
            yield return new WaitForSeconds(action.target.Anim.GetCurrentAnimatorClipInfo(0).Length);
        }
        else yield return new WaitForSeconds(2);
        //Debug.Log("pauuseeee");

        // Tell DemoManager to check the queue and continue to next turn.
        EventManager.Instance.InvokeEvent(EventType.CheckQueue, null);
    }

    public void DoEnemyAction()
    {
        StartCoroutine(TakeEnemyAction());
    }
    // A function used to calculate and push enemy actions to the queue.
    public IEnumerator TakeEnemyAction()
    {
        //Handle camera switching
        //combatUI.BandCamera.SetActive(false);
        //combatUI.EnemyCamera.SetActive(true);

        Debug.Log("Calculating enemy action...");
        BaseEnemy actingCharacter = (BaseEnemy)EventManager.Instance.EventData;
        
        // TODO: This AI is very simple. Should change to be more interesting.
        // Choose random party member and use Attack ability.
        BasePlayer target = null;
        if ( party.Count > 0) { target = party[UnityEngine.Random.Range(0, party.Count)]; };
        BaseAbility ability = actingCharacter.EnemyClass.Abilities[0];
        if ( target == null )
        {
            CheckForWinLoss();
            //return?
        }

        // Adjust camera, pause for animations, update game state 
        // TODO: Change from WaitForSeconds length to acting characters action animation length
        combatUI.SetActiveCamera(combatUI.BandCamera);
        combatUI.BandCamera.m_LookAt = target.BattleModel.transform;

        // play animations if action has one
        if (ability.AbilityAnimationClip != null)
        {
            actingCharacter.Animan.ReceiveAnimationRequirements(ability.AbilityAnimationParticles, ability.AbilitySFX);
            actingCharacter.Anim.Play(ability.AbilityAnimationClip);
            yield return new WaitForSeconds(actingCharacter.Anim.GetCurrentAnimatorClipInfo(0).Length);
        }
        else yield return new WaitForSeconds(3f);

        bool isBlind = IsBlind(actingCharacter);
        bool attackHits = AttackHits(target);
        if (!isBlind && attackHits)
        {
            ApplyEffects(actingCharacter, target, ability);
        } else 
        {
            Debug.Log($"{actingCharacter.Name} tried to attack {target.Name}, but missed!");
            CombatUI.Instance.UpdateCombatLog($"{actingCharacter.Name} tried to attack {target.Name}, but missed!");
            actingCharacter.RemoveCombatStatus(CombatStatus.CombatStatusTypes.BLINDED);
        }

        if (ability.AbilityAnimationClip != null && target.Health > 0)
        {
            target.Anim.Play(ability.AbilityReactionAnimationClip);
            yield return new WaitForSeconds(target.Anim.GetCurrentAnimatorClipInfo(0).Length);
        }
        else yield return new WaitForSeconds(2);

        actingCharacter.OwnsTurn = false;

        // Tell DemoManager to check the queue and continue to next turn.
        EventManager.Instance.InvokeEvent(EventType.CheckQueue, null);

        //Handle camera switching
        //combatUI.OverviewCamera.SetActive(false);
        //combatUI.EnemyCamera.SetActive(false);
        //combatUI.BandCamera.SetActive(true);
    }

    // A function used to calculate and push ally actions to the queue.
    public void DoAllyAction()
    {
        StartCoroutine(TakeAllyAction());
    }
    public IEnumerator TakeAllyAction()
    {
        //Handle camera switching
        //combatUI.EnemyCamera.SetActive(false);
        //combatUI.BandCamera.SetActive(true);

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
            yield break;
        }

        // Adjust camera, pause for animations, update game state 
        // TODO: Change from WaitForSeconds length to acting characters action animation length
        combatUI.SetActiveCamera(combatUI.EnemyCamera);
        combatUI.EnemyCamera.m_LookAt = target.BattleModel.transform;

        // play animations if action has one
        if (ability.AbilityAnimationClip != null)
        {
            actingCharacter.Animan.ReceiveAnimationRequirements(ability.AbilityAnimationParticles, ability.AbilitySFX);
            actingCharacter.Anim.Play(ability.AbilityAnimationClip);
            yield return new WaitForSeconds(actingCharacter.Anim.GetCurrentAnimatorClipInfo(0).Length);
        }
        else yield return new WaitForSeconds(3f);

        // Apply effects of ability, log the outcome, update value bar of target.
        bool isStrengthened = IsStrengthened(actingCharacter);
        int modifiedDamage = isStrengthened ? (ability.Damage * 2) : ability.Damage;
        target.Health -= modifiedDamage;
        Debug.Log(actingCharacter.Name + " deals " + modifiedDamage + " damage to " + target.Name + "!");
        CombatUI.Instance.UpdateCombatLog(actingCharacter.Name + " deals " + modifiedDamage + " damage to " + target.Name + "!");
        PortraitData targetPortrait = CombatUI.FindPortrait(target.Name);
        targetPortrait.anim.SetTrigger("takeDamage");
        targetPortrait.healthBar.UpdateValueBar(target.Health);
        CheckCombatantsHealth(target);

        if ( isStrengthened ) { actingCharacter.RemoveCombatStatus(CombatStatus.CombatStatusTypes.STRENGTHENED); };

        if (ability.AbilityAnimationClip != null && target.Health > 0)
        {
            target.Anim.Play(ability.AbilityReactionAnimationClip);
            yield return new WaitForSeconds(target.Anim.GetCurrentAnimatorClipInfo(0).Length);
        }
        else yield return new WaitForSeconds(2);
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
            CombatUI.Instance.UpdateCombatLog(target.Name + " was downed in the gig!");
            RemoveCharacterFromCombat(target);
        }
    }

    // A function used to remove a downed character from combat.
    public void RemoveCharacterFromCombat(ITargetable character)
    {
        ICombatQueueable[] array = combatQueue.queue.ToArray();
        ITargetable parsedCharacter = ParseTargetable(character);
        ApplyDeathAnimation(parsedCharacter);
        // Remove instance of character from appropriate list and ensure no turns in the queue belong to the downed character.
        if (parsedCharacter is BasePlayer)
        {
            List<BasePlayer> partyCopy = new List<BasePlayer>();
            partyCopy.AddRange(party.Where(x => x != parsedCharacter));
            party = partyCopy;
            // Update UI to no longer show character portrait.
            for (int i = 0; i < CombatUI.Instance.partyPortraits.transform.childCount; i++)
            {
                PortraitData currentChild = CombatUI.Instance.partyPortraits.transform.GetChild(i).GetComponent<PortraitData>();

                if (currentChild.nameText.text == character.Name)
                {
                    Destroy(currentChild.gameObject);
                }
            }
            if (((BasePlayer)parsedCharacter).ID == 0)
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
        if (parsedCharacter is BaseEnemy)
        {
            Debug.Log("Attempting to remove " + parsedCharacter.Name);
            enemies.Remove((BaseEnemy)parsedCharacter);
            // Update UI to no longer show character portrait.
            for (int i = 0; i < CombatUI.Instance.enemyPortraits.transform.childCount; i++)
            {
                PortraitData currentChild = CombatUI.Instance.enemyPortraits.transform.GetChild(i).GetComponent<PortraitData>();

                if (currentChild.nameText.text == character.Name)
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
                    if (t.actingCharacter == parsedCharacter)
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
        if (party.Count == 0)
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
    public ITargetable ParseTargetable(ITargetable character)
    {
        ITargetable output = null;
        foreach (BasePlayer p in party)
        {
            if (p.Name == character.Name)
            {
                output = p;
            }
        }
        foreach (BaseEnemy e in enemies)
        {
            if (e.Name == character.Name)
            {
                output = e;
            }
        }
        return output;
    }

    public void ApplyEffects(BaseEnemy actingCharacter, BasePlayer target, BaseAbility ability)
    {

        bool targetIsProtected = IsProtected(target);
        if (targetIsProtected && party.Count > 1) 
        {
            Debug.Log(target.Name + " has a PROTECTED status effect");
            CombatUI.Instance.UpdateCombatLog(target.Name + " has a PROTECTED status effect"); 
            target.RemoveCombatStatus(CombatStatus.CombatStatusTypes.PROTECTED);
            ITargetable newTarget = AcquireProtectingTarget();
            Debug.Log(newTarget.Name + " has a PROTECTING status effect");
            CombatUI.Instance.UpdateCombatLog(newTarget.Name + " has a PROTECTING status effect");
            newTarget.RemoveCombatStatus(CombatStatus.CombatStatusTypes.PROTECTING);
            Debug.Log(actingCharacter.Name + " tried attacking " + target.Name + ", but " + newTarget.Name + " protected them!");
            CombatUI.Instance.UpdateCombatLog(actingCharacter.Name + " tried attacking " + target.Name + ", but " + newTarget.Name + " protected them!");
            target = (BasePlayer)newTarget;
        }

        // Apply effects of ability, log the outcome, update value bar of target.
        if (target.Shield > 0)
        {
            if (target.Shield >= ability.Damage)
            {
                target.Shield -= ability.Damage;
                Debug.Log(actingCharacter.Name + " deals " + ability.Damage + " damage to " + target.Name + "'s shield!");
                CombatUI.Instance.UpdateCombatLog(actingCharacter.Name + " deals " + ability.Damage + " damage to " + target.Name + "'s shield!");
            } else 
            {
                int overflow = ability.Damage - target.Shield;
                target.Shield = 0;
                target.Health -= overflow;
                Debug.Log(actingCharacter.Name + " destroys " + target.Name + "'s shield, and deals " + overflow + " damage to " + target.Name + "!");
                CombatUI.Instance.UpdateCombatLog(actingCharacter.Name + " destroys " + target.Name + "'s shield, and deals " + overflow + " damage to " + target.Name + "!");
            }
        } else 
        {
            target.Health -= ability.Damage;
            Debug.Log(actingCharacter.Name + " deals " + ability.Damage + " damage to " + target.Name + "!");
            CombatUI.Instance.UpdateCombatLog(actingCharacter.Name + " deals " + ability.Damage + " damage to " + target.Name + "!");
        }

        PortraitData targetPortrait = CombatUI.FindPortrait(target.Name);
        targetPortrait.anim.SetTrigger("takeDamage");
        targetPortrait.healthBar.UpdateValueBar(target.Health);
        CheckCombatantsHealth(target);
    }

    bool AttackHits(ITargetable target)
    {
        int threshold = 100 - target.Elan;
        Debug.Log("Threshold: " + threshold);
        int randomInt = UnityEngine.Random.Range(1, 101);
        Debug.Log("Random Int: " + randomInt);
        return (randomInt < threshold);
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

    private void ApplyDeathAnimation(ITargetable parsedCharacter)
    {
        GameObject model = null;
        if (parsedCharacter is BasePlayer)
        {
            model = ((BasePlayer)parsedCharacter).BattleModel;
        }
        if (parsedCharacter is BaseEnemy)
        {
            model = ((BaseEnemy)parsedCharacter).BattleModel;
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
