using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace mdita_update
{
    public static class MditaUpdater
    {
        private static readonly string UPDATE_LINK = @"http://mdita.metropolitan.ac.rs/mdita-editor/services/greaterversions.php?idcurrent=";

        public static MditaVersion[] GetVersions(long currentVersion = 0)
        {
            using (WebClient client = new WebClient())
            {
                var json = client.DownloadString(UPDATE_LINK + currentVersion);
                return JsonConvert.DeserializeObject<MditaVersion[]>(json);
            }
        }

        public static void GetVersionsInBackground(RunWorkerCompletedEventHandler checkCompleted, long currentVersion = 0)
        {
            var bgw = new BackgroundWorker();
            bgw.DoWork += (sender, args) =>
            {
                args.Result = GetVersions(currentVersion);
            };
            bgw.RunWorkerCompleted += checkCompleted;
            bgw.RunWorkerAsync();
        }
    }
}
