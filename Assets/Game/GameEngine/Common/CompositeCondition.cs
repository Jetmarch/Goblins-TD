using System;
using System.Collections.Generic;

namespace Game.GameEngine.Common
{
    public sealed class CompositeCondition
    {
        private List<Func<bool>> _conditions = new();

        public void AddCondition(Func<bool> condition)
        {
            _conditions.Add(condition);
        }
        
        public bool IsTrue()
        {
            foreach (var condition in _conditions)
            {
                if (!condition())
                {
                    return false;
                }
            }
            return true;
        }
    }
}