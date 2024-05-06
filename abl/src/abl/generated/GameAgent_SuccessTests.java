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
public class GameAgent_SuccessTests {
   static public boolean successTest0(int __$stepID, final Object[] __$behaviorFrame, final BehavingEntity __$thisEntity) {
      switch (__$stepID) {
         case 1: {
            // lookForAllyAgent_1Step2
               List wmeList0;
               ListIterator wmeIter0;
               wmeList0 = BehavingEntity.getBehavingEntity().lookupWME("AllyWME");
               wmeIter0 = wmeList0.listIterator();
               while(wmeIter0.hasNext()) {
                  AllyWME wme__0 = (AllyWME)wmeIter0.next();
                  __$behaviorFrame[0] = wme__0;
                  if (
                     ( wme__0.getOnTree() == false )
                     &&
                     ( wme__0.getID() != 0 )
                  )

                  {
                        List wmeList1;
                        ListIterator wmeIter1;
                        wmeList1 = BehavingEntity.getBehavingEntity().lookupReflectionWMEBySignature("ParallelBehaviorWME", "allyAgentRoot()");
                        wmeIter1 = wmeList1.listIterator();
                        while(wmeIter1.hasNext()) {
                           ParallelBehaviorWME wme__1 = (ParallelBehaviorWME)wmeIter1.next();
                           __$behaviorFrame[1] = wme__1;
                           if (
                              (wme__1.getSignature().equals("allyAgentRoot()"))
                           )

                           {
                              return true;
                           }

                        }


                  }

               }


            return false;
         }
         case 7: {
            // allyAgentRoot_1Step1
               if (
                  ! ((GameAgent)__$thisEntity).dict.isEmpty()
               )

               {
                  return true;
               }


            return false;
         }
         case 13: {
            // manageCombat_1Step2
               if (
                  ((GameAgent)__$thisEntity).dict.getCharacter(((__ValueTypes.IntVar)__$behaviorFrame[0]).i).getInCombat() == true
               )

               {
                  return true;
               }


            return false;
         }
         case 18: {
            // lookForAssistance_1Step1
               if (
                  ((GameAgent)__$thisEntity).dict.getCharacter(((__ValueTypes.IntVar)__$behaviorFrame[0]).i).getHealth() <= 5
               )

               {
                  return true;
               }


            return false;
         }
         case 22: {
            // waitForTurn_1Step2
               if (
                  ((GameAgent)__$thisEntity).dict.getCharacter(((__ValueTypes.IntVar)__$behaviorFrame[0]).i).getOwnsTurn() == true
               )

               {
                  return true;
               }


            return false;
         }
         case 24: {
            // lookToAssist_1Step2
               List wmeList0;
               ListIterator wmeIter0;
               wmeList0 = BehavingEntity.getBehavingEntity().lookupWME("AllyWME");
               wmeIter0 = wmeList0.listIterator();
               while(wmeIter0.hasNext()) {
                  AllyWME wme__0 = (AllyWME)wmeIter0.next();
                  __$behaviorFrame[1] = wme__0;
                  if (
                     ( wme__0.getOwnsTurn() == true )
                  )

                  {
                        if (
                           ((AllyWME)__$behaviorFrame[2]).getOwnsTurn() == false
                        )

                        {
                           return true;
                        }


                  }

               }


            return false;
         }
         case 29: {
            // manageNoncombat_1Step1
               if (
                  ((AllyWME)__$behaviorFrame[0]).getInCombat() == false
               )

               {
                  return true;
               }


            return false;
         }
         case 31: {
            // lookForQuip_1Step1
               if (
                  System.currentTimeMillis() > ((__ValueTypes.LongVar)__$behaviorFrame[3]).l + 10000
               )

               {
                  return true;
               }


            return false;
         }
         case 36: {
            // lookToGossip_1Step1
               if (
                  System.currentTimeMillis() > ((__ValueTypes.LongVar)__$behaviorFrame[2]).l + 5000
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
