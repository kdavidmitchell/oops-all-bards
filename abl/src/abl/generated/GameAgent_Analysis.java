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
public class GameAgent_Analysis {
   static public List<String> analysis0(int __$behaviorID) {
      switch (__$behaviorID) {
         case 1: {
            // lookForAllyAgent-0->ConditionalStep3_IF_MentalStep_GoalStep_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            _$analysisStepIDs.add("manageAllyAgent(AllyWME)"); // stepID 5
            return _$analysisStepIDs;
         }
         case 0: {
            // lookForAllyAgent_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            return _$analysisStepIDs;
         }
         case 2: {
            // allyAgentRoot_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            return _$analysisStepIDs;
         }
         case 3: {
            // manageAllyAgent_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            _$analysisStepIDs.add("lookForQuip(int)"); // stepID 9
            _$analysisStepIDs.add("lookToGossip(int)"); // stepID 10
            _$analysisStepIDs.add("manageCombat(int)"); // stepID 11
            return _$analysisStepIDs;
         }
         case 5: {
            // manageCombat-4->ConditionalStep12_IF_FailStep_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            return _$analysisStepIDs;
         }
         case 4: {
            // manageCombat_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            _$analysisStepIDs.add("lookToAssist(int)"); // stepID 15
            _$analysisStepIDs.add("waitForTurn(int)"); // stepID 16
            _$analysisStepIDs.add("lookForAssistance(int)"); // stepID 17
            return _$analysisStepIDs;
         }
         case 6: {
            // lookForAssistance_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            return _$analysisStepIDs;
         }
         case 7: {
            // waitForTurn_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            return _$analysisStepIDs;
         }
         case 8: {
            // lookToAssist_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            _$analysisStepIDs.add("assistAlly(AllyWME, AllyWME)"); // stepID 25
            return _$analysisStepIDs;
         }
         case 9: {
            // takeTurn_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            return _$analysisStepIDs;
         }
         case 10: {
            // assistAlly_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            return _$analysisStepIDs;
         }
         case 11: {
            // manageNoncombat_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            return _$analysisStepIDs;
         }
         case 13: {
            // lookForQuip-12->ConditionalStep33_IF_FailStep_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            return _$analysisStepIDs;
         }
         case 12: {
            // lookForQuip_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            return _$analysisStepIDs;
         }
         case 15: {
            // lookToGossip-14->ConditionalStep38_IF_FailStep_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            return _$analysisStepIDs;
         }
         case 16: {
            // lookToGossip-14->ConditionalStep40_IF_PrimitiveStep_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            return _$analysisStepIDs;
         }
         case 14: {
            // lookToGossip_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            return _$analysisStepIDs;
         }
         case 17: {
            // GameAgent_RootCollectionBehavior
            List<String> _$analysisStepIDs = new ArrayList<String>();
            _$analysisStepIDs.add("allyAgentRoot()"); // stepID 43
            _$analysisStepIDs.add("lookForAllyAgent()"); // stepID 44
            return _$analysisStepIDs;
         }
         case 18: {
            // __$defaultMemoryExecuteBehavior_1
            List<String> _$analysisStepIDs = new ArrayList<String>();
            return _$analysisStepIDs;
         }
      default:
         throw new AblRuntimeError("Unexpected behaviorID " + __$behaviorID);
      }
   }
}
