package abl.generated;

import abl.runtime.*;
import wm.WME;
import wm.WorkingMemorySet;
import wm.WMEIndex;
import wm.TrackedWorkingMemory;
import java.util.*;
import java.lang.reflect.Method;
import java.lang.reflect.Field;
import abl.learning.*;
import abl.wmes.*;
import abl.actions.*;
import abl.sensors.*;
import abl.util.*;
public class GameAgent_MentalStepExecute {
   static public void mentalExecute0(int __$stepID, final Object[] __$behaviorFrame, final BehavingEntity __$thisEntity, MentalStep __$thisStep) {
      switch (__$stepID) {
         case 0: {
            // lookForAllyAgent_1Step1
            System.out.println("Looking for ally agent...");
            break;
         }
         case 2: {
            // lookForAllyAgent_1Step3
            System.out.println("AllyWME has been added to memory. Setting on tree.");
            ((AllyWME)__$behaviorFrame[0]).setOnTree(true);
            break;
         }
         case 4: {
            // lookForAllyAgent-0->ConditionalStep3_IF_MentalStep_GoalStep_1Step1
            System.out.println("New ally being added to ally manager.");
            break;
         }
         case 6: {
            // lookForAllyAgent_1Step5
            System.out.println("Updating ally wme in dictionary." + ((AllyWME)__$behaviorFrame[0]).getID());
            ((GameAgent)__$thisEntity).dict.addCharacter(((AllyWME)__$behaviorFrame[0]).getID() , ((AllyWME)__$behaviorFrame[0]));
            break;
         }
         case 8: {
            // manageAllyAgent_1Step1
            ((__ValueTypes.IntVar)__$behaviorFrame[1]).i = ((AllyWME)__$behaviorFrame[0]).getID();
            break;
         }
         case 14: {
            // manageCombat_1Step3
            System.out.println("Ally is in combat.");
            break;
         }
         case 19: {
            // lookForAssistance_1Step2
            System.out.println("Ally is in need of assistance.");
            break;
         }
         case 21: {
            // waitForTurn_1Step1
            System.out.println("I am waiting to take my turn.");
            break;
         }
         case 23: {
            // lookToAssist_1Step1
            System.out.println("I am looking to assist.");
            __$behaviorFrame[2] = ((GameAgent)__$thisEntity).dict.getCharacter(((__ValueTypes.IntVar)__$behaviorFrame[0]).i);
            break;
         }
         case 26: {
            // takeTurn_1Step1
            System.out.println("I am taking my turn.");
            break;
         }
         case 27: {
            // assistAlly_1Step1
            ((__ValueTypes.IntVar)__$behaviorFrame[2]).i = ((AllyWME)__$behaviorFrame[0]).getID();
            ((__ValueTypes.IntVar)__$behaviorFrame[3]).i = ((AllyWME)__$behaviorFrame[1]).getID();
            System.out.println("I am assisting.");
            break;
         }
         case 30: {
            // manageNoncombat_1Step2
            System.out.println("Ally is not in combat.");
            break;
         }
         case 32: {
            // lookForQuip_1Step2
            __$behaviorFrame[1] = ((GameAgent)__$thisEntity).dict.getCharacter(((__ValueTypes.IntVar)__$behaviorFrame[0]).i);
            break;
         }
         case 34: {
            // lookForQuip_1Step4
            ((__ValueTypes.BooleanVar)__$behaviorFrame[2]).b = ((AllyWME)__$behaviorFrame[1]).getInCombat();
            System.out.println("Starting quip.");
            break;
         }
         case 37: {
            // lookToGossip_1Step2
            __$behaviorFrame[1] = ((GameAgent)__$thisEntity).dict.getCharacter(((__ValueTypes.IntVar)__$behaviorFrame[0]).i);
            break;
         }
         case 39: {
            // lookToGossip_1Step4
            System.out.println("Look to gossip.");
            break;
         }
         case 42: {
            // GameAgent_RootCollectionBehaviorStep1
            System.out.println("Starting game agent...");
            break;
         }
      default:
         throw new AblRuntimeError("Unexpected stepID " + __$stepID);
      }
   }
}
