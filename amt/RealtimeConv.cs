using System.IO;
using Asio_io;
using System.Windows.Forms;

namespace amt
{
    public class RealtimeConv
    {
        private Amt parent = null;
        private asio_io myasio = null;
        private bool rtconv_init_state = true;
        private int ch_cnt;
        private int conv_leng = 0;
        private int rtconv_inch;
        private int rtconv_outch;
        private int fftpnt;
        private int min_buf_size;
        private int fft_level = 0;
        private int fft_level_max;
        private float[] eq_data = null;

        public RealtimeConv(Amt Parent, int in_ch_cnt, int out_ch_cnt, int fft_lvl)
        {
            parent = Parent;
            myasio = parent.asio;
            rtconv_inch = in_ch_cnt;
            rtconv_outch = out_ch_cnt;
            ch_cnt = out_ch_cnt;
            int p_size = myasio.GetPreferredSize();
            fftpnt = p_size * 2;
            fft_level = fft_lvl;
            FFTLevel2ConvLeng();
            fft_level_max = MaxFFTLevel();
        }

        private void FFTLevel2ConvLeng()
        {
            conv_leng = 0;
            int fftpnt_1 = fftpnt;
            int max_fftpnt = 0;
            for (int i = 0; i < fft_level; i++)
            {
                max_fftpnt = fftpnt_1;
                conv_leng += fftpnt_1;
                fftpnt_1 *= 2;
            }
            InitEQdata();
            min_buf_size = conv_leng + max_fftpnt / 2;
        }

        public void SetEQdata(float[] data, double amp)
        {
            for (int i = 0; i < conv_leng; i++)
            {
                eq_data[i] = (float)(amp * (double)data[i]);
            }
        }

        public string GetFFTPoint()
        {
            return fftpnt.ToString();
        }

        public int GetConvLeng()
        {
            return conv_leng;
        }

        public string GetMinBufSize()
        {
            return min_buf_size.ToString();
        }

        public void InitEQdata()
        {
            eq_data = new float[conv_leng];
            for (int i = 1; i < conv_leng; i++) eq_data[i] = 0f;
            eq_data[0] = 1f;
        }

        public int SetFFTLevel(int fft_lvl)
        {
            fft_level = fft_lvl;
            if (fft_level <= 0)
            {
                fft_level = 1;
            }
            else if (fft_level > fft_level_max)
            {
                fft_level = fft_level_max;
            }

            FFTLevel2ConvLeng();
            return fft_level;
        }

        public int AddFFTLevel(int val)
        {
            SetFFTLevel(fft_level + val);
            return fft_level;
        }

        private int MaxFFTLevel()
        {
            int fftpnt_1 = fftpnt;
            int fft_level1 = 0;
            int min_bsize;
            int conv_length = 0;

            int leng = myasio.GetDataLength();
            do
            {
                conv_length += fftpnt_1;
                min_bsize = conv_length + fftpnt_1 / 2;
                fftpnt_1 *= 2;
                fft_level1++;
            }
            while (min_bsize <= leng);

            return fft_level1 - 1;
        }

        public void SetChannel(int in_ch_cnt, int out_ch_cnt)
        {
            rtconv_inch = in_ch_cnt;
            rtconv_outch = out_ch_cnt;
            rtconv_init_state = true;
        }

