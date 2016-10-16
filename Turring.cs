using System;
using System.Collections.Generic;
using System.IO;

namespace Turingas
{
    class Turing
    {
        char[] line;
        int position;
        Dictionary<int, Rules> rules = new Dictionary<int, Rules>();
        ruleParameter first;

       public bool isRunning = true;
        int c = 0;

        ruleParameter rp = new ruleParameter();
        Rules r = new Rules();

        public bool ReadFromFile(string filename)
        {

            if (!File.Exists(filename))
            {
                Console.WriteLine("Tokio failo nera");
                return false;
            }

            using (StreamReader sr = new StreamReader(filename))
            {
                position = Int32.Parse(sr.ReadLine());
                string temp = sr.ReadLine();
                line = temp.ToCharArray();

                int c = 0;               
                string buffer;
                while (sr.Peek() > -1)
                {
                    buffer = sr.ReadLine();
                    if (buffer.Length == 0) continue;

                    ruleParameter rp = new ruleParameter();

                    int num = 999;

                    int x = 0;
                    string tmp = "";
                    while (buffer[x] != ' ') tmp += buffer[x++];
                    x -= 1;

                    if (!Int32.TryParse(tmp, out num))
                    {
                        continue;
                    }
                    int id = num;

                    char current = buffer[2+x];
                    rp.change = buffer[4+x];
                    rp.step = buffer[6+x];

                    int r = 8+x;
                    tmp = "";
                    while (buffer.Length > r)  tmp += buffer[r++];
            
   
                    if (!Int32.TryParse(tmp,out num))
                    {
                        continue;
                    }
                    rp.next = num;

                    if (c == 0) first = rp;
                    if (!rules.ContainsKey(id)) rules.Add(id, new Rules());
                    rules[id].add(current, rp);

                }
            }
            return true;
        }


        public void Step()
        {
            if(isRunning)
            {
                DoCommand(rp.next, ref r, ref rp);
            }
        }

        public void Start()
        {
            DoCommand(0, ref r, ref rp);
        }

        private void DoCommand(int key1, ref Rules r,ref ruleParameter rp)
        {

            if (getRule(key1, ref r) && position > -1 && position < line.Length)
                if (r.get(line[position], ref rp))
                {
                    string tmp = new string(line);
                    Console.Write(tmp + " " + line[position] + " " + rp.change + " " + rp.next +  " " + c +  '\n');
                    if(rp.change != '*')
                         line[position] = rp.change;

                    if (rp.step == 'R') position++;
                    else position--;

                    c++;
                    
                }
                else { isRunning = false; Console.WriteLine("No symbol for the rule"); }
            else { isRunning = false; Console.WriteLine("No symbol for the rule");}
        }

        private bool getRule(int key, ref Rules r)
        {
            if (!rules.ContainsKey(key))
            {
                isRunning = false;
                return false;
            } 
            r = rules[key];
            return true;
        }


    }
}
