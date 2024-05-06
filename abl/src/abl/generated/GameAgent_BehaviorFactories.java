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
public class GameAgent_BehaviorFactories {
   static public Behavior behaviorFactory0(int __$behaviorID, Object[] __$args, Map __$boundVars, GoalStep __$parentGoal, String __$signature, BehavingEntity __$thisEntity, __BehaviorDesc __$behaviorDesc) {
      switch (__$behaviorID) {
         case 1: {
            // lookForAllyAgent-0->ConditionalStep3_IF_MentalStep_GoalStep_1
               final Object[] __$behaviorFrame = __$args;

            final __StepDesc[] __$steps = {new __StepDesc(4, GameAgent.__$stepFactory0_rfield), new __StepDesc(5, GameAgent.__$stepFactory0_rfield)};
            return new SequentialBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 1, __$behaviorFrame, __$steps, __$behaviorDesc);
         }
         case 0: {
            // lookForAllyAgent_1
               final Object[] __$behaviorFrame = new Object[2];

            final __StepDesc[] __$steps = {new __StepDesc(0, GameAgent.__$stepFactory0_rfield), new __StepDesc(1, GameAgent.__$stepFactory0_rfield), new __StepDesc(2, GameAgent.__$stepFactory0_rfield), new __StepDesc(3, GameAgent.__$stepFactory0_rfield), new __StepDesc(6, GameAgent.__$stepFactory0_rfield)};
            return new SequentialBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 0, __$behaviorFrame, __$steps, __$behaviorDesc);
         }
         case 2: {
            // allyAgentRoot_1

            final __StepDesc[] __$steps = {new __StepDesc(7, GameAgent.__$stepFactory0_rfield)};
            return new ParallelBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 2, null, __$steps, 1, __$behaviorDesc);
         }
         case 3: {
            // manageAllyAgent_1
               final Object[] __$behaviorFrame = new Object[2];
               __$behaviorFrame[0] = (AllyWME)__$args[0];
               __$behaviorFrame[1] = new __ValueTypes.IntVar();

            final __StepDesc[] __$steps = {new __StepDesc(8, GameAgent.__$stepFactory0_rfield), new __StepDesc(9, GameAgent.__$stepFactory0_rfield), new __StepDesc(10, GameAgent.__$stepFactory0_rfield), new __StepDesc(11, GameAgent.__$stepFactory0_rfield)};
            return new ParallelBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 3, __$behaviorFrame, __$steps, 4, __$behaviorDesc);
         }
         case 5: {
            // manageCombat-4->ConditionalStep12_IF_FailStep_1
               final Object[] __$behaviorFrame = __$args;

            final __StepDesc[] __$steps = {new __StepDesc(-2, GameAgent.__$stepFactory0_rfield)};
            return new SequentialBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 5, __$behaviorFrame, __$steps, __$behaviorDesc);
         }
         case 4: {
            // manageCombat_1
               final Object[] __$behaviorFrame = new Object[1];
               __$behaviorFrame[0] = new __ValueTypes.IntVar((Integer)__$args[0]);

            final __StepDesc[] __$steps = {new __StepDesc(12, GameAgent.__$stepFactory0_rfield), new __StepDesc(13, GameAgent.__$stepFactory0_rfield), new __StepDesc(14, GameAgent.__$stepFactory0_rfield), new __StepDesc(15, GameAgent.__$stepFactory0_rfield), new __StepDesc(16, GameAgent.__$stepFactory0_rfield), new __StepDesc(17, GameAgent.__$stepFactory0_rfield)};
            return new SequentialBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 4, __$behaviorFrame, __$steps, __$behaviorDesc);
         }
         case 6: {
            // lookForAssistance_1
               final Object[] __$behaviorFrame = new Object[1];
               __$behaviorFrame[0] = new __ValueTypes.IntVar((Integer)__$args[0]);

            final __StepDesc[] __$steps = {new __StepDesc(18, GameAgent.__$stepFactory0_rfield), new __StepDesc(19, GameAgent.__$stepFactory0_rfield), new __StepDesc(20, GameAgent.__$stepFactory0_rfield)};
            return new SequentialBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 6, __$behaviorFrame, __$steps, __$behaviorDesc);
         }
         case 7: {
            // waitForTurn_1
               final Object[] __$behaviorFrame = new Object[1];
               __$behaviorFrame[0] = new __ValueTypes.IntVar((Integer)__$args[0]);

            final __StepDesc[] __$steps = {new __StepDesc(21, GameAgent.__$stepFactory0_rfield), new __StepDesc(22, GameAgent.__$stepFactory0_rfield)};
            return new SequentialBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 7, __$behaviorFrame, __$steps, __$behaviorDesc);
         }
         case 8: {
            // lookToAssist_1
               final Object[] __$behaviorFrame = new Object[3];
               __$behaviorFrame[0] = new __ValueTypes.IntVar((Integer)__$args[0]);

            final __StepDesc[] __$steps = {new __StepDesc(23, GameAgent.__$stepFactory0_rfield), new __StepDesc(24, GameAgent.__$stepFactory0_rfield), new __StepDesc(25, GameAgent.__$stepFactory0_rfield)};
            return new SequentialBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 8, __$behaviorFrame, __$steps, __$behaviorDesc);
         }
         case 9: {
            // takeTurn_1
               final Object[] __$behaviorFrame = new Object[1];
               __$behaviorFrame[0] = new __ValueTypes.IntVar((Integer)__$args[0]);

            final __StepDesc[] __$steps = {new __StepDesc(26, GameAgent.__$stepFactory0_rfield)};
            return new SequentialBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 9, __$behaviorFrame, __$steps, __$behaviorDesc);
         }
         case 10: {
            // assistAlly_1
               final Object[] __$behaviorFrame = new Object[4];
               __$behaviorFrame[0] = (AllyWME)__$args[0];
               __$behaviorFrame[1] = (AllyWME)__$args[1];
               __$behaviorFrame[2] = new __ValueTypes.IntVar();
               __$behaviorFrame[3] = new __ValueTypes.IntVar();

            final __StepDesc[] __$steps = {new __StepDesc(27, GameAgent.__$stepFactory0_rfield), new __StepDesc(28, GameAgent.__$stepFactory0_rfield)};
            return new SequentialBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 10, __$behaviorFrame, __$steps, __$behaviorDesc);
         }
         case 11: {
            // manageNoncombat_1
               final Object[] __$behaviorFrame = new Object[1];
               __$behaviorFrame[0] = (AllyWME)__$args[0];

            final __StepDesc[] __$steps = {new __StepDesc(29, GameAgent.__$stepFactory0_rfield), new __StepDesc(30, GameAgent.__$stepFactory0_rfield)};
            return new SequentialBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 11, __$behaviorFrame, __$steps, __$behaviorDesc);
         }
         case 13: {
            // lookForQuip-12->ConditionalStep33_IF_FailStep_1
               final Object[] __$behaviorFrame = __$args;

            final __StepDesc[] __$steps = {new __StepDesc(-2, GameAgent.__$stepFactory0_rfield)};
            return new SequentialBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 13, __$behaviorFrame, __$steps, __$behaviorDesc);
         }
         case 12: {
            // lookForQuip_1
               final Object[] __$behaviorFrame = new Object[4];
               __$behaviorFrame[0] = new __ValueTypes.IntVar((Integer)__$args[0]);
               __$behaviorFrame[2] = new __ValueTypes.BooleanVar();
               __$behaviorFrame[3] = new __ValueTypes.LongVar(System.currentTimeMillis());

            final __StepDesc[] __$steps = {new __StepDesc(31, GameAgent.__$stepFactory0_rfield), new __StepDesc(32, GameAgent.__$stepFactory0_rfield), new __StepDesc(33, GameAgent.__$stepFactory0_rfield), new __StepDesc(34, GameAgent.__$stepFactory0_rfield), new __StepDesc(35, GameAgent.__$stepFactory0_rfield)};
            return new SequentialBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 12, __$behaviorFrame, __$steps, __$behaviorDesc);
         }
         case 15: {
            // lookToGossip-14->ConditionalStep38_IF_FailStep_1
               final Object[] __$behaviorFrame = __$args;

            final __StepDesc[] __$steps = {new __StepDesc(-2, GameAgent.__$stepFactory0_rfield)};
            return new SequentialBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 15, __$behaviorFrame, __$steps, __$behaviorDesc);
         }
         case 16: {
            // lookToGossip-14->ConditionalStep40_IF_PrimitiveStep_1
               final Object[] __$behaviorFrame = __$args;

            final __StepDesc[] __$steps = {new __StepDesc(41, GameAgent.__$stepFactory0_rfield)};
            return new SequentialBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 16, __$behaviorFrame, __$steps, __$behaviorDesc);
         }
         case 14: {
            // lookToGossip_1
               final Object[] __$behaviorFrame = new Object[5];
               __$behaviorFrame[0] = new __ValueTypes.IntVar((Integer)__$args[0]);
               __$behaviorFrame[2] = new __ValueTypes.LongVar(System.currentTimeMillis());
               __$behaviorFrame[3] = new CalDistanceWME();
               __$behaviorFrame[4] = new ArrayHolder(15);

            final __StepDesc[] __$steps = {new __StepDesc(36, GameAgent.__$stepFactory0_rfield), new __StepDesc(37, GameAgent.__$stepFactory0_rfield), new __StepDesc(38, GameAgent.__$stepFactory0_rfield), new __StepDesc(39, GameAgent.__$stepFactory0_rfield), new __StepDesc(40, GameAgent.__$stepFactory0_rfield)};
            return new SequentialBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 14, __$behaviorFrame, __$steps, __$behaviorDesc);
         }
         case 17: {
            // GameAgent_RootCollectionBehavior

            final __StepDesc[] __$steps = {new __StepDesc(42, GameAgent.__$stepFactory0_rfield), new __StepDesc(43, GameAgent.__$stepFactory0_rfield), new __StepDesc(44, GameAgent.__$stepFactory0_rfield)};
            return new CollectionBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 17, null, __$steps, 3, __$behaviorDesc);
         }
         case 18: {
            // __$defaultMemoryExecuteBehavior_1
               final Object[] __$behaviorFrame = __$args;

            final __StepDesc[] __$steps = null;
            return new MemoryExecuteBehaviorDebug(__$parentGoal, null, null, null, null, false, __$signature, (short)0, 18, __$behaviorFrame, __$steps, 0, __$behaviorDesc);
         }
      default:
         throw new AblRuntimeError("Unexpected behaviorID " + __$behaviorID);
      }
   }
}
