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
public class GameAgent_ArgumentStepExecute {
   static public Object[] argumentExecute0(int __$stepID, final Object[] __$behaviorFrame, final BehavingEntity __$thisEntity) {
      switch (__$stepID) {
         case 5: {
            // lookForAllyAgent-0->ConditionalStep3_IF_MentalStep_GoalStep_1Step2
            final Object[] args = new Object[2];
            args[0] = ((ParallelBehaviorWME)__$behaviorFrame[1]);
            args[1] = ((AllyWME)__$behaviorFrame[0]);
            return args;
         }
         case 9: {
            // manageAllyAgent_1Step2
            final Object[] args = new Object[1];
            args[0] = new Integer(((__ValueTypes.IntVar)__$behaviorFrame[1]).i);
            return args;
         }
         case 10: {
            // manageAllyAgent_1Step3
            final Object[] args = new Object[1];
            args[0] = new Integer(((__ValueTypes.IntVar)__$behaviorFrame[1]).i);
            return args;
         }
         case 11: {
            // manageAllyAgent_1Step4
            final Object[] args = new Object[1];
            args[0] = new Integer(((__ValueTypes.IntVar)__$behaviorFrame[1]).i);
            return args;
         }
         case 15: {
            // manageCombat_1Step4
            final Object[] args = new Object[1];
            args[0] = new Integer(((__ValueTypes.IntVar)__$behaviorFrame[0]).i);
            return args;
         }
         case 16: {
            // manageCombat_1Step5
            final Object[] args = new Object[1];
            args[0] = new Integer(((__ValueTypes.IntVar)__$behaviorFrame[0]).i);
            return args;
         }
         case 17: {
            // manageCombat_1Step6
            final Object[] args = new Object[1];
            args[0] = new Integer(((__ValueTypes.IntVar)__$behaviorFrame[0]).i);
            return args;
         }
         case 20: {
            // lookForAssistance_1Step3
            final Object[] args = new Object[1];
            args[0] = new Integer(((__ValueTypes.IntVar)__$behaviorFrame[0]).i);
            return args;
         }
         case 25: {
            // lookToAssist_1Step3
            final Object[] args = new Object[2];
            args[0] = ((AllyWME)__$behaviorFrame[2]);
            args[1] = ((AllyWME)__$behaviorFrame[1]);
            return args;
         }
         case 28: {
            // assistAlly_1Step2
            final Object[] args = new Object[2];
            args[0] = new Integer(((__ValueTypes.IntVar)__$behaviorFrame[2]).i);
            args[1] = new Integer(((__ValueTypes.IntVar)__$behaviorFrame[3]).i);
            return args;
         }
         case 35: {
            // lookForQuip_1Step5
            final Object[] args = new Object[2];
            args[0] = new Integer(((__ValueTypes.IntVar)__$behaviorFrame[0]).i);
            args[1] = new Boolean(((__ValueTypes.BooleanVar)__$behaviorFrame[2]).b);
            return args;
         }
         case 41: {
            // lookToGossip-14->ConditionalStep40_IF_PrimitiveStep_1Step1
            final Object[] args = new Object[2];
            args[0] = new Integer(((__ValueTypes.IntVar)__$behaviorFrame[0]).i);
            args[1] = ((ArrayHolder)__$behaviorFrame[4]);
            return args;
         }
      default:
         throw new AblRuntimeError("Unexpected stepID " + __$stepID);
      }
   }
}
