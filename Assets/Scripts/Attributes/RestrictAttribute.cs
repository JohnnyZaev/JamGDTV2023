using System;
using UnityEngine;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class RestrictAttribute : PropertyAttribute
    {
        public const string NotImplementInterfaceErrorMessage = "Object hasn't components of given type";
        public const string NotReferenceErrorMessage = "Property is not a reference type";
        public System.Type RequiredType { get; private set; }

        public RestrictAttribute(System.Type type)
        {
            RequiredType = type;
        }
    }
}