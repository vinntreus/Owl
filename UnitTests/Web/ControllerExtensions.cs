using System;
using System.Web.Mvc;

namespace UnitTests.Web
{
    public static class ControllerExtensions
    {
        public static bool HasAttribute(this Controller controller, string method, Type type, params Type[] param)
        {
            return controller.GetType().GetMethod(method, param).GetCustomAttributes(type,false).Length > 0;
        } 
    }
}