using System;
using System.IO;
using System.Windows.Forms;

namespace amt
{
	/// <summary>
	/// Property の概要の説明です。
	/// </summary>
	public class Property
	{
		private Amt parent;
        private string property_fullpath;
		public Property(Amt Parent)
		{
			parent = Parent;
            parent.app_file_path = GetFileSystemPath();
            property_fullpath = parent.app_file_path + @"\property.txt";
            parent.Cnsl_msg("Application Data Path : "+ parent.app_file_path + "\r\n");
		}
        public bool Load(bool with_driver_info)
		{
			bool stored = true;
			try
			{
				StreamReader myStreamReader = new System.IO.StreamReader(property_fullpath);
                string property_text = myStreamReader.ReadToEnd();
				myStreamReader.Close();
                GetProperty(property_text, with_driver_info);
			}
			catch  (FileNotFoundException)
			{
				stored=false;
			}
			catch  (Exception e1)
			{
				System.Windows.Forms.MessageBox.Show( "Error: "+e1.Message );
				throw;
			}
			return stored;
		}

        private static string GetFileSystemPath()
        {
            // パスを取得
            string path = String.Format(@"{0}\{1}\{2}",
              Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),  // ベース・パス
              Application.CompanyName,            // 会社名
              Application.ProductName);           // 製品名

            // パスのフォルダを作成
            lock (typeof(Application))
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            return path;
        }

