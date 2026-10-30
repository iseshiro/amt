using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace amt
{
    public class SourcePath
    {
        private List<inv_info> inv_list = null;
        public List<src_info> src_list = null;
        private Amt parent = null;
        public int filt_id = -1;
        public int src_length = 0;
        public int src_sfreq = 0;
        public int mic_count = 0;
        public int sp_count = 0;

        public SourcePath(Amt Parent)
        {
            parent = Parent;
            inv_list = new List<inv_info>();
            src_list = new List<src_info>();
        }

        public string src_filename(int src_id, int ich)
        {
            int dcnt = src_list[src_id].digit_cnt;
            string num_str_ich = "", file_path2;
            if (dcnt == 0) return src_list[src_id].file_path;
            if (dcnt == 1) num_str_ich = ich.ToString("0");
            else if (dcnt == 2) num_str_ich = ich.ToString("00");
            else if (dcnt == 3) num_str_ich = ich.ToString("000");
            else return "";
            file_path2 = src_list[src_id].file_path + num_str_ich + "." + src_list[src_id].file_ext;
            return file_path2;
        }

        public string src_filename_c(int src_id)
        {
            int dcnt = src_list[src_id].digit_cnt;
            string num_str_ich = "", file_path2;
            if (dcnt == 0) return src_list[src_id].file_path;
            if (dcnt == 1) num_str_ich = "%d";
            else if (dcnt == 2) num_str_ich = "%02d";
            else if (dcnt == 3) num_str_ich = "%03d";
            else return "";
            file_path2 = src_list[src_id].file_path + num_str_ich + "." + src_list[src_id].file_ext;
            return file_path2;
        }

        public void set_digit_point(String filepath, ref int digit_cnt, ref int digit_point)
        {
            int digit_point1 = 0, digit_point2 = 0, digit_point3 = 0;
            digit_point1 = filepath.LastIndexOf("1");
            digit_point2 = filepath.LastIndexOf("01");
            digit_point3 = filepath.LastIndexOf("001");
            digit_point = -2;
            int fullpath_leng = filepath.Length;

            if (digit_point3 == fullpath_leng - 7)
            {
                digit_cnt = 3;
                digit_point = digit_point3;
            }
            else if (digit_point2 == fullpath_leng - 6)
            {
                digit_cnt = 2;
                digit_point = digit_point2;
            }
            else if (digit_point1 == fullpath_leng - 5)
            {
                digit_cnt = 1;
                digit_point = digit_point1;
            }
            else
            {
                digit_cnt = 0;
                digit_point = fullpath_leng;
            }
        }

        public void src_list_add(int id, double start_time, double stop_time, float amp_db, double fade_in, double fade_out, string eq_type, String filepath, String moviepath)
        {
            int digit_cnt = 0;
            int digit_point = 0;
            string file_path = filepath;
            set_digit_point(filepath, ref digit_cnt, ref digit_point);
            if(digit_cnt > 0)
            {
                file_path = filepath.Remove(digit_point);
            }
            src_info ss = new src_info(id, digit_cnt, file_path, "wav");
            src_list.Add(ss);
            string filename = src_filename(id, 1);
            if (check_wav(filename, true))
            {
                ss.set_info(src_length, src_sfreq, start_time, stop_time, amp_db, fade_in, fade_out, eq_type, moviepath);
            }
        }

        //public int get_source_path1(int src_cnt)
        //{
        //    int src_id = -1;
        //    {
        //        string filename = "C:\\temp\\Geidai_song\\cal_song_m1.wav";

        //        if (!check_wav(filename, true))
        //        {
        //            string message = "Invalid wav header : " + filename;
        //            MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return -1;
        //        }
        //        if (channel_count == 2)
        //        {
        //            src_id = src_list.Count;
        //            src_list.Add(new src_info(src_list.Count + 1, 0, filename, ""));
        //        }
        //        else
        //        {
        //            src_id = set_source_path(src_cnt, filename);
        //        }
        //    }
        //    return src_id;
        //}

        public int get_source_path(int src_cnt, String filt)
        {
            int src_id = -1;
            if (parent.fd.OpenFileDialog("Source file", "*." + filt))
            {
                string filename = parent.fd.filename;

                if (!check_wav(filename, true))
                {
                    string message = "Invalid wav header : " + filename;
                    MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                if (channel_count == 1)
                {
                    src_id = set_source_path(src_cnt, filename);
                }
                else
                {
                    src_id = src_list.Count;
                    src_list.Add(new src_info(src_list.Count + 1, 0, filename, ""));
                }
            }
            return src_id;
        }

        public int set_source_path(int src_cnt, string filename)
        {
            int digit_cnt = 0;
            int digit_point = 0;
            string file_path = filename;
            set_digit_point(filename, ref digit_cnt, ref digit_point);
            string file_path_a = filename.Substring(0, digit_point);
            string file_path_b = filename.Substring(digit_point + digit_cnt + 1, filename.Length - digit_point - digit_cnt - 1);
            //                string file_path = filename.Remove(digit_point);
            int id = src_list.Count;
            //                src_list.Add(new src_info(src_list.Count + 1, digit_cnt, file_path, Src_ext));
            src_list.Add(new src_info(src_list.Count + 1, digit_cnt, file_path_a, file_path_b));

            string file_path2 = "";
            string parent_path = Path.GetDirectoryName(filename);
            for (int ich = 1; ich <= src_cnt; ich++)
            {

                file_path2 = src_filename(id, ich);
                if (!FileProcess.Exists(file_path2))
                {
                    string message = "ファイルがありません。 : " + file_path2;
                    MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                if (!check_wav(file_path2, ich==1))
                {
                    string message = "Invalid wav header : " + file_path2;
                    MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                if (ich == 1)
                {
                    string file_name1 = Path.GetFileName(file_path2);
                    parent.Cnsl_msg("File exists : " + file_name1 + "-");
                }
            }
            string file_name2 = Path.GetFileName(file_path2);
            parent.Cnsl_msg(file_name2 + "\n");
            return id;
        }

        public string filt_filename(int ich, int och, bool is_inv)
        {
            int dcnt = inv_list[filt_id].digit_cnt;
            string num_str_ich = "", num_str_och = "", file_path2;
            if (dcnt == 1) num_str_ich = ich.ToString("0");
            else if (dcnt == 2) num_str_ich = ich.ToString("00");
            else if (dcnt == 3) num_str_ich = ich.ToString("000");
            if (dcnt == 1) num_str_och = och.ToString("0");
            else if (dcnt == 2) num_str_och = och.ToString("00");
            else if (dcnt == 3) num_str_och = och.ToString("000");

            if (is_inv)
            {
                file_path2 = inv_list[filt_id].file_path + num_str_och + inv_list[filt_id].file_middle + num_str_ich + inv_list[filt_id].file_ext;
            }
            else
            {
                file_path2 = inv_list[filt_id].file_path + num_str_ich + inv_list[filt_id].file_middle + num_str_och + inv_list[filt_id].file_ext;
            }
            return file_path2;
        }

        public bool get_filter_path(int mic_cnt, int sp_cnt, String Filt, bool is_inv)
        {
            if (parent.fd.OpenFileDialog("Filter", Filt))
            {
                return get_filter_path1(parent.fd.filename, mic_cnt, sp_cnt, is_inv);
            }
            return false;
        }

        public bool get_filter_path1(string filename, int mic_cnt, int sp_cnt, bool is_inv)
        {
            string file_path2 = "";
            if(get_filter_path2(filename))
            {
                for (int ich = 1; ich <= mic_cnt; ich++)
                {
                    for (int och = 1; och <= sp_cnt; och++)
                    {

                        file_path2 = filt_filename(ich, och, is_inv);
                        if (!FileProcess.Exists(file_path2))
                        {
                            string message = "ファイルがありません。 : " + file_path2;
                            MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        if (ich == 1 && och == 1)
                        {
                            string file_name1 = Path.GetFileName(file_path2);
                            parent.Cnsl_msg("File exists : " + file_name1 + "-");
                        }
                    }
                }
                string file_name2 = Path.GetFileName(file_path2);
                parent.Cnsl_msg(file_name2 + "\n");
                return true;
            }
            return false;
        }

        public bool check_mic_sp_cnt(int ich_max, int och_max)
        {
            sp_count = och_max;
            for (int och = 1; och <= och_max; och++)
            {
                string file_path2 = filt_filename(1, och, false);
                if (!FileProcess.Exists(file_path2))
                {
                    if (och == 1) return false;
                    sp_count = och - 1;
                    break;
                }
            }

            mic_count = ich_max;
            for (int ich = 1; ich <= ich_max; ich++)
            {
                string file_path2 = filt_filename(ich, sp_count, false);
                if (!FileProcess.Exists(file_path2))
                {
                    mic_count = ich - 1;
                    break;
                }
            }
            return true;
        }

        public bool get_filter_path2(string filename)
        {
            int digit_point = 0;
            int digit_cnt = 0;
            int last_digit_point = 0;
            string file_path, file_middle;

            if ((last_digit_point = filename.LastIndexOf("001")) >= 0) { digit_cnt = 3; }
            else if ((last_digit_point = filename.LastIndexOf("01")) >= 0) { digit_cnt = 2; }
            else if ((last_digit_point = filename.LastIndexOf("1")) >= 0) { digit_cnt = 1; }
            if (digit_cnt == 0) return false;

            string ff = filename.Substring(0, last_digit_point - 1);

            if (digit_cnt == 3) { digit_point = ff.LastIndexOf("001"); }
            else if (digit_cnt == 2) { digit_point = ff.LastIndexOf("01"); }
            else if (digit_cnt == 1) { digit_point = ff.LastIndexOf("1"); }
            else return false;

            if (digit_point < 0)  return false;

            file_path = filename.Remove(digit_point);
            string parent_path = Path.GetDirectoryName(filename);
            file_middle = filename.Substring(digit_point + digit_cnt, last_digit_point - digit_point - digit_cnt);
            parent.Cnsl_msg("Filter folder :" + parent_path + "\n");
            filt_id = inv_list.Count;
            String Filt_ext = Path.GetExtension(filename);

            inv_list.Add(new inv_info(inv_list.Count + 1, digit_cnt, file_path, file_middle, Filt_ext));

            return true;
        }

        private int channel_count = 1;
        private bool check_wav(string filename, bool update_src_info)
        {
            WavStream ws = new WavStream(parent);
            bool check_ok = ws.WavHeaderCheck(filename);
            channel_count = ws.Channel();
            if (!check_ok)
            {
                string message = "ファイルが見つからないか、ヘッダーが壊れています。 : " + filename;
                MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            int word_cnt = ws.WordCount();
            int sfreq = ws.SampleFrequency();
            if (!update_src_info && (word_cnt != src_length || sfreq != src_sfreq))
            {
                string message = "ファイル長かサンプリング周波数が異なります : " + filename;
                MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(update_src_info)
            {
                src_length = word_cnt;
                src_sfreq = sfreq;
            }
            return true;
        }

        public int stream_open(int id, int ch_cnt)
        {
            int min_leng = 0;
            for (int ich = 1; ich <= ch_cnt; ich++)
            {
                string filename = src_filename(id, ich);
                int length = -1;
                unsafe
                {
                    fixed (Char* ss = filename.ToCharArray())
                    {
                        length = parent.asio.stream_open_mch_in(ss, 1, ich);
                    }
                }
                if(length < 0)
                {
                    parent.Cnsl_msg("Error rtconv_src_path : File Name (" + filename + ")\r\n");
                    return 0;
                }
                if (ich == 1 || length < min_leng)
                {
                    min_leng = length;
                }
                if (src_list[id].digit_cnt == 0) break;    // 16bit 2ch wav file
            }
            return min_leng;
        }

        public bool stream_close(int ch_cnt)
        {
            for (int ich = 1; ich <= ch_cnt; ich++)
            {
                ChannelInfo cinfo = parent.ch_info_in[ich - 1];
                parent.asio.stream_close((int)cinfo.type, cinfo.btn_no);
            }

            return true;
        }
        public int playlist_stream_attach(int id, int start_pnt, int stop_pnt)
        {
            bool is_single = false;
            if (src_list[id].digit_cnt == 0) is_single = true;
            return parent.asio.playlist_stream_attach(id, start_pnt, stop_pnt, is_single);
        }

        public void playlist_stream_detach()
        {
            //            parent.asio.playlist_stream_detach();
        }
    }

    public class src_info
    {
        public int id;
        public int digit_cnt = 0;
        public string file_path = "";
        public string file_middle = "";
        public string file_ext = "";
        public int length;
        public int sfreq;
        public double start_time;
        public double stop_time;
        public string eq_type;
        public double amp_db;
        public double fade_in;
        public double fade_out;
        public string movie_path = "";

        public src_info(int ID, int Digit_cnt, string File_path, string File_ext)
        {
            id = ID;
            digit_cnt = Digit_cnt;
            file_path = File_path;
            file_middle = "";
            file_ext = File_ext;
        }

        public void set_info(int Length, int Sfreq, double Start_time, double Stop_time, double Amp_db, double Fade_in, double Fade_out, string Eq_type, string Movie)
        {
            length = Length;
            sfreq = Sfreq;
            start_time = Start_time;
            stop_time = Stop_time;
            eq_type = Eq_type;
            amp_db = Amp_db;
            fade_in = Fade_in;
            fade_out = Fade_out;
            movie_path = Movie;
        }
    }

    public class inv_info
    {
        public int id;
        public int digit_cnt = 0;
        public string file_path = "";
        public string file_middle = "";
        public string file_ext = "";

        public inv_info(int ID, int Digit_cnt, string File_path, string File_middle, string File_ext)
        {
            id = ID;
            digit_cnt = Digit_cnt;
            file_path = File_path;
            file_middle = File_middle;
            file_ext = File_ext;
        }
    }
}
