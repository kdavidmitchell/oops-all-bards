package abl.util;

import java.util.*;

public class ArrayHolder {
 private int[] ary;

 public ArrayHolder(int[] ary) {
     this.ary = ary;
 }
 
 public ArrayHolder(int size) {
     this.ary = new int[size];
     Arrays.fill(this.ary, -1);
 }

 public int[] getArray() {
     return ary;
 }
 
 public void setArray(int[] ary) {
     this.ary = ary;
 }
 
 public void addElement(int pos, int val) {
	 this.ary[pos] = val;
 }
}
