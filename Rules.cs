using System.Collections.Generic;

namespace Turingas
{
    class Rules
    {
         Dictionary<char, ruleParameter> r = new Dictionary<char, ruleParameter>();

        public void add(char key,ruleParameter para)
        {
            if (r.ContainsKey(key)) return;
            r.Add(key, para);
        }

        public bool get(char key, ref ruleParameter para)
        {
            if (!r.ContainsKey(key))
            {
                if (r.ContainsKey('*')) key = '*';
                else return false;
            }
                
            para = r[key];
            return true;
        }
    
    }

    class ruleParameter
    {
        public char change;
        public char step;
        public int next;
    }

}
