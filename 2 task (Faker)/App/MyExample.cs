using System;
using System.Collections.Generic;

namespace App
{
    public struct MyStruct
    {
        public long Long;
        public string AnotherString;

        public override string ToString()
        {
            return GetType().Name + "(" + Long + ", " + AnotherString + ")";
        }
    }

    public class MyExample
    {
        public string aString { set; get; }

        public int Int { set; get; }

        public long Long;

        public DateTime aDateTime;

        public List<NestedExample> NestedExamples { set; get; }

        public List<int> Ints;

        public override string ToString()
        {
            return GetType().Name + 
                   "('" + 
                   aString + "', " + 
                   Int + ", " + 
                   aDateTime + ", " +
                   Long + ", List(" + 
                   String.Join(", ", NestedExamples) + "), List(" + 
                   String.Join(", ", Ints) + 
                   "))";
        }
    }

    public class NestedExample
    {
        public MyExample MyExample { set; get; }

        public bool Bool;

        public char Char;

        public MyStruct MyStruct;

        public NestedExample(MyExample myExample)
        {
            
        }

        public override string ToString()
        {
            return GetType().Name + "[" + Bool + ", '" + Char + "', " + MyStruct + "]";
        }
    }
}