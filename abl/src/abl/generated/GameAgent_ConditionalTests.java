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
public class GameAgent_ConditionalTests {
   static public boolean conditionalTest0(int __$stepID, final Object[] __$behaviorFrame, final BehavingEntity __$thisEntity) {
      switch (__$stepID) {
         case 3: {
            // lookForAllyAgent_1Step4
               if (
                  ! ((GameAgent)__$thisEntity).dict.containsKey(((AllyWME)__$behaviorFrame[0]).getID())
               )

               {
                  return true;
               }


            return false;
         }
         case 12: {
            // manageCombat_1Step1
               if (
                  ((GameAgent)__$thisEntity).dict.getCharacter(((__ValueTypes.IntVar)__$behaviorFrame[0]).i) == null
               )

               {
                  return true;
               }


            return false;
         }
         case 33: {
            // lookForQuip_1Step3
               if (
                  ((AllyWME)__$behaviorFrame[1]) == null
               )

               {
                  return true;
               }


            return false;
         }
         case 38: {
            // lookToGossip_1Step3
               if (
                  ((AllyWME)__$behaviorFrame[1]) == null
               )

               {
                  return true;
               }


            return false;
         }
         case 40: {
            // lookToGossip_1Step5
               if (
                  ((CalDistanceWME)__$behaviorFrame[3]).isNPCAround(((ArrayHolder)__$behaviorFrame[4]) , ((AllyWME)__$behaviorFrame[1]) , ((GameAgent)__$thisEntity).dict)
               )

               {
                  return true;
               }


            return false;
         }
      default:
         throw new AblRuntimeError("Unexpected stepID " + __$stepID);
      }
   }
}
