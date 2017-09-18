using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wox.Plugin;
using HashHelper;
using RegexTool;
using System.Windows.Forms;
using System.Threading;

namespace ClipboardPlus
{
    public class Main : IPlugin
    {
        public static string clipData = String.Empty;

        public static int GetClipboard()
        {
            int clipDataType = 0;
            Thread thread = new Thread(() =>
            {
                IDataObject iData = Clipboard.GetDataObject();
                if (iData.GetDataPresent(DataFormats.Text))
                {
                    //clipData = (String)iData.GetData(DataFormats.Text);
                    clipData = Clipboard.GetText();
                    clipDataType = 1;
                }
                else if (iData.GetDataPresent(DataFormats.FileDrop))
                {
                    clipData = Clipboard.GetFileDropList()[0];
                    clipDataType = 2;
                }
            });
            thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
            thread.Start();
            thread.Join();
            return clipDataType;
        }
        public List<Result> Query(Query query)
        {
            int type = GetClipboard();
            List<Result> results = new List<Result>();
            Dictionary<string, List<string>> matchDict = RegexHelper.getMatch(clipData);
            switch (type)
            {
                // text
                case 1:
                    // regex
                    foreach (string key in matchDict.Keys)
                    {
                        for (int i = 0; i < matchDict[key].Count; i++)
                        {
                            results.Add(new Result()
                            {
                                Title = key,
                                SubTitle = matchDict[key][i],
                                IcoPath = "Images\\clip_red.png",
                                Action = e =>
                                {
                                    Clipboard.SetDataObject(matchDict[key][i]);
                                    return true;
                                }
                            });
                        }
                    }
                    
                    // hash
                    results.Add(new Result()
                    {
                        Title = "md5",
                        SubTitle = StringHashHelper.GetHash(clipData),
                        IcoPath = "Images\\clip_yellow.png",
                        Action = e =>
                        {
                            Clipboard.SetDataObject(StringHashHelper.GetHash(clipData));
                            return true;
                        }
                    });
                    break;
                // file
                case 2:
                    break;
            }
             return results;
        }

        public void Init(PluginInitContext context)
        {

        }
    }
}
