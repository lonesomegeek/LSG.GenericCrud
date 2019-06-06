using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GenericCrud.Helpers
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class IgnoreInChangesetAttribute : Attribute
    {
    }
}
