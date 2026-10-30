using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Collections;

namespace amt
{
    struct InverseSystem
    {
        public string inv_id;
        public string src_id;
        public string sfreq;
        public string date;
        public string organization;
        public string method;
        public string constants;
        public string server_url;
        public string directory;
        public string comment;
    }

    struct RecordedData
    {
        public string src_id;
        public string data_type;
        public string channel;
        public string sfreq;
        public string microphone;
        public string date;
        public string place;
        public string condition;
        public string member;
        public string organization;
        public string artist;
        public string album_title;
        public string song_title;
        public string server_url;
        public string directory;
        public string comment;
        public string reference_url;
        public string artwork;
    }

    struct ProcesedData
    {
        public string proc_id;
        public string data_type;
        public string src_id1;
        public string src_id2;
        public string sfreq;
        public string date;
        public string organization;
        public string server_url;
        public string directory;
        public string comment;
    }

    class AudioDataList
    {
        InverseSystem inverseSystem;
        RecordedData recordedData;
        ProcesedData processedData;
        string fileEncoding = "utf-8";

        const string Delim = ":";
        enum JSONReadStatus{NONE, ReadStart, MainEntry, EachEntry, Name, Value};
        JSONReadStatus curStatus;
        string curReadName;
        const string PROC_ID_PREFIX = "proc";
        private Amt parent = null;

        static class JSONTypeString{
            public const string entry = "entry";
            public const string inv_id = "inv_id";
            public const string src_id = "src_id";
            public const string proc_id = "proc_id";
            public const string data_type = "data_type";
            public const string src_id1 = "src_id1";
            public const string src_id2 = "src_id2";
            public const string sfreq = "sfreq";
            public const string method = "method";
            public const string constants = "constants";
            public const string date = "date";
            public const string organization = "organization";
            public const string server_url = "server_url";
            public const string directory = "directory";
            public const string comment = "comment";
            public const string channel = "channel";
            public const string microphone = "microphone";
            public const string place = "place";
            public const string condition = "condition";
            public const string member = "member";
            public const string artist = "artist";
            public const string album_title = "album_title";
            public const string song_title = "song_title";
            public const string reference_url = "reference_url";
            public const string artwork = "artwork";
        }

        ArrayList inverse_list;
        ArrayList recorded_list;
        ArrayList processed_list;
        private string src_dir = @"D:\cygwin\home\ise\Work\Crest\2015\2015.9\";
//        private string src_dir = @"C:\Users\ise\Documents\Work\Crest\2015\2015.9\";
        private string inverse_json_file = "BoSC_DataBase_InverseSystem.json";
        private string recorded_json_file = "BoSC_DataBase_RecordedData.json";
        private string processed_json_file = "BoSC_DataBase_ProcessedData.json";
        public enum Json_type { inverse, recorded, processed };

        public AudioDataList(Amt Parent)
        {
            // TODO: Complete member initialization
            parent = Parent;
            inverse_list = InitAudioList(src_dir + inverse_json_file, Json_type.inverse);
            recorded_list = InitAudioList(src_dir + recorded_json_file, Json_type.recorded);
            processed_list = InitAudioList(src_dir + processed_json_file, Json_type.processed);
        }

        private ArrayList InitAudioList(string json_file, Json_type jtype)
        {
            ArrayList json_list = null;
            if (File.Exists(json_file))
            {
                StreamReader sr = new StreamReader(json_file, Encoding.GetEncoding(fileEncoding));
                string responseBody = sr.ReadToEnd();
                json_list = JSONParse(responseBody, jtype);
                sr.Close();
                parent.Cnsl_msg("[SUCCESS] Read : " + json_file + "Data count is " + json_list.Count.ToString() + "\r\n");
            }
            else
            {
                parent.Cnsl_msg("[Error] File does not exist : " + json_file + "\r\n");
            }
            return json_list;
        }

