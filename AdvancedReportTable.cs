namespace Tesseract_UI_Tools
{
    internal class AdvancedReportTable
    {
        private enum AdvancedReportTableState { STARTED, PAGEAWARE, STOPPED }

        StreamWriter streamWriter;
        DateTime startTime;
        DateTime startFileTime;
        AdvancedReportTableState state;
        public string FullPath { get; private set; }


        public AdvancedReportTable(string fullName,TesseractUIParameters Params)
        {
            startTime = DateTime.Now;
            FullPath = Path.Combine(fullName, $"report-{startTime.ToFileTimeUtc()}.html");
            state = AdvancedReportTableState.STOPPED;
            streamWriter = new StreamWriter(FullPath, false);
            streamWriter.WriteLine($"<!DOCTYPE html>");
            streamWriter.WriteLine($"<head>");
            streamWriter.WriteLine($"  <title>Report of process - {startTime}</title>");
            streamWriter.WriteLine($"</head>");
            streamWriter.WriteLine($"<body>");
            streamWriter.WriteLine($"  <table style='font-family: monospace; white-space: pre; text-align: left'>");
            streamWriter.WriteLine($"    <thead>");
            streamWriter.WriteLine($"      <tr><th>Parameter</th><th>Value</th></tr>");
            streamWriter.WriteLine($"    </thead>");
            streamWriter.WriteLine($"    <tbody>");
            streamWriter.WriteLine($"      <tr><td>Start Time</td><td>{startTime}</td></tr>");
            foreach(var prop in Params.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly).Where(o => o.GetIndexParameters().Length == 0 ))
            {
                streamWriter.WriteLine($"      <tr><td>{prop.Name}</td><td>{prop.GetValue(Params)}</td></tr>");
            }
            streamWriter.WriteLine($"    </tbody>");
            streamWriter.WriteLine($"  </table>");
            streamWriter.WriteLine($"  <hr/>");
            streamWriter.WriteLine($"  <table style='font-family: monospace; white-space: pre; text-align: left'>");
            streamWriter.WriteLine($"    <thead>");
            streamWriter.WriteLine($"      <tr><th>Start Time</th><th>Filename</th><th>Pages</th><th>Time Ellapsed</th><th>Words</th><th>Mean Confidence</th></tr>");
            streamWriter.WriteLine($"    </thead>");
            streamWriter.WriteLine($"    <tbody>");
        }

        public void StartFile(string filename)
        {
            if (state != AdvancedReportTableState.STOPPED) throw new Exception("StartFile was called on" + state);
            state = AdvancedReportTableState.STARTED;
            startFileTime = DateTime.Now;
            streamWriter.Write($"      <tr><td style='vertical-align: top'>{startFileTime}</td><td>{filename}</td>");
        }
        public void Pages(int pages)
        {
            if (state != AdvancedReportTableState.STARTED) throw new Exception("Pages was called on" + state);
            state = AdvancedReportTableState.PAGEAWARE;
            streamWriter.Write($"<td>{pages}</td>");
        }
        public void Stop(int words, float meanConf)
        {
            if (state != AdvancedReportTableState.PAGEAWARE) throw new Exception("Stop was called on" + state);
            state = AdvancedReportTableState.STOPPED;
            var ellapsed = DateTime.Now - startFileTime;
            streamWriter.WriteLine($"<td>{ellapsed.Seconds}s</td><td>{words}</td><td>{meanConf}</td></tr>");
        }
        public void Stop(string txt)
        {
            if (state != AdvancedReportTableState.PAGEAWARE) throw new Exception("Stop was called on" + state);
            state = AdvancedReportTableState.STOPPED;
            var ellapsed = DateTime.Now - startFileTime;
            streamWriter.WriteLine($"<td>{ellapsed.Seconds}s</td><td colspan='2' style='color: red'>{txt}</td></tr>");
        }

        public void Close()
        {
            if (state != AdvancedReportTableState.STOPPED) throw new Exception("Close was called on" + state);
            state = AdvancedReportTableState.STOPPED;
            streamWriter.WriteLine($"    </tbody>");
            streamWriter.WriteLine($"  </table>");
            streamWriter.WriteLine($"</body>");
            streamWriter.Close();
        }

        internal void SetError(Exception ex)
        {
            switch (state)
            {
                case AdvancedReportTableState.STARTED:
                    streamWriter.WriteLine($"<td>n/a</td>");
                    state = AdvancedReportTableState.PAGEAWARE;
                    Stop(ex.ToString());
                    break;
                case AdvancedReportTableState.PAGEAWARE:
                    Stop(ex.ToString());
                    break;
                case AdvancedReportTableState.STOPPED:
                    streamWriter.WriteLine($"      <tr><td style='vertical-align: top'>{DateTime.Now}</td><td colspan='5' style='color: red'>{ex}</td></tr>");
                    break;
            }
        }
    }
}