        public bool Init_f(SourcePath src_path, bool single_mode, bool is_inv, int udp_ch, ToolStripProgressBar toolStripProgressBar)
        {
            if (rtconv_init_state)
            {
                ChannelInfo.StreamType type = ChannelInfo.StreamType.wavread;
                if (udp_ch > 0) type = ChannelInfo.StreamType.wavread_add;
                for (int ch = 0; ch < rtconv_inch; ch++)
                {
                    parent.DataCreate(parent.ch_info_in[ch], type);
                }
                for (int ch = 0; ch < rtconv_outch; ch++)
                {
                    if (!parent.OutBufCreate(parent.ch_info_out[ch], ChannelInfo.StreamType.asioout))
                    {
                        return false;
                    }
                }
                if(udp_ch > 0)
                {
                    for (int ch = 0; ch < udp_ch; ch++)
                    {
                        parent.DataCreate(parent.ch_info_work[ch], ChannelInfo.StreamType.no_io);
                    }
                }
                bool check_ok = myasio.rtconv_init(fft_level, rtconv_inch, rtconv_outch, single_mode);
                if (!check_ok)
                {
                    parent.Cnsl_msg("Error: rtconv_init\n");
                    return false;
                }
            }

            //            parent.asio.spamp_gain_push(0x245);  // MUTE
            //myasio.rtconv_imp_updating(true);

            if (single_mode)
            {
                double p_scale = 100.0 / (double)rtconv_inch;
                for (int ich = 1; ich <= rtconv_inch; ich++)
                {
                    int p_val = (int)(p_scale * (double)ich);
                    toolStripProgressBar.Value = p_val;
                    string filename = src_path.filt_filename(ich, ich, false);
                    WavStream ws = new WavStream(parent);
                    bool check_ok = ws.WavHeaderCheck(filename);
                    if (!check_ok) { parent.Cnsl_msg("Error: WavHeaderCheck\n"); return false; }
                    int word_cnt = ws.WordCount();
                    check_ok = ws.WavRead1ch(filename, word_cnt);
                    if (check_ok)
                    {
                        for (int fft_lvl = 1; fft_lvl <= fft_level; fft_lvl++)
                        {
                            bool is_last = (ich == rtconv_inch) && (fft_lvl == fft_level);
                            ws.Rtconv_set(fft_lvl, ich, ich, word_cnt, is_last);
                        }
                    }
                    else
                    {
                        parent.Cnsl_msg("Error: WavRead1ch\n");
                        return false;
                    }
                }
            }
            else
            {
                double p_scale = 100.0 / (double)(rtconv_inch * rtconv_outch);
                for (int ich = 1; ich <= rtconv_inch; ich++)
                {
                    for (int och = 1; och <= rtconv_outch; och++)
                    {
                        int p_val = (int)(p_scale * (double)((ich-1)* rtconv_outch+och));
                        toolStripProgressBar.Value = p_val;
                        string filename = src_path.filt_filename(ich, och, is_inv);
                        if(is_inv)
                        {
                            FileInfo fi = new FileInfo(filename);
                            int word_cnt = (int)(fi.Length / 8);
                            float[] data = new float[word_cnt];
                            using (BinaryReader reader = new BinaryReader(FileProcess.Open(filename, FileMode.Open)))
                            {
                                for (int i = 0; i < word_cnt; i++)
                                {
                                    data[i] = (float)reader.ReadDouble();
                                }
                            }

                            for (int fft_lvl = 1; fft_lvl <= fft_level; fft_lvl++)
                            {
                                bool is_last = (ich == rtconv_inch) && (och == rtconv_outch) && (fft_lvl == fft_level);
                                unsafe
                                {
                                    fixed (float* dd = data)
                                    {
                                        parent.asio.rtconv_set(fft_lvl, dd, ich, och, word_cnt, is_last);
                                    }
                                }
                            }
                        }
                        else
                        {
                            WavStream ws = new WavStream(parent);
                            if (!ws.WavRead(filename, false)) return false;
                            int word_cnt = ws.WordCount();
                            for (int fft_lvl = 1; fft_lvl <= fft_level; fft_lvl++)
                            {
                                bool is_last = (ich == rtconv_inch) && (och == rtconv_outch) && (fft_lvl == fft_level);
                                ws.Rtconv_set(fft_lvl, ich, och, word_cnt, is_last);
                            }
                        }
                    }
                }
            }

            //            parent.asio.spamp_gain_push(parent.sp_amp_set.current_val);  // Current Gain val
            //myasio.rtconv_imp_updating(false);

            if (rtconv_init_state)
            {
                for (int ch = 1; ch <= rtconv_inch; ch++)
                {
                    parent.Created_signal(parent.ch_info_in[ch - 1]);
                }
                for (int ch = 1; ch <= rtconv_outch; ch++)
                {
                    parent.OutbufDataClear(parent.ch_info_out[ch - 1]);
                }
            }

            //if (test_mode)
            //{
            //    myasio.rtconv_test(rtconv_inch, 1);
            //}
            rtconv_init_state = false;

            //myasio.reset_error_cnt();
            return true;
        }

        public bool Init_eq(bool stream_in)
        {
            if (rtconv_init_state)
            {
                ChannelInfo.StreamType type;
                if (stream_in) type = ChannelInfo.StreamType.wavread;
                else type = ChannelInfo.StreamType.asioin;
                for (int ch = 0; ch < ch_cnt; ch++)
                {
                    parent.DataCreate(parent.ch_info_in[ch], type);
                }
                for (int ch = 0; ch < ch_cnt; ch++)
                {
                    if (!parent.OutBufCreate(parent.ch_info_out[ch], ChannelInfo.StreamType.asioout))
                    {
                        return false;
                    }
                }
                bool check_ok = myasio.rtconv_init(fft_level, ch_cnt, ch_cnt, true);
                if (!check_ok)
                {
                    parent.Cnsl_msg("Error: rtconv_init\n");
                    return false;
                }
            }

            //            parent.asio.spamp_gain_push(0x245);  // MUTE
            //myasio.rtconv_imp_updating(true);

            for (int ich = 1; ich <= ch_cnt; ich++)
            {
                for (int fft_lvl = 1; fft_lvl <= fft_level; fft_lvl++)
                {
                    bool is_last = (ich == ch_cnt) && (fft_lvl == fft_level);
                    unsafe
                    {
                        fixed (float* dd = eq_data)
                        {
                            myasio.rtconv_set(fft_lvl, dd, ich, ich, conv_leng, is_last);
                        }
                    }
                }
            }

            //            parent.asio.spamp_gain_push(parent.sp_amp_set.current_val);  // Current Gain val
            //myasio.rtconv_imp_updating(false);

            if (rtconv_init_state)
            {
                for (int ch = 1; ch <= ch_cnt; ch++)
                {
                    parent.Created_signal(parent.ch_info_in[ch - 1]);
                }
                for (int ch = 1; ch <= ch_cnt; ch++)
                {
                    parent.OutbufDataClear(parent.ch_info_out[ch - 1]);
                }
            }
            rtconv_init_state = false;

            //myasio.reset_error_cnt();
            return true;
        }
    }
}