        private void GetProperty(string property_text, bool with_driver_info)
        {
            string[] sr1 = property_text.Split('\n');
            foreach (string sr in sr1)
            {
                if ((sr.IndexOf("driver_name") == 0) && with_driver_info)
                {
                    string[] sr2 = sr.Split('=');
                    parent.selected_driver_name = sr2[1].Trim();
                }
                else if (sr.IndexOf("input_btn_cnt") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.in_btn_cnt = Convert.ToInt32(sr2[1].Trim());
                }
                else if (sr.IndexOf("output_btn_cnt") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.out_btn_cnt = Convert.ToInt32(sr2[1].Trim());
                }
                else if (sr.IndexOf("work_btn_cnt") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.work_btn_cnt = Convert.ToInt32(sr2[1].Trim());
                }
                else if (sr.IndexOf("start_time_ms") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.default_start_time_ms = Convert.ToDouble(sr2[1].Trim());
                }
                else if (sr.IndexOf("end_time_ms") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.default_end_time_ms = Convert.ToDouble(sr2[1].Trim());
                }
                else if (sr.IndexOf("grid_time_ms") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.default_grid_time_ms = Convert.ToDouble(sr2[1].Trim());
                }
                else if (sr.IndexOf("magnify") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.default_magnify = Convert.ToDouble(sr2[1].Trim());
                }
                else if (sr.IndexOf("start_freq_hz") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.default_start_freq_hz = Convert.ToDouble(sr2[1].Trim());
                }
                else if (sr.IndexOf("end_freq_hz") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.default_end_freq_hz = Convert.ToDouble(sr2[1].Trim());
                }
                else if (sr.IndexOf("grid_freq_hz") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.default_grid_freq_hz = Convert.ToDouble(sr2[1].Trim());
                }
                else if (sr.IndexOf("fft_point") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.default_fft_point = Convert.ToInt32(sr2[1].Trim());
                }
                else if (sr.IndexOf("amp_log") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.default_amp_log = (sr2[1].Trim() == "true");
                }
                else if (sr.IndexOf("disp_range_db") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.default_disp_range_db = Convert.ToInt32(sr2[1].Trim());
                }
                else if (sr.IndexOf("grid_range_d") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.default_grid_range_db = Convert.ToInt32(sr2[1].Trim());
                }
                else if (sr.IndexOf("level_adjust_db") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.default_level_adjust_db = Convert.ToInt32(sr2[1].Trim());
                }
                else if (sr.IndexOf("work_folder") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.work_folder = sr2[1].Trim();
                }
                else if (sr.IndexOf("assign_ch") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.assign_ch = (sr2[1].Trim() == "true");
                }
                else if (sr.IndexOf("assign_in_ch") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.assign_in_ch = sr2[1].Trim();
                }
                else if (sr.IndexOf("assign_out_ch") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.assign_out_ch = sr2[1].Trim();
                }
                else if (sr.IndexOf("script_folder") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.script_folder = sr2[1].Trim();
                }
                else if (sr.IndexOf("data_length") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.default_dleng = Convert.ToInt32(sr2[1].Trim());
                }
                else if (sr.IndexOf("max_row_of_button") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.max_row_of_button = Convert.ToInt32(sr2[1].Trim());
                }
                else if (sr.IndexOf("filter_db_file") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.filter_db_file = sr2[1].Trim();
                }
                else if (sr.IndexOf("song_db_file") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.song_db_file = sr2[1].Trim();
                }
                else if (sr.IndexOf("contents_db_file") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.contents_db_file = sr2[1].Trim();
                }
                else if (sr.IndexOf("inv_file") == 0)
                {
                    string[] sr2 = sr.Split('=');
                    parent.inv_file = sr2[1].Trim();
                }
            }
        }
        public void Save()
		{
			try
			{
                StreamWriter myStreamWriter = new System.IO.StreamWriter(property_fullpath);
                myStreamWriter.WriteLine("driver_name = {0:D}", parent.selected_driver_name);
                myStreamWriter.WriteLine("data_length = {0}", parent.default_dleng);
                myStreamWriter.WriteLine("input_btn_cnt = {0:D}", parent.in_btn_cnt);
                myStreamWriter.WriteLine("output_btn_cnt = {0:D}", parent.out_btn_cnt);
                myStreamWriter.WriteLine("work_btn_cnt = {0:D}", parent.work_btn_cnt);
                myStreamWriter.WriteLine("start_time_ms = {0:F}", parent.default_start_time_ms);
                myStreamWriter.WriteLine("end_time_ms = {0:F}", parent.default_end_time_ms);
                myStreamWriter.WriteLine("grid_time_ms = {0:F}", parent.default_grid_time_ms);
                myStreamWriter.WriteLine("magnify = {0:E}", parent.default_magnify);
                myStreamWriter.WriteLine("start_freq_hz = {0:F}", parent.default_start_freq_hz);
                myStreamWriter.WriteLine("end_freq_hz = {0:F}", parent.default_end_freq_hz);
                myStreamWriter.WriteLine("grid_freq_hz = {0:F}", parent.default_grid_freq_hz);
                myStreamWriter.WriteLine("fft_point = {0:D}", parent.default_fft_point);
                if (parent.default_amp_log)
                {
                    myStreamWriter.WriteLine("amp_log = true");
                }
                else
                {
                    myStreamWriter.WriteLine("amp_log = false");
                }
                myStreamWriter.WriteLine("disp_range_db = {0:D}", parent.default_disp_range_db);
                myStreamWriter.WriteLine("grid_range_db = {0:D}", parent.default_grid_range_db);
                myStreamWriter.WriteLine("level_adjust_db = {0:D}", parent.default_level_adjust_db);
                myStreamWriter.WriteLine("work_folder = {0}", parent.work_folder);
                myStreamWriter.WriteLine("max_row_of_button = {0}", parent.max_row_of_button);
                if (parent.assign_ch)
                {
                    myStreamWriter.WriteLine("assign_ch = true");
                    myStreamWriter.WriteLine("assign_in_ch = {0}", parent.assign_in_ch);
                    myStreamWriter.WriteLine("assign_out_ch = {0}", parent.assign_out_ch);
                }
                else
                {
                    myStreamWriter.WriteLine("assign_ch = false");
                }
                myStreamWriter.WriteLine("script_folder = {0}", parent.script_folder);
                myStreamWriter.WriteLine("filter_db_file = {0}", parent.filter_db_file);
                myStreamWriter.WriteLine("song_db_file = {0}", parent.song_db_file);
                myStreamWriter.WriteLine("contents_db_file = {0}", parent.contents_db_file);
                myStreamWriter.WriteLine("inv_file = {0}", parent.inv_file);

                myStreamWriter.Close();
            }
			catch  (Exception e1)
			{
				System.Windows.Forms.MessageBox.Show( "Error: "+e1.Message );
				throw;
			}
		}
	}
}