        private ArrayList JSONParse(string responseBody, Json_type jtype)
        {
            int str_index = 0;
            curStatus = JSONReadStatus.NONE;
            curReadName = "";
            ArrayList json_list = new ArrayList();
            while (str_index < responseBody.Length)
            {
                char cur_c = responseBody[str_index];
                switch (cur_c)
                {
                    case '{':
                        if (curStatus == JSONReadStatus.NONE)
                        {
                            curStatus = JSONReadStatus.ReadStart;
                        }
                        else if (curStatus == JSONReadStatus.EachEntry)
                        {
                            curStatus = JSONReadStatus.Name;
                        }
                        str_index++;
                        break;
                    case '"':
                        str_index++;
                        int end_index = responseBody.IndexOf('"', str_index);
                        string data_str = responseBody.Substring(str_index,end_index-str_index);
                        str_index = end_index+1;
                        if(curStatus != JSONReadStatus.Value)
                        {
                            CheckName(data_str);
                        }
                        else
                        {
                            SetValue(data_str, jtype);
                        }
                        break;
                    case ':':
          
                        if (curStatus == JSONReadStatus.Name)
                        {
                            curStatus = JSONReadStatus.Value;
                        }
                        else if (curStatus == JSONReadStatus.MainEntry)
                        {
                            curStatus = JSONReadStatus.EachEntry;
                        }
                        else
                        {
                            curStatus = JSONReadStatus.Name;
                        }
                        str_index++;
                        break;
                    case '}':
                        if (curStatus == JSONReadStatus.Value)
                        {
                            curStatus = JSONReadStatus.EachEntry;
                            switch (jtype)
                            {
                                case Json_type.inverse:
                                    json_list.Add(inverseSystem);
                                    break;
                                case Json_type.recorded:
                                    json_list.Add(recordedData);
                                    break;
                                case Json_type.processed:
                                    json_list.Add(processedData);
                                    break;
                            }

                        }else if(curStatus == JSONReadStatus.MainEntry)
                        {
                            curStatus = JSONReadStatus.NONE;
                        }
                        str_index++;
                        break;
                    case ']':
                        if(curStatus ==JSONReadStatus.EachEntry){
                            curStatus = JSONReadStatus.MainEntry;
                        }
                        str_index++;
                        break;

                    case '[':
                        str_index++;
                        break;
                    case ',':
                        if (curStatus == JSONReadStatus.Value || curStatus == JSONReadStatus.Name)
                        {
                            curStatus = JSONReadStatus.EachEntry;
                        }
                        str_index++;
                        break;
                    default:
                        str_index++;
                        break;
                }
            }
            return json_list;
        }
        private void CheckName(string str)
        {
            if (str == JSONTypeString.entry && curStatus == JSONReadStatus.ReadStart)
            {
                curStatus = JSONReadStatus.MainEntry;
            }
            else
            {
                curStatus = JSONReadStatus.Name;
                curReadName = str;
            }
        }
        public void SetValue(string str, Json_type jtype)
        {
            switch (jtype)
            {
                case Json_type.inverse:
                    SetValue_inverse(str);
                    break;
                case Json_type.recorded:
                    SetValue_recorded(str);
                    break;
                case Json_type.processed:
                    SetValue_processed(str);
                    break;
            }
        }
        public void SetValue_inverse(string str)
        {
            switch (curReadName)
            {
                case JSONTypeString.inv_id:
                    inverseSystem.inv_id = str;
                    break;
                case JSONTypeString.src_id:
                    inverseSystem.src_id = str;
                    break;
                case JSONTypeString.sfreq:
                    inverseSystem.sfreq = str;
                    break;
                case JSONTypeString.date:
                    inverseSystem.date = str;
                    break;
                case JSONTypeString.organization:
                    inverseSystem.organization = str;
                    break;
                case JSONTypeString.method:
                    inverseSystem.method = str;
                    break;
                case JSONTypeString.constants:
                    inverseSystem.constants = str;
                    break;
                case JSONTypeString.server_url:
                    inverseSystem.server_url = str;
                    break;
                case JSONTypeString.directory:
                    inverseSystem.directory = str;
                    break;
                case JSONTypeString.comment:
                    inverseSystem.comment = str;
                    break;
                default:
                    break;
            }
        }
        public void SetValue_recorded(string str)
        {
            switch (curReadName)
            {
                case JSONTypeString.src_id:
                    recordedData.src_id = str;
                    break;
                case JSONTypeString.data_type:
                    recordedData.data_type = str;
                    break;
                case JSONTypeString.channel:
                    recordedData.channel = str;
                    break;
                case JSONTypeString.sfreq:
                    recordedData.sfreq = str;
                    break;
                case JSONTypeString.microphone:
                    recordedData.microphone = str;
                    break;
                case JSONTypeString.date:
                    recordedData.date = str;
                    break;
                case JSONTypeString.place:
                    recordedData.place = str;
                    break;
                case JSONTypeString.condition:
                    recordedData.condition = str;
                    break;
                case JSONTypeString.member:
                    recordedData.member = str;
                    break;
                case JSONTypeString.organization:
                    recordedData.organization = str;
                    break;
                case JSONTypeString.artist:
                    recordedData.artist = str;
                    break;
                case JSONTypeString.album_title:
                    recordedData.album_title = str;
                    break;
                case JSONTypeString.song_title:
                    recordedData.song_title = str;
                    break;
                case JSONTypeString.server_url:
                    recordedData.server_url = str;
                    break;
                case JSONTypeString.directory:
                    recordedData.directory = str;
                    break;
                case JSONTypeString.comment:
                    recordedData.comment = str;
                    break;
                case JSONTypeString.reference_url:
                    recordedData.reference_url = str;
                    break;
                case JSONTypeString.artwork:
                    recordedData.artwork = str;
                    break;
                default:
                    break;
            }
        }
        public void SetValue_processed(string str)
        {
            switch (curReadName)
            {
                case JSONTypeString.proc_id:
                    processedData.proc_id = str;
                    break;
                case JSONTypeString.data_type:
                    processedData.data_type = str;
                    break;
                case JSONTypeString.src_id1:
                    processedData.src_id1 = str;
                    break;
                case JSONTypeString.src_id2:
                    processedData.src_id2 = str;
                    break;
                case JSONTypeString.sfreq:
                    processedData.sfreq = str;
                    break;
                case JSONTypeString.date:
                    processedData.date = str;
                    break;
                case JSONTypeString.organization:
                    processedData.organization = str;
                    break;
                case JSONTypeString.server_url:
                    processedData.server_url = str;
                    break;
                case JSONTypeString.directory:
                    processedData.directory = str;
                    break;
                case JSONTypeString.comment:
                    processedData.comment = str;
                    break;
                default:
                    break;
            }
        }

