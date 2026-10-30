using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Text.Json;
using System.Net.Http;
using Asio_io;
using System.Web.Http.Results;
using static System.Net.Mime.MediaTypeNames;

namespace amt
{
    public partial class MoverForm : Form
    {
        private Amt parent = null;
        private string responseBody = "";
        private string downloadDir = "";
        private asio_io myasio = null;

        public MoverForm(Amt Parent)
        {
            InitializeComponent();

            parent = Parent;
            myasio = parent.asio;
        }
        static void DownloadFileAsync(string fileUrl, string downloadPath)
        {
            //DownloadFileAsync_new(fileUrl, downloadPath).Wait();

            //WAVファイルをダウンロード
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(fileUrl), downloadPath);
            }
        }

        private void mover_start_btn_Click(object sender, EventArgs e)
        {
            if( parent.sample_freq != 48000 )
            {
                int p_sfreq = parent.sample_freq;
                MessageBox.Show("Sample Freq is not 48000Hz. ---> " + p_sfreq.ToString() + "Hz");
                return;
            }
            downloadDir = workfolder_tb.Text;
            if (!Directory.Exists(downloadDir))
            {
                MessageBox.Show(downloadDir + " not exist.");
                return;
            }
            char last_char = downloadDir[downloadDir.Length - 1];
            if (last_char != '\\')
            {
                downloadDir = downloadDir + "\\";
            }

            string dst_filename = downloadDir + project_name_tb.Text + ".json";

            try
            {
                StreamReader sr1 = new StreamReader(dst_filename, Encoding.GetEncoding("utf-8"));
                responseBody = sr1.ReadToEnd();
                sr1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            Root bb = JsonSerializer.Deserialize<Root>(responseBody);

            int media_count = bb.g_proj.media_list.Count;
            ConsoleMsg1.AppendText("Project Name = " + bb.g_proj.name + "\r\n");
            ConsoleMsg1.AppendText("Project Sample Freq = " + bb.g_proj.sampl_freq.ToString() + "\r\n");
            ConsoleMsg1.AppendText("Media Count = " + media_count.ToString() + "\r\n");
            WavStream ws = new WavStream(parent);
            for (int media_no = 0; media_no < media_count; media_no++)
            {
                string media_name = bb.g_proj.media_list[media_no].name;
                string dst_media_name = downloadDir + bb.g_proj.media_list[media_no].name;
                ConsoleMsg1.AppendText("Media Name = " + bb.g_proj.media_list[media_no].name + "\r\n");
                FileInfo fileInfo = new FileInfo(dst_media_name);
                ConsoleMsg1.AppendText("Media Sample Freq = " + bb.g_proj.sampl_freq.ToString() + "\r\n");
                if (!fileInfo.Exists)
                {
                    MessageBox.Show("Media Sample Freq is not Project Sample Freq.");
                    return;
                }
                if (!ws.WavHeaderCheck(dst_media_name))
                {
                    int m_sfreq = ws.SampleFrequency();
                    if( bb.g_proj.sampl_freq != m_sfreq )
                    {
                        MessageBox.Show(dst_media_name + ": Sample Freq = " + m_sfreq.ToString() + "Hz");
                        return;
                    }
                }
            }

            int project_datasize = (int)Math.Floor(bb.g_proj.end_time * (double)bb.g_proj.sampl_freq);
            float[] data1 = new float[project_datasize];

            string partwav_folder = downloadDir + "partwav";
            if (!Directory.Exists(partwav_folder))
            {
                Directory.CreateDirectory(partwav_folder);
            }
            string output_folder = downloadDir + "output";
            if (!Directory.Exists(output_folder))
            {
                Directory.CreateDirectory(output_folder);
            }
            double time_intvl = (double)bb.g_proj.frame_size / (double)parent.sample_freq;
            int time_seq_size = (int)(bb.g_proj.end_time / time_intvl) + 2;
            Vector3[][] time_seq = new Vector3[bb.g_proj.part.Count][];
            
            for (int part_no = 0; part_no < bb.g_proj.part.Count; part_no++)
            {
                int file_count = bb.g_proj.part[part_no].file.Count;
                string wav_out_fname = partwav_folder + "\\part" + part_no.ToString() + ".wav";

                for (int file_no = 0; file_no < file_count; file_no++)
                {
                    int media_id = bb.g_proj.part[part_no].file[file_no].media_id;
                    string fname = bb.g_proj.media_list[media_id].name;

                    string wav_full_path = downloadDir + fname;
                    if (!ws.WavRead(wav_full_path, true))
                    {
                        if(ws.WavError == WavStream.wav_error.sample_freq_error)
                        {
                            ConsoleMsg1.AppendText("Wav sample Freq. is different from Asio Sample Freq.\r\n");
                        }
                        else return;
                    }
                    double delay = bb.g_proj.part[part_no].file[file_no].delay_time;
                    double st_time = bb.g_proj.part[part_no].file[file_no].start_time;
                    double ed_time = bb.g_proj.part[part_no].file[file_no].end_time;
                    int dl_pnt = (int)Math.Floor(delay * (double)bb.g_proj.sampl_freq);
                    int st_pnt = (int)Math.Floor(st_time * (double)bb.g_proj.sampl_freq);
                    int ed_pnt = (int)Math.Floor(ed_time * (double)bb.g_proj.sampl_freq);

                    ws.AddData(data1, dl_pnt, st_pnt, ed_pnt);
                }
                int word_count = 0;
                unsafe
                {
                    fixed (float* d1 = data1)
                    {
                        fixed (Char* s1 = wav_out_fname.ToCharArray())
                        {
                            word_count = myasio.resample(d1, project_datasize, s1, bb.g_proj.sampl_freq, parent.sample_freq, true);
                        }
                    }
                }
                ConsoleMsg1.AppendText("Resample complete : word count = " + word_count.ToString() + "\r\n");

                List<Sequence> seq_data = bb.g_proj.part[part_no].sequence;
                ConsoleMsg1.AppendText("time_seq_size=" + time_seq_size.ToString() + "\r\n");
                time_seq[part_no] = new Vector3[time_seq_size];
                if (!make_time_sequence_d(ref time_seq[part_no], seq_data, time_intvl, bb.g_proj.end_time)) return;
            }

            //vr_src.CreateVRFiles(80, output_folder, bb.g_proj, time_seq);
            int fmic_cnt = 80;
            int shift_size = bb.g_proj.frame_size;
            int sfreq = bb.g_proj.sampl_freq;
            myasio.fmove_init(fmic_cnt, shift_size, sfreq);
            for (int ich = 1; ich <= fmic_cnt; ich++)
            {
                string filename = output_folder + "\\output_" + ich.ToString("00") + ".wav";
                int error = -1;
                unsafe
                {
                    fixed (Char* ss = filename.ToCharArray())
                    {
                        error = myasio.fmove_set_outfile1(ich, ss);
                    }
                }
                if (error < 0)
                {
                    MessageBox.Show("Can not create output file\n" + filename, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            int src_count = bb.g_proj.part.Count;
            myasio.fmove_src_cnt(src_count);
            parent.rtmove_on = true;
            for (int src_no = 0; src_no < src_count; src_no++)
            {
                Vector3[] ts = time_seq[src_no];

                //parent.DebugLog("fmove_add_src" + src_no.ToString());
                myasio.fmove_add_src(src_no, 0.0, 1.0, 0.0, bb.g_proj.end_time, time_seq_size);
                string filename = partwav_folder + "\\part" + src_no.ToString() + ".wav";
                //parent.DebugLog("fmove_set_fileinfo : " + filename);

                unsafe
                {
                    fixed (Char* ss = filename.ToCharArray())
                    {
                        myasio.fmove_set_fileinfo(src_no, ss);
                    }
                }
                Vector3 prev_sd = new Vector3(0.0f, 0.0f, 0.0f);
                for (int seq_no = 0; seq_no < time_seq_size; seq_no++)
                {
                    Vector3 sd = ts[seq_no];
                    bool stay = (prev_sd == sd);
                    bool result = myasio.fmove_add_seq(src_no, seq_no, sd.X, sd.Y, sd.Z, stay);
                    if (!result) return;
                    prev_sd = sd;
                }
            }
            //parent.DebugLog("VR Creator : keep_moving");
            bool keep_moving = true;
            int shift_no = 1;
            //double progress_rate = 100.0 / (double)shift_count;

            while (keep_moving)
            {
                //int ii = (int)(progress_rate * (double)shift_no);
                //if (ii > 100) ii = 100;
                ConsoleMsg1.AppendText("Shift : " + shift_no.ToString() + "/" + time_seq_size.ToString() + "\r\n");
                keep_moving = myasio.fmove_move(shift_no);
                double cur_x = myasio.fmove_current_posi(0, 0);
                double cur_y = myasio.fmove_current_posi(0, 1);
                double cur_z = myasio.fmove_current_posi(0, 2);
                double start_time = (double)(shift_no - 1) * (double)shift_size / (double)sfreq;
                double end_time = (double)shift_no * (double)shift_size / (double)sfreq;
                //string log_txt = "Moving : " + shift_no.ToString() + ", " + start_time.ToString("0.00") + "-" + end_time.ToString("0.00") + ", (" + cur_x.ToString("0.00") + ", " + cur_y.ToString("0.00") + ", " + cur_z.ToString("0.00") + ")";
                //parent.DebugLog(log_txt);
                shift_no++;
                if (shift_no >= time_seq_size)
                {
                    if (keep_moving) ConsoleMsg1.AppendText("Last proc.\r\n");
                    if (keep_moving)
                    {
                        MessageBox.Show("Error: Sequence size is missing: time_seq_size=" + time_seq_size.ToString(), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    ConsoleMsg1.AppendText("Last proc.\r\n");
                }
            }
            ConsoleMsg1.AppendText("Move Complete.\r\n");
            //float rr = myasio.fmove_maxval();
            //parent.Cnsl_msg("Max val = " + rr.ToString());
            for (int ich = 1; ich <= fmic_cnt; ich++)
            {
                string filename = output_folder + "\\output_" + ich.ToString("00") + ".wav";
                int error = -1;
                unsafe
                {
                    fixed (Char* ss = filename.ToCharArray())
                    {
                        error = myasio.fmove_close1(ich, ss);
                    }
                }
                if (error < 0)
                {
                    MessageBox.Show("Can not create output file\n" + filename, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool make_time_sequence_d(ref Vector3[] time_seq, List<Sequence> seq_data, double time_intvl, double total_time)
        {
            double current_time = 0.0;
            int current_state_no = 0;
            bool state_changed = true;
            PolarPosition current_position_p = new PolarPosition();
            Vector3 current_position_o = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 current_delta_o = new Vector3(0.0f, 0.0f, 0.0f);
            PolarPosition seq_to;
            PolarPosition current_delta_p = new PolarPosition();
            int divide_count;
            double next_random_time = 0.0;
            Random cRandom = new System.Random();
            int seq_no = 0;

            while (current_time < total_time)
            {
                if (seq_no >= time_seq.Length) return false;
                if (current_state_no + 1 < seq_data.Count)
                {
                    if (current_time >= seq_data[current_state_no + 1].time)
                    {
                        state_changed = true;
                        current_state_no = current_state_no + 1;
                    }
                }

                switch (seq_data[current_state_no].motion.state)
                {
                    case "unmoving": // need unmoving first
                        if (state_changed)
                        {
                            current_position_p = seq_data[current_state_no].motion.polar_position;
                        }
                        break;
                    case "linearly":
                        if (state_changed)
                        {
                            current_position_p = seq_data[current_state_no].motion.polar_position;
                            double distance = current_position_p.radius;
                            double azimuth = current_position_p.azimuth / 180.0 * Math.PI;
                            double elevation = current_position_p.elevation / 180.0 * Math.PI;
                            double xx = distance * Math.Cos(elevation) * Math.Sin(azimuth);
                            double yy = distance * Math.Cos(elevation) * Math.Cos(azimuth);
                            double zz = distance * Math.Sin(elevation);
                            current_position_o = new Vector3((float)xx, (float)yy, (float)zz);
                            seq_to = seq_data[current_state_no + 1].motion.polar_position;
                            divide_count = (int)Math.Floor((seq_data[current_state_no + 1].time - seq_data[current_state_no].time) / time_intvl) + 1;
                            current_delta_o = OrthogonalDelta(current_position_p, seq_to, divide_count);
                        }
                        else
                        {
                            current_position_o = current_position_o + current_delta_o;
                            current_position_p = Ortho2Polar(current_position_o);
                        }
                        break;
                    case "spherically":
                        if (state_changed)
                        {
                            current_position_p = seq_data[current_state_no].motion.polar_position;
                            seq_to = seq_data[current_state_no + 1].motion.polar_position;
                            divide_count = (int)Math.Floor((seq_data[current_state_no + 1].time - seq_data[current_state_no].time) / time_intvl) + 1;
                            Cycle cycle = seq_data[current_state_no].motion.cycle;
                            current_delta_p = PolarDelta(current_position_p, seq_to, divide_count, cycle, time_intvl);
                        }
                        else
                        {
                            current_position_p.radius = current_position_p.radius + current_delta_p.radius;
                            current_position_p.azimuth = current_position_p.azimuth + current_delta_p.azimuth;
                            current_position_p.elevation = current_position_p.elevation + current_delta_p.elevation;
                            if (current_position_p.elevation > 90.0)
                            {
                                current_position_p.azimuth = current_position_p.azimuth + 180.0;
                                current_position_p.elevation = 180.0 - current_position_p.elevation;
                                current_delta_p.elevation = -current_delta_p.elevation;
                            }
                            else if (current_position_p.elevation < -90.0)
                            {
                                current_position_p.azimuth = current_position_p.azimuth + 180.0;
                                current_position_p.elevation = -180.0 - current_position_p.elevation;
                                current_delta_p.elevation = -current_delta_p.elevation;
                            }
                            if (current_position_p.azimuth > 180.0)
                            {
                                current_position_p.azimuth = current_position_p.azimuth - 360.0;

                            }
                            else if (current_position_p.azimuth < -180.0)
                            {
                                current_position_p.azimuth = current_position_p.azimuth + 360.0;
                            }
                        }
                        break;
                    case "randomly":
                        if (state_changed)
                        {
                            next_random_time = current_time + seq_data[current_state_no].motion.duration;
                            current_position_p = seq_data[current_state_no].motion.polar_position;
                        }
                        if (current_time >= next_random_time)
                        {
                            next_random_time = next_random_time + seq_data[current_state_no].motion.duration;
                            current_position_p.azimuth = cRandom.NextDouble() * 360.0 - 180.0;
                            current_position_p.elevation = cRandom.NextDouble() * 180.0 - 90.0;

                        }
                        break;
                }
                state_changed = false;
                current_time = current_time + time_intvl;
                time_seq[seq_no++] = Polar2Ortho(current_position_p);
            }
            return true;
        }

        private Vector3 OrthogonalDelta(PolarPosition seq_from, PolarPosition seq_to, int divide_count)
        {
            double azi_from = seq_from.azimuth / 180.0 * Math.PI;
            double ele_from = seq_from.elevation / 180.0 * Math.PI;
            double azi_to = seq_to.azimuth / 180.0 * Math.PI;
            double ele_to = seq_to.elevation / 180.0 * Math.PI;

            double f_rd = seq_from.radius * Math.Cos(ele_from);
            double f_x = f_rd * Math.Sin(azi_from);
            double f_y = f_rd * Math.Cos(azi_from);
            double f_z = seq_from.radius * Math.Sin(ele_from);

            double t_rd = seq_to.radius * Math.Cos(ele_to);
            double t_x = t_rd * Math.Sin(azi_to);
            double t_y = t_rd * Math.Cos(azi_to);
            double t_z = seq_to.radius * Math.Sin(ele_to);

            double d_x = (t_x - f_x) / divide_count;
            double d_y = (t_y - f_y) / divide_count;
            double d_z = (t_z - f_z) / divide_count;

            return new Vector3((float)d_x, (float)d_y, (float)d_z);
        }

        private Vector3 Polar2Ortho(PolarPosition seq_from)
        {
            double azi_from = seq_from.azimuth / 180.0 * Math.PI;
            double ele_from = seq_from.elevation / 180.0 * Math.PI;

            double f_rd = seq_from.radius * Math.Cos(ele_from);
            double f_x = f_rd * Math.Sin(azi_from);
            double f_y = f_rd * Math.Cos(azi_from);
            double f_z = seq_from.radius * Math.Sin(ele_from);

            return new Vector3((float)f_x, (float)f_y, (float)f_z);
        }
        private PolarPosition Ortho2Polar(Vector3 posi_o)
        {
            double xx = posi_o.X;
            double yy = posi_o.Y;
            double zz = posi_o.Z;
            double yz = yy * yy + zz * zz;
            PolarPosition d = new PolarPosition();
            d.radius = Math.Sqrt(xx * xx + yz);
            d.azimuth = Math.Atan2(xx, yy) / Math.PI * 180.0;
            d.elevation = Math.Atan2(zz, Math.Sqrt(yz)) / Math.PI * 180.0;

            return d;
        }

        private PolarPosition PolarDelta(PolarPosition p_from, PolarPosition p_to, int divide_count, Cycle cycle, double time_intvl)
        {
            PolarPosition d = new PolarPosition();
            d.radius = (p_to.radius - p_from.radius) / divide_count;
            d.azimuth = (p_to.azimuth - p_from.azimuth) / divide_count + cycle.h * time_intvl * 360.0;
            d.elevation = (p_to.elevation - p_from.elevation) / divide_count + cycle.v * time_intvl * 360.0;
            return d;
        }
    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Cycle
    {
        public double h { get; set; }
        public double v { get; set; }
    }

    public class File
    {
        public int id { get; set; }
        public int media_id { get; set; }
        public object wav_image { get; set; }
        public double amp { get; set; }

        public double delay_time { get; set; }
        public double start_time { get; set; }
        public double end_time { get; set; }
        public int track { get; set; }
        public bool selecting { get; set; }
        public bool selected { get; set; }
    }

    public class MediaList
    {
        public File file { get; set; }
        public string name { get; set; }
        public object signal { get; set; }
        public int sampl_freq { get; set; }
        public int word_count { get; set; }
        public object wavdata { get; set; }
        public object wav_image { get; set; }
        public bool selecting { get; set; }
        public bool selected { get; set; }
    }

    public class MicList
    {
        public int id { get; set; }
        public OrthogonalPosition orthogonal_position { get; set; }
    }

    public class Motion
    {
        public string state { get; set; }
        public PolarPosition polar_position { get; set; }
        public Cycle cycle { get; set; }
        public double duration { get; set; }
    }

    public class OrthogonalPosition
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }

    public class Part
    {
        public int id { get; set; }
        public string name { get; set; }
        public double amp { get; set; }
        public double start_time { get; set; }
        public double end_time { get; set; }
        public List<File> file { get; set; }
        public int track_count { get; set; }
        public List<Sequence> sequence { get; set; }
        public List<object> time_seq_posi { get; set; }
        public object time_seq_posi_image { get; set; }
        public int border_top { get; set; }
        public int border_bottom { get; set; }
    }

    public class PolarPosition
    {
        public double radius { get; set; }
        public double azimuth { get; set; }
        public double elevation { get; set; }
    }

    public class Root
    {
        public Project g_proj { get; set; }
    }

    public class Project
    {
        public string username { get; set; }
        public string name { get; set; }
        public double end_time { get; set; }
        public int sampl_freq { get; set; }
        public int frame_size { get; set; }
        public double time_intvl { get; set; }
        public int part_id { get; set; }
        public int file_id { get; set; }
        public int seq_id { get; set; }
        public List<Part> part { get; set; }
        public List<MicList> mic_list { get; set; }
        public List<MediaList> media_list { get; set; }
        public int border_bottom { get; set; }
    }

    public class SeqImage
    {
    }

    public class Sequence
    {
        public int id { get; set; }
        public double time { get; set; }
        public Motion motion { get; set; }
        public string comment { get; set; }
        public SeqImage seq_image { get; set; }
        public bool selecting { get; set; }
        public bool selected { get; set; }
    }
}
