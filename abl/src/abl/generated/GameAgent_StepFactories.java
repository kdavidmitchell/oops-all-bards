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
public class GameAgent_StepFactories {
   static public Step stepFactory0(int __$stepID, Behavior __$behaviorParent, final Object[] __$behaviorFrame) {
      final Method __$stepFactory = GameAgent.__$stepFactory0_rfield;
      switch (__$stepID) {
         case -3: {
            // default wait step
            return new WaitStepDebug(-3, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, null, null, null, null);
         }
         case -2: {
            // default fail step
            return new FailStepDebug(-2, __$stepFactory, __$behaviorParent, false, false, false, (short)-32768, (short)0, false, null, null);
         }
         case -1: {
            // default succeed step
            return new SucceedStepDebug(-1, __$stepFactory, __$behaviorParent, false, false, (short)-32768, (short)0, false, null, null);
         }
         case 0: {
            // lookForAllyAgent_1Step1
            return new MentalStepDebug(0, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$mentalExecute0_rfield, null, null, null, (byte)2, "lookForAllyAgent_1Step1");
         }
         case 1: {
            // lookForAllyAgent_1Step2
            return new WaitStepDebug(1, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, null, GameAgent.__$successTest0_rfield, null, null);
         }
         case 2: {
            // lookForAllyAgent_1Step3
            return new MentalStepDebug(2, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$mentalExecute0_rfield, null, null, null, (byte)2, "lookForAllyAgent_1Step3");
         }
         case 4: {
            // lookForAllyAgent-0->ConditionalStep3_IF_MentalStep_GoalStep_1Step1
            return new MentalStepDebug(4, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$mentalExecute0_rfield, null, null, null, (byte)2, "lookForAllyAgent-0->ConditionalStep3_IF_MentalStep_GoalStep_1Step1");
         }
         case 5: {
            // lookForAllyAgent-0->ConditionalStep3_IF_MentalStep_GoalStep_1Step2
            return new GoalStepDebug(5, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$argumentExecute0_rfield, null, null, null, "manageAllyAgent(AllyWME)", null, (short)2);
         }
         case 3: {
            // lookForAllyAgent_1Step4
            return new ConditionalStepDebug(3, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, null, null, null, null, GameAgent.__$conditionalTest0_rfield, null, "lookForAllyAgent-0->ConditionalStep3_IF_MentalStep_GoalStep()", null);
         }
         case 6: {
            // lookForAllyAgent_1Step5
            return new MentalStepDebug(6, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$mentalExecute0_rfield, null, null, null, (byte)2, "lookForAllyAgent_1Step5");
         }
         case 7: {
            // allyAgentRoot_1Step1
            return new WaitStepDebug(7, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, null, GameAgent.__$successTest0_rfield, null, null);
         }
         case 8: {
            // manageAllyAgent_1Step1
            return new MentalStepDebug(8, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$mentalExecute0_rfield, null, null, null, (byte)2, "manageAllyAgent_1Step1");
         }
         case 9: {
            // manageAllyAgent_1Step2
            return new GoalStepDebug(9, __$stepFactory, __$behaviorParent, true, false, false, false, false, false, (short)1, (short)0, false, null, GameAgent.__$argumentExecute0_rfield, null, null, null, "lookForQuip(int)", null, (short)0);
         }
         case 10: {
            // manageAllyAgent_1Step3
            return new GoalStepDebug(10, __$stepFactory, __$behaviorParent, true, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$argumentExecute0_rfield, null, null, null, "lookToGossip(int)", null, (short)0);
         }
         case 11: {
            // manageAllyAgent_1Step4
            return new GoalStepDebug(11, __$stepFactory, __$behaviorParent, true, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$argumentExecute0_rfield, null, null, null, "manageCombat(int)", null, (short)0);
         }
         case 12: {
            // manageCombat_1Step1
            return new ConditionalStepDebug(12, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, null, null, null, null, GameAgent.__$conditionalTest0_rfield, null, "manageCombat-4->ConditionalStep12_IF_FailStep()", null);
         }
         case 13: {
            // manageCombat_1Step2
            return new WaitStepDebug(13, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, null, GameAgent.__$successTest0_rfield, null, null);
         }
         case 14: {
            // manageCombat_1Step3
            return new MentalStepDebug(14, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$mentalExecute0_rfield, null, null, null, (byte)2, "manageCombat_1Step3");
         }
         case 15: {
            // manageCombat_1Step4
            return new GoalStepDebug(15, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$argumentExecute0_rfield, null, null, null, "lookToAssist(int)", null, (short)0);
         }
         case 16: {
            // manageCombat_1Step5
            return new GoalStepDebug(16, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$argumentExecute0_rfield, null, null, null, "waitForTurn(int)", null, (short)0);
         }
         case 17: {
            // manageCombat_1Step6
            return new GoalStepDebug(17, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$argumentExecute0_rfield, null, null, null, "lookForAssistance(int)", null, (short)0);
         }
         case 18: {
            // lookForAssistance_1Step1
            return new WaitStepDebug(18, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, null, GameAgent.__$successTest0_rfield, null, null);
         }
         case 19: {
            // lookForAssistance_1Step2
            return new MentalStepDebug(19, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$mentalExecute0_rfield, null, null, null, (byte)2, "lookForAssistance_1Step2");
         }
         case 20: {
            // lookForAssistance_1Step3
            return new PrimitiveStepDebug(20, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$argumentExecute0_rfield, null, null, null, new RequestAssistance(), null, "requestAssistance");
         }
         case 21: {
            // waitForTurn_1Step1
            return new MentalStepDebug(21, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$mentalExecute0_rfield, null, null, null, (byte)2, "waitForTurn_1Step1");
         }
         case 22: {
            // waitForTurn_1Step2
            return new WaitStepDebug(22, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, null, GameAgent.__$successTest0_rfield, null, null);
         }
         case 23: {
            // lookToAssist_1Step1
            return new MentalStepDebug(23, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$mentalExecute0_rfield, null, null, null, (byte)2, "lookToAssist_1Step1");
         }
         case 24: {
            // lookToAssist_1Step2
            return new WaitStepDebug(24, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, null, GameAgent.__$successTest0_rfield, null, null);
         }
         case 25: {
            // lookToAssist_1Step3
            return new GoalStepDebug(25, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$argumentExecute0_rfield, null, null, null, "assistAlly(AllyWME, AllyWME)", null, (short)0);
         }
         case 26: {
            // takeTurn_1Step1
            return new MentalStepDebug(26, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$mentalExecute0_rfield, null, null, null, (byte)2, "takeTurn_1Step1");
         }
         case 27: {
            // assistAlly_1Step1
            return new MentalStepDebug(27, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$mentalExecute0_rfield, null, null, null, (byte)2, "assistAlly_1Step1");
         }
         case 28: {
            // assistAlly_1Step2
            return new PrimitiveStepDebug(28, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$argumentExecute0_rfield, null, null, null, new Protect(), null, "protect");
         }
         case 29: {
            // manageNoncombat_1Step1
            return new WaitStepDebug(29, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, null, GameAgent.__$successTest0_rfield, null, null);
         }
         case 30: {
            // manageNoncombat_1Step2
            return new MentalStepDebug(30, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$mentalExecute0_rfield, null, null, null, (byte)2, "manageNoncombat_1Step2");
         }
         case 31: {
            // lookForQuip_1Step1
            return new WaitStepDebug(31, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, null, GameAgent.__$successTest0_rfield, null, null);
         }
         case 32: {
            // lookForQuip_1Step2
            return new MentalStepDebug(32, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$mentalExecute0_rfield, null, null, null, (byte)2, "lookForQuip_1Step2");
         }
         case 33: {
            // lookForQuip_1Step3
            return new ConditionalStepDebug(33, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, null, null, null, null, GameAgent.__$conditionalTest0_rfield, null, "lookForQuip-12->ConditionalStep33_IF_FailStep()", null);
         }
         case 34: {
            // lookForQuip_1Step4
            return new MentalStepDebug(34, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$mentalExecute0_rfield, null, null, null, (byte)2, "lookForQuip_1Step4");
         }
         case 35: {
            // lookForQuip_1Step5
            return new PrimitiveStepDebug(35, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$argumentExecute0_rfield, null, null, null, new Quip(), null, "quip");
         }
         case 36: {
            // lookToGossip_1Step1
            return new WaitStepDebug(36, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, null, GameAgent.__$successTest0_rfield, null, null);
         }
         case 37: {
            // lookToGossip_1Step2
            return new MentalStepDebug(37, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$mentalExecute0_rfield, null, null, null, (byte)2, "lookToGossip_1Step2");
         }
         case 38: {
            // lookToGossip_1Step3
            return new ConditionalStepDebug(38, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, null, null, null, null, GameAgent.__$conditionalTest0_rfield, null, "lookToGossip-14->ConditionalStep38_IF_FailStep()", null);
         }
         case 39: {
            // lookToGossip_1Step4
            return new MentalStepDebug(39, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$mentalExecute0_rfield, null, null, null, (byte)2, "lookToGossip_1Step4");
         }
         case 41: {
            // lookToGossip-14->ConditionalStep40_IF_PrimitiveStep_1Step1
            return new PrimitiveStepDebug(41, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, GameAgent.__$argumentExecute0_rfield, null, null, null, new Gossip(), null, "gossip");
         }
         case 40: {
            // lookToGossip_1Step5
            return new ConditionalStepDebug(40, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)-32768, (short)0, false, null, null, null, null, null, GameAgent.__$conditionalTest0_rfield, null, "lookToGossip-14->ConditionalStep40_IF_PrimitiveStep()", null);
         }
         case 42: {
            // GameAgent_RootCollectionBehaviorStep1
            return new MentalStepDebug(42, __$stepFactory, __$behaviorParent, false, false, false, false, false, false, (short)3, (short)0, false, null, GameAgent.__$mentalExecute0_rfield, null, null, null, (byte)2, "GameAgent_RootCollectionBehaviorStep1");
         }
         case 43: {
            // GameAgent_RootCollectionBehaviorStep2
            return new GoalStepDebug(43, __$stepFactory, __$behaviorParent, true, false, false, false, false, false, (short)-32768, (short)0, false, null, null, null, null, null, "allyAgentRoot()", null, (short)0);
         }
         case 44: {
            // GameAgent_RootCollectionBehaviorStep3
            return new GoalStepDebug(44, __$stepFactory, __$behaviorParent, true, false, false, false, false, false, (short)-32768, (short)0, false, null, null, null, null, null, "lookForAllyAgent()", null, (short)0);
         }
      default:
         throw new AblRuntimeError("Unexpected stepID " + __$stepID);
      }
   }
}
