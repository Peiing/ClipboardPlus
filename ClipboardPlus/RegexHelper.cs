using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace RegexTool
{
    class RegexHelper
    {
        private static Dictionary<string, string> exprs = new Dictionary<string, string>
        {
            { "mail", @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" },
            {"url",@"https?://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?" }
        };

        public static Dictionary<string,List<string>> getMatch(string text)
        {
            Dictionary<string, List<string>> matchDict = new Dictionary<string, List<string>>();
            foreach(string key in exprs.Keys)
            {
                MatchCollection mc = Regex.Matches(text, exprs[key]);
                foreach (Match m in mc)
                {
                    if (!matchDict.ContainsKey(key))
                    {
                        matchDict.Add(key, new List<string>());
                    }
                    matchDict[key].Add(m.ToString());
                }
            }
            return matchDict;
        }
    }
}
