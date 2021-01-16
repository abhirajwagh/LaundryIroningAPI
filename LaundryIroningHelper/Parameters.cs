using System;
using System.Collections.Generic;
using System.Text;

namespace LaundryIroningHelper
{
    public class Parameters
    {
        public string ParamKey { get; set; }
        public Object Value { get; set; }
        public ParamDirection Direction { get; set; }
        public Parameters()
        {

        }
        public Parameters(string paramKey, object value, ParamDirection direction = ParamDirection.Input)
        {
            ParamKey = paramKey;
            Value = value;
            Direction = direction;
        }
    }

    public enum ParamDirection
    {
        //
        // Summary:
        //     The parameter is an input parameter.
        Input = 1,
        //
        // Summary:
        //     The parameter is an output parameter.
        Output = 2,
        //
        // Summary:
        //     The parameter is capable of both input and output.
        InputOutput = 3,
        //
        // Summary:
        //     The parameter represents a return value from an operation such as a stored procedure,
        //     built-in function, or user-defined function.
        ReturnValue = 6
    }
}
