using OpenCvSharp;

namespace Tesseract_UI_Tools
{
    public class OCROutput
    {
        public Rect[] Rects = Array.Empty<Rect>();
        public string[] Components = Array.Empty<string>();
        public float[] Confidences = Array.Empty<float>();
        public string[] Debug = Array.Empty<string>();
        private string DebugString;

        public OCROutput(string Debug = "OCROutput")
        {
            DebugString = Debug;
        }

        public void Save(string OutputFile, string Extra="")
        {
            System.Diagnostics.Debug.Assert(Rects.Length == Components.Length && Components.Length == Confidences.Length);
            using( StreamWriter writer = new StreamWriter(OutputFile, false, System.Text.Encoding.UTF8 ))
            {
                writer.WriteLine($"Origin\tX1\tY1\tX2\tY2\tConfidence\tText\t{Extra}");
                for (int i = 0; i < Rects.Length; i++)
                {
                    if(Components[i].Trim() != "")
                    {
                        writer.WriteLine($"{(Debug.Length > 0 ? Debug[i] : DebugString)}\t{Rects[i].TopLeft.X}\t{Rects[i].TopLeft.Y}\t{Rects[i].BottomRight.X}\t{Rects[i].BottomRight.Y}\t{Confidences[i]}\t{Components[i]}");
                    }
                }
            }
        }

        public static OCROutput Load(string OutputFile)
        {
            List<string> Lines = new List<string>();
            using( StreamReader reader = new StreamReader(OutputFile, System.Text.Encoding.UTF8))
            {
                string? CurrLine;
                reader.ReadLine(); // Drop Header
                while((CurrLine = reader.ReadLine()) != null)
                {
                    if( CurrLine.Split('\t').Last().Trim() != "")
                    {
                        Lines.Add(CurrLine);
                    }
                }
            }
            OCROutput Output = new OCROutput();
            Output.Rects = new Rect[Lines.Count()];
            Output.Components = new string[Lines.Count()];
            Output.Confidences = new float[Lines.Count()];
            Output.Debug = new string[Lines.Count()];
            for(int i = 0; i < Lines.Count(); i++)
            {
                string CurrLine = Lines[i];
                string[] Values = CurrLine.Split('\t');
                Output.Debug[i] = Values[0];
                int X1 = int.Parse(Values[1]);
                int Y1 = int.Parse(Values[2]);
                int X2 = int.Parse(Values[3]);
                int Y2 = int.Parse(Values[4]);
                Output.Rects[i] = new Rect(X1, Y1, X2 - X1, Y2 - Y1);
                Output.Confidences[i] = float.Parse(Values[5]);
                Output.Components[i] = Values[6];
            }
            return Output;
        }

        public static OCROutput MergeBest(OCROutput AOCROutput, OCROutput BOCROutput)
        {
            // Populate map of A to B intersections
            Dictionary<int, int> AMatchesB = new Dictionary<int, int>();
            for( int i = 0; i < AOCROutput.Rects.Length; i++)
            {
                Rect ACurr = AOCROutput.Rects[i];
                for( int j = 0; j < BOCROutput.Rects.Length; j++)
                {
                    Rect BCurr = BOCROutput.Rects[j];
                    if( ACurr.IntersectsWith(BCurr))
                    {
                        AMatchesB.Add(i, j);
                        break;
                    }
                }
                if( !AMatchesB.ContainsKey(i))
                {
                    AMatchesB.Add(i, -1);
                }
            }
            // Populate list of B that were not intersected
            List<int> BToAdd = new List<int>();
            for(int i = 0; i < BOCROutput.Rects.Length; i++)
            {
                if( !AMatchesB.ContainsValue(i))
                {
                    BToAdd.Add(i);
                }
            }
            // Merge
            int NewSize = AMatchesB.Count + BToAdd.Count;
            OCROutput Output = new OCROutput("MergeBest");
            Output.Rects = new Rect[NewSize];
            Output.Components = new string[NewSize];
            Output.Confidences = new float[NewSize];
            Output.Debug = new string[NewSize];
            for(int i=0; i<AMatchesB.Count; i++)
            {
                if( AMatchesB[i] == -1 || AOCROutput.Confidences[i] > BOCROutput.Confidences[AMatchesB[i]] )
                {
                    Output.Rects[i] = AOCROutput.Rects[i];
                    Output.Components[i] = AOCROutput.Components[i];
                    Output.Confidences[i] = AOCROutput.Confidences[i];
                    Output.Debug[i] = AOCROutput.Debug.Length > 0 ? AOCROutput.Debug[i] : AOCROutput.DebugString;
                }
                else
                {
                    Output.Rects[i] = BOCROutput.Rects[AMatchesB[i]];
                    Output.Components[i] = BOCROutput.Components[AMatchesB[i]];
                    Output.Confidences[i] = BOCROutput.Confidences[AMatchesB[i]];
                    Output.Debug[i] = BOCROutput.Debug.Length > 0 ? BOCROutput.Debug[AMatchesB[i]] : BOCROutput.DebugString;
                }
            }
            int Index = AMatchesB.Count;
            foreach(int BIndex in BToAdd)
            {
                Output.Rects[Index] = BOCROutput.Rects[BIndex];
                Output.Components[Index] = BOCROutput.Components[BIndex];
                Output.Confidences[Index] = BOCROutput.Confidences[BIndex];
                Output.Debug[Index] = BOCROutput.Debug.Length > 0 ? BOCROutput.Debug[BIndex] : BOCROutput.DebugString;
                Index++;
            }
            return Output;
        }
    }
}
