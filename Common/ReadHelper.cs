using System;
using System.Reflection;
using System.Security.Claims;
using System.Xml.Linq;

namespace Common
{
	public static class ReadHelper
	{
		public static void CheckClass()
		{
			var @class = "Length";
			var test = Type.GetType(@class);
        }
        public static void Check()
		{
            string @class = "Length";
			string method = "meter";
            Type? myClassType = Type.GetType(String.Format("{0}", @class));
			object? instance = myClassType == null ? null : "Yousef";
            var myMethodExists = myClassType.GetMethod(method) != null;
        }
        public static bool HasMethod(this object objectToCheck, string methodName)
        {
            var type = objectToCheck.GetType();
            return type.GetMethod(methodName) != null;
        }
    }
}

