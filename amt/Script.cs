using System;
using System.IO;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

namespace amt
{
    /// <summary>
    /// Script の概要の説明です。
    /// </summary>
    public class Script
    {
        public ArrayList myScript;
        public ArrayList netScript;
        private Amt parent;
        public string script_name = "";
        public bool netScript_on = false;

        public Script(Amt Parent)
        {
            parent = Parent;
            myScript = new ArrayList();
            netScript = new ArrayList();
        }

        public void Add(string script_line)
        {
            myScript.Add(script_line);
        }

        public void netScript_Add(string script_line)
        {
            netScript.Add(script_line);
        }

        public bool LoadScript(string script_fullpath)
        {
            bool ret = false;
            script_name = Path.GetFileNameWithoutExtension(script_fullpath);

            try
            {
                StreamReader rText = new StreamReader(script_fullpath);
                string s1;
                int line_no = 0;
                myScript = new ArrayList();
                while ((s1 = rText.ReadLine()) != null)
                {
                    line_no++;
                    if (s1.StartsWith("%"))  // コメント行
                    {
                        if (s1.Contains("cmd_name"))
                        {
                            string[] s2 = s1.Split('=');
                            script_name = s2[1].Trim();
                        }
                    }
                    else
                    {
                        string s2 = Regex.Replace(s1, "%.*", "");
                        string s3 = Regex.Replace(s2, " {2,}", " ");
                        string s4 = s3.Trim();
                        myScript.Add(s4);
                    }
                }
                rText.Close();
                ExpandLoop1();
                ret = true;
            }
            catch (FileNotFoundException)
            {
                script_name = "No_File";
                System.Windows.Forms.MessageBox.Show("Error: " + script_name);
            }
            catch (Exception e1)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + e1.Message);
                throw;
            }
            return ret;
        }

        private bool isLoopStart(string line)
        {
            string[] parts = line.Split(' ');
            if (parts[0] == "FOR") return true;
            return false;
        }
        private bool isLoopEnd(string line)
        {
            string[] parts = line.Split(' ');
            if (parts[0] == "END") return true;
            return false;
        }

        private void ExpandLoop1()
        {
            for (int ii = 0; ii < myScript.Count;  ii++)
            {
                string line = (string)myScript[ii];
                if (isLoopStart(line))
                {
                    int jj = FindLoopEnd(ii);
                    ii = ExpandLoop(ii, jj);
                }
            }
        }

        private int FindLoopEnd(int start_line)
        {
            int end_line = -1;
            for (int ii = start_line+1; ii < myScript.Count; ii++)
            {
                string line = (string)myScript[ii];
                if (isLoopEnd(line))
                {
                    end_line = ii;
                    break;
                }
                else if (isLoopStart(line))
                {
                    int jj = FindLoopEnd(ii);
                    ii = ExpandLoop(ii, jj);
                }
            }
            return end_line;
        }

        private int ExpandLoop(int start_line, int end_line)
        {
            string loopline = (string)myScript[start_line];
            string parts = loopline.Replace("FOR ", "");
            string[] parts1 = parts.Split('=');
            string loop_var = "$" + parts1[0].Trim();
            ArrayList loop_list = new ArrayList();
            ArrayList loop_script = new ArrayList();
            ArrayList loop_script1 = new ArrayList();

            if (parts1[1].Contains(":"))
            {
                string[] parts2 = parts1[1].Split(':');
                int loop_start = System.Convert.ToInt32(parts2[0].Trim());
                int loop_end = System.Convert.ToInt32(parts2[1].Trim());
                string fmt = "";
                if (parts2[0].StartsWith("0"))
                {
                    int digit = loop_end.ToString().Length;
                    fmt = "d" + digit.ToString();
                }
                else
                {
                    fmt = "d";
                }

                for (int ll = loop_start; ll <= loop_end; ll++)
                {
                    loop_list.Add(ll.ToString(fmt));
                }
            }
            else if (Regex.IsMatch(parts1[1], @"\[.*\]"))
            {
                string pattern = @"(\[)(?<loop_list>.+?)(\])";
                string ss1 = Regex.Match(parts1[1], pattern).Groups["loop_list"].Value;
                string[] ss2 = ss1.Split(' ');
                for (int ll = 0; ll < ss2.Length; ll++)
                {
                    loop_list.Add(ss2[ll]);
                }
            }

            IEnumerator myEnumerator = loop_list.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string loop = (string)myEnumerator.Current;
                for (int ll = start_line+1; ll < end_line; ll++)
                {
                    string line = (string)myScript[ll];
                    string line1 = line.Replace(loop_var, loop);
                    loop_script1.Add(line1);
                }
            }
            for (int jj = start_line; jj <= end_line; jj++)
            {
                myScript.RemoveAt(start_line);
            }
            myEnumerator = loop_script1.GetEnumerator();
            string curr = null;
            int curr_no = start_line;
            while (myEnumerator.MoveNext())
            {
                curr = (string)myEnumerator.Current;
                myScript.Insert(curr_no, curr);
                curr_no++;
            }

            return curr_no-1;
        }

        public void SaveScript(string script_fullpath)
        {
            IEnumerator myEnumerator = myScript.GetEnumerator();
            string curr = null;
            StreamWriter wText = new StreamWriter(script_fullpath);
            while (myEnumerator.MoveNext())
            {
                curr = (string)myEnumerator.Current;
                wText.Write(curr + "\r\n");
            }
            wText.Flush();
            wText.Close();
        }
        public void ExecScript()
        {
            IEnumerator myEnumerator = myScript.GetEnumerator();

            while (myEnumerator.MoveNext() && parent.script_continue)
            {
                string s1 = (string)myEnumerator.Current;
                if (!parent.Exec_cmd(s1)) break;
            }
            if (parent.script_continue)
            {
                parent.Cnsl_msg("Script complete.\r\n");
                parent.CloseEscKey();
            }
            else
            {
                string s1 = (string)myEnumerator.Current;
                parent.Cnsl_msg("Script was broken before:\r\n" + s1 + "\r\n");
            }
        }
        public void ExecNetScript()
        {
            ArrayList currentList = new ArrayList();

            while (netScript_on)
            {
                if (netScript.Count > 0)
                {
                    lock (netScript)
                    {
                        for (int i = 0; i < netScript.Count; i++)
                        {
                            currentList.Add(netScript[i]);
                        }
                        netScript = new ArrayList();
                    }
                    IEnumerator myEnumerator = currentList.GetEnumerator();
                    while (myEnumerator.MoveNext())
                    {
                        string s1 = (string)myEnumerator.Current;
                        if (!parent.Exec_cmd(s1)) break;
                    }
                }
                else
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
        }
    }
}
