using System;

namespace Game.GameEngine.Common
{
    [AttributeUsage(AttributeTargets.All)]
    public class PrototypeAttribute : Attribute
    {
        public string Message { get; set; }

        public PrototypeAttribute(string message = null)
        {
            Message = message;
        }
    }
}