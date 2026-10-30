using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;
using System.Net;
using System.Security.Policy;
using Newtonsoft.Json.Linq;

namespace amt
{
	[StructLayout(LayoutKind.Explicit)]
	struct Byte2int16
	{
		[FieldOffset(0)] public byte b0;
		[FieldOffset(1)] public byte b1;

		[FieldOffset(0)] public Int16 s0;
	}
	
	[StructLayout(LayoutKind.Explicit)]
    struct Byte2int32
	{
		[FieldOffset(0)] public byte b0;
		[FieldOffset(1)] public byte b1;
		[FieldOffset(2)] public byte b2;
		[FieldOffset(3)] public byte b3;

		[FieldOffset(0)] public Int32 i0;
	}

	[StructLayout(LayoutKind.Explicit)]
	struct B2f
	{
		[FieldOffset(0)] public byte b0;
		[FieldOffset(1)] public byte b1;
		[FieldOffset(2)] public byte b2;
		[FieldOffset(3)] public byte b3;

		[FieldOffset(0)] public float f0;
	}

	/// <summary>
	/// WavStream ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class WavStream
	{
//		private MemoryStream ms;
		private float[] data;
        private float[] data1;
        private float[] buf1;

		private string RIFF_ID;
		private Int32  File_Size;
		private string WAVE_ID;
		private string CHNK_ID1;
        private Int32 CHNK_Size1;
		private short wFormatTag;
        public short nChannels;
        private Int32 nSamplePerSec;
        private Int32 nAvgBytesPerSec;
		private short nBlockAlign;
		private short wBitsPerSample;
		private string CHNK_ID2;
        private Int32 CHNK_Size2;

		private int word_cnt;
		private Amt parent;
        private double amp16_1;
        private double amp16;
        private double amp32_1;
        private double amp32;

        public wav_error WavError = wav_error.no_error;
        public enum Wav_format { int8, int16, int24, int32, float32, not_wav };

        public enum wav_error { no_error, sample_freq_error, format_error };
        public WavStream(Amt Parent)
		{
			parent = Parent;
            amp16_1 = parent.asio.get_amp16_1();
            amp16 = parent.asio.get_amp16();
            amp32_1 = parent.asio.get_amp32_1();
            amp32 = parent.asio.get_amp32();
        }
        
        public Wav_format WavFormat()
        {
            Wav_format ret = Wav_format.not_wav;
            if (wFormatTag == 1 && wBitsPerSample == 16) ret = Wav_format.int16;
            else if (wFormatTag == 1 && wBitsPerSample == 8) ret = Wav_format.int8;
            else if (wFormatTag == 1 && wBitsPerSample == 24) ret = Wav_format.int24;
            else if (wFormatTag == 1 && wBitsPerSample == 32) ret = Wav_format.int32;
            else if (wFormatTag == 3 && wBitsPerSample == 32) ret = Wav_format.float32;
            return ret;
        }

        public FileStream WavOpen(string name)
		{
			FileStream fs = FileProcess.OpenRead(name);
            if (!ReadHeader(fs))
            {
                throw new FileLoadException("at WavStream: WavOpen()");
            }
			parent.Cnsl_msg("wav file name: "+name+"\r\n");
			parent.Cnsl_msg("nChannels: "+nChannels.ToString()+"\r\n");
			parent.Cnsl_msg("nSamplePerSec: "+nSamplePerSec.ToString()+"\r\n");

			parent.Cnsl_msg("Word count: "+word_cnt.ToString()+"\r\n");

			return fs;
        }

        public void TextRead(string name, bool isHex)
        {
            StreamReader sr = new StreamReader(name);
            string line;
            ArrayList al = new ArrayList();
            int cnt = 0;
            int val;

            while ((line = sr.ReadLine()) != null)
            {
                if( isHex )
                {
                    val = System.Convert.ToInt32(line, 16);
                }
                else
                {
                    val = System.Convert.ToInt32(line);
                }
                al.Add(val);
                cnt++;
            }
            word_cnt = cnt;
            data = new float[cnt];
            double amp16_m = 1.0 / 32768.5;
            double amp16_p = 1.0 / 32767.5;
            for (int i = 0; i < cnt; i++)
            {
                double amp16_a;
                int iii = (int)al[i];
                if( iii > 0 )
                {
                    amp16_a = amp16_p;
                }
                else
                {
                    amp16_a = amp16_m;
                }
                data[i] = (float)((double)iii * amp16_a);
            }
        }

        public void AddData(float[] outbuf, int dl_pnt, int st_pnt, int ed_pnt)
        {
            int jj = dl_pnt;
            for (int ii = st_pnt; ii < ed_pnt; ii++)
            {
                outbuf[jj++] += data[ii];
            }
        }

        private bool ReadHeader(FileStream fs)
        {
            WavError = wav_error.format_error;
            byte[] wh = new byte[44];

            RIFF_ID = Get_char4(fs);
            if (RIFF_ID != "RIFF") return false;
            File_Size = Get_int32(fs);
            WAVE_ID = Get_char4(fs);
            if (WAVE_ID != "WAVE") return false;

            CHNK_ID1 = Get_char4(fs);
            CHNK_Size1 = Get_int32(fs);

            while (CHNK_ID1 != "fmt ")
            {
                fs.Seek(CHNK_Size1, SeekOrigin.Current);
                CHNK_ID1 = Get_char4(fs);
                CHNK_Size1 = Get_int32(fs);
            }

            wFormatTag = Get_short(fs);
            nChannels = Get_short(fs);
            nSamplePerSec = Get_int32(fs);
            nAvgBytesPerSec = Get_int32(fs);
            nBlockAlign = Get_short(fs);
            wBitsPerSample = Get_short(fs);
            if (CHNK_Size1 > 16)
            {
                fs.Seek(CHNK_Size1 - 16, SeekOrigin.Current);
            }

            CHNK_ID2 = Get_char4(fs);
            CHNK_Size2 = Get_int32(fs);

            while (CHNK_ID2 != "data")
            {
                fs.Seek(CHNK_Size2, SeekOrigin.Current);
                CHNK_ID2 = Get_char4(fs);
                CHNK_Size2 = Get_int32(fs);
            }

            return Check_data((int)fs.Length);
        }

        private void WriteHeader(FileStream fs)
        {
            Put_char4(fs, RIFF_ID);
            Put_int32(fs, File_Size);
            Put_char4(fs, WAVE_ID);
            Put_char4(fs, CHNK_ID1);
            Put_int32(fs, CHNK_Size1);
            Put_short(fs, wFormatTag);
            Put_short(fs, nChannels);
            Put_int32(fs, nSamplePerSec);
            Put_int32(fs, nAvgBytesPerSec);
            Put_short(fs, nBlockAlign);
            Put_short(fs, wBitsPerSample);
            Put_char4(fs, CHNK_ID2);
            Put_int32(fs, CHNK_Size2);
        }

        public bool WavRead(string name, bool force_read)
        {
            FileStream fs = FileProcess.OpenRead(name);
            if (!ReadHeader(fs) && !force_read)
            {
                return false;
            }

            Get_data_1ch(fs, word_cnt);

            fs.Close();
            return true;
        }

        private void Get_data_1ch(FileStream fs, int length)
        {
            int byte_size = length * nBlockAlign;

            switch (WavFormat())
            {
                case Wav_format.int16:
                    Get_data16(fs, byte_size);
                    break;
                case Wav_format.int8:
                    Get_data8(fs, byte_size);
                    break;
                case Wav_format.int24:
                    Get_data24(fs, byte_size);
                    break;
                case Wav_format.int32:
                    Get_data32(fs, byte_size);
                    break;
                case Wav_format.float32:
                    Get_data32f(fs, byte_size);
                    break;
                default:
                    throw new FileLoadException("at WavStream: WavRead()");
            }
        }

        public bool WavRead1ch(string name, int length)
        {
            FileStream fs = FileProcess.OpenRead(name);
            parent.Cnsl_msg("wav file name: " + name + "\r\n");
            if (!ReadHeader(fs))
            {
                return false;
            }

            if (nSamplePerSec != parent.sample_freq)
            {
                parent.Cnsl_msg("Error: nSamplePerSec: " + nSamplePerSec.ToString() + "\r\n");
                return false;
            }
            parent.Cnsl_msg("Read length: " + length.ToString() + "\r\n");
            Get_data_1ch(fs, length);
            fs.Close();

            return true;
        }

        public bool WavReadInv(string name, int length, int sp_ch, int mic_ch)
        {
            FileStream fs = FileProcess.OpenRead(name);
            parent.Cnsl_msg("wav file name: " + name + "\r\n");
            if (!ReadHeader(fs))
            {
                return false;
            }

            if (nSamplePerSec != parent.sample_freq)
            {
                parent.Cnsl_msg("Error: nSamplePerSec: " + nSamplePerSec.ToString() + "\r\n");
                return false;
            }
            parent.Cnsl_msg("Read length: " + length.ToString() + "\r\n");
            Get_data_1ch(fs, length);
            unsafe
            {
                fixed (float* dd = data)
                {
                    parent.asio.makeinv_dataset(length, sp_ch, mic_ch, dd);
                }
            }
            fs.Close();

            return true;
        }

        public bool WavHeaderCheck(string name)
        {
            byte[] wh = new byte[44];
            bool ok = false;
            if(FileProcess.Exists(name))
            {
                using(FileStream fs = FileProcess.OpenRead(name))
                {
                    int fs_leng = (int)fs.Length;
                    ok = ReadHeader(fs);
                }
            }
            return ok;
        }

        //public void WavHeaderRepair(string name)
        //{
        //    FileStream fs = new FileStream(name, FileMode.Open, FileAccess.ReadWrite );

        //    nSamplePerSec = parent.sample_freq;
        //    SetWaveParameters(1, ((int)fs.Length - 44)/2, false);
        //    WriteHeader(fs);
        //    fs.Close();
        //}

        public FileStream WavHeaderWrite(string name, int leng, bool is_float)
        {
            byte[] wh = new byte[44];
            FileStream fs = new FileStream(name, FileMode.Create);

            word_cnt = leng;
            nSamplePerSec = parent.sample_freq;
            SetWaveParameters(1, word_cnt, is_float);
            WriteHeader(fs);

            return fs;
        }

        public void WavWrite(FileStream fs, ChannelInfo cinfo, bool is_float)
		{
			byte[] wh=new byte[44];
//			FileStream fs = FileProcess.OpenRead(name);

			int d_size=parent.asio.GetDataLength();
			nSamplePerSec=parent.sample_freq;
            SetWaveParameters(1, d_size, is_float);
            WriteHeader(fs);

            data = new float[d_size];
			unsafe
			{
                if (parent.repeat_mode_cb.Checked)
                {
                    int point0 = parent.repeat_stop_point;
                    int leng0 = d_size - point0;
                    fixed (float* dd = data)
                    {
                        parent.asio.get_data((int)cinfo.type, cinfo.btn_no, dd, leng0, point0);
                    }
                    if (point0 > 0)
                    {
                        fixed (float* dd = &data[leng0])
                        {
                            parent.asio.get_data((int)cinfo.type, cinfo.btn_no, dd, point0, 0);
                        }
                    }
                }
                else
                {
                    fixed (float* dd = data)
                    {
                        parent.asio.get_data((int)cinfo.type, cinfo.btn_no, dd, d_size, 0);
                    }
                }
			}

            if (is_float)
            {
                Put_float_array(fs);
            }
            else
            {
                Put_short_array(fs);
            }

//			parent.cnsl_msg("wav file name: "+name+"\r\n");
			parent.Cnsl_msg("nChannels: "+nChannels.ToString()+"\r\n");
			parent.Cnsl_msg("nSamplePerSec: "+nSamplePerSec.ToString()+"\r\n");
			parent.Cnsl_msg("Data size: "+d_size.ToString()+"\r\n");

			fs.Close();
		}

        public void WavFloatWrite(FileStream fs, float[] data_in, int d_size)
		{
			byte[] wh=new byte[44];
//			FileStream fs = FileProcess.OpenRead(name);

			nSamplePerSec=parent.sample_freq;
            SetWaveParameters(1, d_size, true);
            WriteHeader(fs);

            data = data_in;
            Put_float_array(fs);

//			parent.cnsl_msg("wav file name: "+name+"\r\n");
			parent.Cnsl_msg("nChannels: "+nChannels.ToString()+"\r\n");
			parent.Cnsl_msg("nSamplePerSec: "+nSamplePerSec.ToString()+"\r\n");
			parent.Cnsl_msg("Data size: "+d_size.ToString()+"\r\n");

			fs.Close();
		}

        public void WavWriteText(FileStream fs, ChannelInfo cinfo, bool isHex)
        {
            int d_size = parent.asio.GetDataLength();
            //string dtype = parent.asio.DataTypeString();

            data = new float[d_size];
            unsafe
            {
                if (parent.repeat_mode_cb.Checked)
                {
                    int point0 = parent.asio.getADpoint();
                    int leng0 = d_size - point0;
                    fixed (float* dd = data)
                    {
                        parent.asio.get_data((int)cinfo.type, cinfo.btn_no, dd, leng0, point0);
                    }
                    fixed (float* dd = &data[leng0])
                    {
                        parent.asio.get_data((int)cinfo.type, cinfo.btn_no, dd, point0, 0);
                    }
                }
                else
                {
                    fixed (float* dd = data)
                    {
                        parent.asio.get_data((int)cinfo.type, cinfo.btn_no, dd, d_size, 0);
                    }
                }
            }
            StreamWriter ss = new StreamWriter(fs);
            String sss;
            for (int i = 0; i < d_size; i++)
            {
                if( isHex )
                {
                    sss = Convert.ToString((int)(data[i] * amp16), 16).ToUpper();
                    sss = sss.PadLeft(4, '0') + " ";
                    sss = sss.Substring(sss.Length - 5);
                }
                else
                {
                    int ii = (int)(data[i] * amp16);
                    sss = ii.ToString();
                }

                ss.Write(sss + "\r\n");
            }

            fs.Close();
        }

		private bool Check_data(int real_file_size)
		{
            if (CHNK_ID1 != "fmt ") return false;
            if (CHNK_ID2 != "data") return false;

            if (File_Size + 8 != real_file_size) return false;
            if (wFormatTag != 1 && wFormatTag != 3) return false;
            if (nSamplePerSec != 44100 && nSamplePerSec != 48000 && nSamplePerSec != 88200 && nSamplePerSec != 96000 && nSamplePerSec != 192000)
            {
                WavError = wav_error.sample_freq_error;
                return false;
            }
            if (nSamplePerSec != parent.sample_freq)
            {
                int sfreq = parent.sample_freq;
                parent.Cnsl_msg("--> nSamplePerSec Error: WAV header:" + nSamplePerSec.ToString() + " not equal to audio interface" + sfreq.ToString() + "\r\n");
                WavError = wav_error.sample_freq_error;
                return false;
            }
            int mm = wBitsPerSample / 8;
            if (nAvgBytesPerSec != nSamplePerSec * mm * nChannels) return false;
            if (nBlockAlign != mm * nChannels) return false;

            word_cnt = (int)CHNK_Size2 / nBlockAlign;
            WavError = wav_error.no_error;

            return true;
		}

        public void Rtconv_set(int fft_level, int in_ch, int out_ch, int word_cnt_max, bool is_last)
        {
            unsafe
            {
                fixed (float* dd = data)
                {
                    parent.asio.rtconv_set(fft_level, dd, in_ch, out_ch, word_cnt_max, is_last);
                }
            }
        }

        public void Get_data_3d(ref float[] ddd, int och, int fir_tap, ArrayList reload_order)
        {
            int point = (och-1)*fir_tap;
            for (int i = 0; i<fir_tap; i++ )
            {
                ddd[point+i] = data[(int)reload_order[i]];
            }
        }

        public void Rtconv_fpga_set(int fpga_ch, int out_ch, string reload_order_file)
        {
            StreamReader sr = new StreamReader(reload_order_file);
            string line;
            ArrayList reload_order = null;
            reload_order = new ArrayList();
            int cnt = 0;

            while ((line = sr.ReadLine()) != null)
            {
                string[] strs = line.Split(' ');
                int index = int.Parse(strs[2]);
                int coef_no = int.Parse(strs[5]);
                if (cnt == index)
                {
                    reload_order.Add(coef_no);
                    cnt++;
                }
            }
            data1 = new float[cnt];
            for (int i = 0; i < cnt;i++ )
            {
                data1[i] = data[(int)reload_order[i]];
             }
            unsafe
            {
                fixed (float* dd = data1)
                {
                    parent.asio.rtconv_fpga_set(fpga_ch, dd, out_ch, cnt);
                }
            }
        }

        public void Load_data(ChannelInfo cinfo)
        {
            int selected_ch = 1;
            if (nChannels > 1)
            {
                SelectChannel selch = new SelectChannel(nChannels);
                if (selch.ShowDialog(parent) == DialogResult.OK)
                {
                    selected_ch = selch.selected_ch;
                }
                selch.Dispose();
            }
            unsafe
            {
                fixed (float* dd = data)
                {
                    parent.asio.load_data((int)cinfo.type, cinfo.btn_no, dd, word_cnt, nChannels, selected_ch);
                }
            }
        }

        public double Verify_data(ChannelInfo cinfo, int offset)
        {
            double retval = 0.0;
            unsafe
            {
                fixed (float* dd = data)
                {
                    retval = parent.asio.verify_data((int)cinfo.type, cinfo.btn_no, dd, offset, word_cnt);
                }
            }
            return retval;
        }

        public void Load_data1(ChannelInfo cinfo, int point, int length)
        {
            unsafe
            {
                fixed (float* dd = data)
                {
                    parent.asio.load_data1((int)cinfo.type, cinfo.btn_no, dd, point, length);
                }
            }
        }

		private string Get_char4(FileStream fsg)
		{
			byte[] wh=new byte[4];
			int read = fsg.Read (wh, 0, 4);
			return Encoding.ASCII.GetString(wh);
		}
		private Int32 Get_int32(FileStream fsg)
		{
			byte[] wh=new byte[4];
			Byte2int32 aa=new Byte2int32();
			int read = fsg.Read (wh, 0, 4);
			aa.b0=wh[0];
			aa.b1=wh[1];
			aa.b2=wh[2];
			aa.b3=wh[3];
			return aa.i0;
		}
		private float Get_float(FileStream fsg)
		{
			byte[] wh=new byte[4];
			B2f aa=new B2f();
			int read = fsg.Read (wh, 0, 4);
			aa.b0=wh[0];
			aa.b1=wh[1];
			aa.b2=wh[2];
			aa.b3=wh[3];
			return aa.f0;
		}

		private short Get_short(FileStream fsg)
		{
			byte[] wh=new byte[2];
			Byte2int16 aa=new Byte2int16();
			int read = fsg.Read (wh, 0, 2);
			aa.b0=wh[0];
			aa.b1=wh[1];
			return aa.s0;
		}
        public void Get_data16(FileStream fsg, long Size)
        {
            int data_count = (int)Size / 2;
            data = new float[data_count];
            byte[] wh = new byte[2];
            Byte2int16 aa = new Byte2int16();
            for (int i = 0; i < data_count; i++)
            {
                if (fsg.Read(wh, 0, 2) != 2)
                {
                    throw new FileLoadException("at WavStream: get_data16");
                }
                aa.b0 = wh[0];
                aa.b1 = wh[1];
                data[i] = (float)(aa.s0 * amp16_1);
            }
        }
        private void Get_data24(FileStream fsg, long Size)
        {
            int data_count = (int)Size / 3;
            data = new float[data_count];
            byte[] wh = new byte[3];
            Byte2int32 aa = new Byte2int32();
            float temp;
            for (int i = 0; i < data_count; i++)
            {
                if (fsg.Read(wh, 0, 3) != 3)
                {
                    throw new FileLoadException("at WavStream: get_data24");
                }
                aa.b0 = 0;
                aa.b1 = wh[0];
                aa.b2 = wh[1];
                aa.b3 = wh[2];
                temp = (float)(aa.i0 * amp32_1);
                data[i] = (float)(aa.i0 * amp32_1);
            }
        }
        private void Get_data32(FileStream fsg, long Size)
        {
            int data_count = (int)Size / 4;
            data = new float[data_count];
            byte[] wh = new byte[4];
            Byte2int32 aa = new Byte2int32();
            float temp;
            parent.toolStripProgressBar1.Maximum = data_count;
            for (int i = 0; i < data_count; i++)
            {
                if (fsg.Read(wh, 0, 4) != 4)
                {
                    throw new FileLoadException("at WavStream: get_data32");
                }
                aa.b0 = wh[0];
                aa.b1 = wh[1];
                aa.b2 = wh[2];
                aa.b3 = wh[3];
                temp = (float)(aa.i0 * amp32_1);
                data[i] = (float)(aa.i0 * amp32_1);
                parent.toolStripProgressBar1.Value = i;   // Another thread should be invoked
            }
        }
        private void Get_data32f(FileStream fsg, long Size)
        {
            int data_count = (int)Size / 4;
            data = new float[data_count];
            byte[] wh = new byte[4];
            B2f aa = new B2f();
            for (int i = 0; i < data_count; i++)
            {
                if (fsg.Read(wh, 0, 4) != 4)
                {
                    throw new FileLoadException("at WavStream: get_data32f");
                }
                aa.b0 = wh[0];
                aa.b1 = wh[1];
                aa.b2 = wh[2];
                aa.b3 = wh[3];
                if (float.IsNaN(aa.f0))
                {
                    data[i] = 0.0f;
                }
                else
                {
                    data[i] = aa.f0;
                }
            }
        }
        private void Get_data16S(FileStream fsg, long Size, int Ch)
		{
			int data_count=(int)Size/4;
            data = new float[data_count];
			byte[] wh=new byte[4];
			Byte2int16 aa=new Byte2int16();
			if(Ch==1)
			{
				for(int i=0;i<data_count;i++)
				{
					if(fsg.Read (wh, 0, 4)!=4)
					{
						throw new FileLoadException("at WavStream: get_data16S");
					}
					aa.b0=wh[0];
					aa.b1=wh[1];
                    data[i] = (float)(aa.s0 * amp16_1);
				}
			}
			else if(Ch==2)
			{
				for(int i=0;i<data_count;i++)
				{
					if(fsg.Read (wh, 0, 4)!=4)
					{
						throw new FileLoadException("at WavStream: get_data16S");
					}
					aa.b0=wh[2];
					aa.b1=wh[3];
                    data[i] = (float)(aa.s0 * amp16_1);
                }
			}
			else if(Ch==3)
			{
				Byte2int16 aa1=new Byte2int16();
				for(int i=0;i<data_count;i++)
				{
					if(fsg.Read (wh, 0, 4)!=4)
					{
						throw new FileLoadException("at WavStream: get_data16S");
					}
					aa.b0=wh[0];
					aa.b1=wh[1];
					aa1.b0=wh[2];
					aa1.b1=wh[3];
                    data[i] = (float)(((aa.s0 + aa1.s0) / 2) * amp16_1);
				}
			}
		}
		private void Get_data8(FileStream fsg, long Size)
		{
			int data_count=(int)Size;
            data = new float[data_count];
			byte[] wh=new byte[data_count];
			if(fsg.Read (wh, 0, data_count)!=data_count)
			{
				throw new FileLoadException("at WavStream: get_data8");
			}
			for(int i=0;i<data_count;i++)
			{
                data[i] = (float)((short)(wh[i] * 256) * amp16_1);
			}
			wBitsPerSample=16;
		}

		private void Put_char4(Stream fsg, string str)
		{
            byte[] wh = Encoding.ASCII.GetBytes(str); 
			fsg.Write (wh, 0, 4);
		}
		private void Put_int32(Stream fsg, int ll)
		{
            Byte2int32 aa = new Byte2int32()
            {
                i0 = ll
            };
            byte[] wh=new byte[4];
			wh[0]=aa.b0;
			wh[1]=aa.b1;
			wh[2]=aa.b2;
			wh[3]=aa.b3;
			fsg.Write (wh, 0, 4);
		}

		private void Put_short(Stream fsg, short ss)
		{
            Byte2int16 aa = new Byte2int16()
            {
                s0 = ss
            };
            byte[] wh = new byte[2];
            wh[0]=aa.b0;
			wh[1]=aa.b1;
			fsg.Write (wh, 0, 2);
		}
		private void Put_short_array(Stream fsg)
		{
			byte[] wh=new byte[2];
			Byte2int16 aa=new Byte2int16();
			for(int i=0;i<data.Length;i++)
			{
				aa.s0=(short)(data[i] * amp16);
				wh[0]=aa.b0;
				wh[1]=aa.b1;
				fsg.Write (wh, 0, 2);
			}
		}
        private void Put_float_array(Stream fsg)
        {
            byte[] wh = new byte[4];
            B2f aa = new B2f();
            for (int i = 0; i < data.Length; i++)
            {
                aa.f0 = data[i];
                wh[0] = aa.b0;
                wh[1] = aa.b1;
                wh[2] = aa.b2;
                wh[3] = aa.b3;
                fsg.Write(wh, 0, 4);
            }
        }
        public void Wav2chto1ch(string lr_wav, string l_wav, string r_wav)
        {
            if (l_wav != "" && r_wav == "")
            {
                Wav2chto1ch_s(lr_wav, l_wav, 1);
            }
            else if (l_wav == "" && r_wav != "")
            {
                Wav2chto1ch_s(lr_wav, r_wav, 2);
            }
            else if (l_wav != "" && r_wav != "")
            {
                Wav2chto1chx2(lr_wav, l_wav, r_wav);
            }
        }
        public void Wav2chto1ch_s(string in_wav, string out_wav, int ch_selected)
		{
            FileStream infs = FileProcess.OpenRead(in_wav);
            bool ok = ReadHeader(infs);

			parent.Cnsl_msg("Input wav file name: "+in_wav+"\r\n");
			parent.Cnsl_msg("nChannels: "+nChannels.ToString()+"\r\n");
			parent.Cnsl_msg("nSamplePerSec: "+nSamplePerSec.ToString()+"\r\n");

            if (!ok || nChannels != 2)
			{
				infs.Close();
				parent.Cnsl_msg("Please select 2ch wav data.\r\n");
				return;
			}
			parent.Cnsl_msg("Word count: "+word_cnt.ToString()+"\r\n");

			FileStream outfs = FileProcess.OpenWrite(out_wav);  

			parent.Cnsl_msg("Output wav file name: "+out_wav+"\r\n");
			int data_count=(int)CHNK_Size2/4;
            SetWaveParameters(1, data_count, false);
            WriteHeader(outfs);
			
			byte[] wh=new byte[4];
//			byte2int16 aa=new byte2int16();
			if(ch_selected==1)
			{
				for(int i=0;i<data_count;i++)
				{
					if(infs.Read (wh, 0, 4)!=4)
					{
						throw new FileLoadException("at WavStream: Wav2chto1ch");
					}
					outfs.Write (wh, 0, 2);
				}
			}
			else if(ch_selected==2)
			{
				for(int i=0;i<data_count;i++)
				{
					if(infs.Read (wh, 0, 4)!=4)
					{
						throw new FileLoadException("at WavStream: Wav2chto1ch");
					}
					wh[0]=wh[2];
					wh[1]=wh[3];
					outfs.Write (wh, 0, 2);
				}
			}
			infs.Close();
			outfs.Flush();
			outfs.Close();
		}
        public void Wav2chto1chx2(string lr_wav, string l_wav, string r_wav)
        {
            FileStream infs = FileProcess.OpenRead(lr_wav);
            bool ok = ReadHeader(infs);

            parent.Cnsl_msg("Input wav file name: " + lr_wav + "\r\n");
            parent.Cnsl_msg("nChannels: " + nChannels.ToString() + "\r\n");
            parent.Cnsl_msg("nSamplePerSec: " + nSamplePerSec.ToString() + "\r\n");

            if (!ok || nChannels != 2)
            {
                infs.Close();
                parent.Cnsl_msg("Please select 2ch wav data.\r\n");
                return;
            }
            parent.Cnsl_msg("Word count: " + word_cnt.ToString() + "\r\n");

            FileStream out_l_fs = FileProcess.OpenWrite(l_wav);
            FileStream out_r_fs = FileProcess.OpenWrite(r_wav);

            parent.Cnsl_msg("Output wav file name: " + l_wav + "\r\n");
            SetWaveParameters(1, word_cnt, false);
            WriteHeader(out_l_fs);
            WriteHeader(out_r_fs);

            byte[] wh = new byte[4];
            //			byte2int16 aa=new byte2int16();
            for (int i = 0; i < word_cnt; i++)
            {
                if (infs.Read(wh, 0, 4) != 4)
                {
                    throw new FileLoadException("at WavStream: Wav2chto1ch");
                }
                out_l_fs.Write(wh, 0, 2);
                wh[0] = wh[2];
                wh[1] = wh[3];
                out_r_fs.Write(wh, 0, 2);
            }
            infs.Close();
            out_l_fs.Flush();
            out_l_fs.Close();
            out_r_fs.Flush();
            out_r_fs.Close();
        }
        public int Channel()
        {
            return nChannels;
        }
        public int SampleFrequency()
        {
            return nSamplePerSec;
        }
        public int WordCount()
        {
            return word_cnt;
        }
        private void SetWaveParameters(short channel, int w_count, bool is_float)
        {
            int mm = 2;

            wFormatTag = 1;
            if (is_float)
            {
                mm = 4;
                wFormatTag = 3;
            }
            nChannels = channel;
            nAvgBytesPerSec = nSamplePerSec * mm * channel;
            nBlockAlign = (short)(mm * channel);
            wBitsPerSample = (short)(mm * 8);
            CHNK_Size2 = w_count * nBlockAlign;
            File_Size = CHNK_Size2 + 36;

            RIFF_ID = "RIFF";
            WAVE_ID = "WAVE";
            CHNK_ID1 = "fmt ";
            CHNK_Size1 = 16;
            CHNK_ID2 = "data";
        }
       public void Wav1chto2ch(string l_wav, string r_wav, string lr_wav)
        {
            FileStream l_fs = this.WavOpen(l_wav);
            int data_count_l = (int)CHNK_Size2 / 2;
            FileStream r_fs = this.WavOpen(r_wav);
            int data_count_r = (int)CHNK_Size2 / 2;

            int data_count = (data_count_l < data_count_r ? data_count_l : data_count_r);

            parent.Cnsl_msg("Output wav file name: " + lr_wav + "\r\n");
            parent.Cnsl_msg("nChannels: " + nChannels.ToString() + "\r\n");
            parent.Cnsl_msg("nSamplePerSec: " + nSamplePerSec.ToString() + "\r\n");

            FileStream out_lr_fs = FileProcess.OpenWrite(lr_wav);
            PreOutWav2ch(out_lr_fs, data_count);
            OutWav2ch(out_lr_fs, l_fs, r_fs, data_count);
            out_lr_fs.Flush();
            out_lr_fs.Close();
            l_fs.Close();
            r_fs.Close();
        }
        public void OutWav2ch(FileStream outfs, FileStream in_l_fs, FileStream in_r_fs, int data_count)
		{
            byte[] wh = new byte[4];
            byte[] wh1 = new byte[2];
            for (int cnt = 0; cnt < data_count; cnt++)
            {
                if (in_l_fs.Read(wh, 0, 2) != 2)
                {
                    throw new FileLoadException("at OutWav2ch: Lch");
                }
                if (in_r_fs.Read(wh1, 0, 2) != 2)
                {
                    throw new FileLoadException("at OutWav2ch: Rch");
                }
                wh[2] = wh1[0];
                wh[3] = wh1[1];
                outfs.Write(wh, 0, 4);
            }
        }
        public void PreOutWav2ch(FileStream outfs, int word_count)
		{
            SetWaveParameters(2, word_cnt, false);
            WriteHeader(outfs);
		}
		public void OutWav1ch(FileStream outfs, ChannelInfo cinfo, int frame_size)
		{
			Byte2int16 a1=new Byte2int16();
			byte[] wh=new byte[4];
			if(cinfo != null)
			{
				unsafe
				{
					fixed(float *dd=buf1)
					{
                        parent.asio.get_data((int)cinfo.type, cinfo.btn_no, dd, frame_size * 2, 0);
					}
				}
			}
			else
			{
				for(int i=0;i<frame_size;i++) buf1[i]=0;
			}
			for(int i=0;i<frame_size;i++)
			{
				a1.s0 = (short)((data1[i]+buf1[i]) * amp16);
				wh[0]=a1.b0;
				wh[1]=a1.b1;
				outfs.Write (wh, 0, 2);

				data1[i]=buf1[frame_size+i];
			}
		}
		public void PreOutWav1ch(FileStream outfs, int frame_size, int frame_count)
		{
            SetWaveParameters(1, frame_size * (frame_count + 1), false);
            WriteHeader(outfs);

            data1 = new float[frame_size];
            buf1 = new float[frame_size * 2];
		}
	}
}