        public string GetDirectory(string dataIndex)
        {
            IEnumerator myEnumerator = processed_list.GetEnumerator();
            string directory = "";
            while (myEnumerator.MoveNext())
            {
                processedData = (ProcesedData)myEnumerator.Current;
                if( processedData.proc_id == dataIndex )
                {
                    directory = processedData.directory;
                    break;
                }
            }
            return directory;
        }
        public bool Open(string index, int chNum)
        {
            string directory = GetDirectory(index);
            string sFilePath = "";
            for (int i = 0; i < chNum; i++)
            {
                try
                {
                    sFilePath = String.Format(directory, i + 1);
                    parent.Set_stream(i, sFilePath );
                }
                catch (System.Exception ex)
                {
                    if (sFilePath != "")
                    {
                        Debug.WriteLine(ex.Message + ": " + sFilePath);
                    }
                    else
                    {
                        Debug.WriteLine(ex.Message);
                    }
                    return false;
                }
            }

            return true; // SUCCESS
        }

        public ArrayList GetProcessedData(string data_type)
        {
            ArrayList json_list = new ArrayList();
            IEnumerator myEnumerator = processed_list.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                processedData = (ProcesedData)myEnumerator.Current;
                if (processedData.data_type == data_type)
                {
                    json_list.Add(processedData);
                }
            }
            return json_list;
        }

        public string[] GetRecordedData(string src_id)
        {
            ArrayList json_list = new ArrayList();
            IEnumerator myEnumerator = recorded_list.GetEnumerator();
            string[] ret_str = new string[3];
            while (myEnumerator.MoveNext())
            {
                recordedData = (RecordedData)myEnumerator.Current;
                if (recordedData.src_id == src_id)
                {
                    ret_str[0] = recordedData.artist;
                    ret_str[1] = recordedData.album_title;
                    ret_str[2] = recordedData.song_title;
                    break;
                }
            }
            return ret_str;
        }
    }
}
