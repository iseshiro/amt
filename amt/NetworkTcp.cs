using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace amt
{
    class NetworkTcp
    {
        private Amt parent = null;
        private bool tcp_listening = false;
        private Thread tcp_listen_thread = null;
        private bool NetAlive = false;
        private TcpListener tcpL = null;
        private TcpClient tcpServer;
        private const int CONTROL_PORT = 2001;
        private NetworkStream server_ns = null;
        public delegate void ExecCmdDelegate(string cmd_text);
        ExecCmdDelegate ExecCmd = null;
        private System.Windows.Forms.TextBox msgBox;
        private bool show_recv_msg = true;

        public NetworkTcp(Amt Parent, System.Windows.Forms.TextBox msgbox, ExecCmdDelegate execCmd)
        {
            parent = Parent;
            msgBox = msgbox;
            ExecCmd = execCmd;
        }

        public void SetShowRecvMsg(bool Show_recv_msg)
        {
            show_recv_msg = Show_recv_msg;
        }

        public void StartServer(bool Show_recv_msg)
        {
            show_recv_msg = Show_recv_msg;
            if (!tcp_listening)
            {
                try
                {
                    tcpL = new TcpListener(IPAddress.Any, CONTROL_PORT);
                    tcpL.Start();

                    // threadを起動
                    NetAlive = true;
                    if (tcp_listen_thread != null && tcp_listen_thread.IsAlive)
                    {
                        msgBox.AppendText("tcp_listen_thread.IsAlive == true --> thread Aborting\r\n");
                        tcp_listen_thread.Abort();
                    }
                    tcp_listen_thread = new Thread(new ThreadStart(TcpListen))
                    {
                        Name = "TcpListen"
                    };
                    tcp_listen_thread.Start();
                    tcp_listening = true;
                    msgBox.AppendText("Start Server\r\n");
                }
                catch (Exception exception)
                {
                    msgBox.AppendText(exception.Message + "\r\n");
                }
            }
        }

        public void StopServer()
        {
            msgBox.AppendText("Stop Server\r\n");
            NetAlive = false;
            if (tcp_listening)
            {
                if (tcp_listen_thread != null)
                {
                    if (tcp_listen_thread.IsAlive)
                    {
                        if (server_ns != null)
                        {
                            server_ns.Close();
                            server_ns = null;
                        }
                        tcp_listen_thread.Abort();
                        msgBox.AppendText("Abort tcp_listen_thread\r\n");
                    }
                    tcp_listen_thread = null;
                }
            }
            tcp_listening = false;
        }

        private void TcpListen()
        {
            try
            {
                tcpServer = tcpL.AcceptTcpClient();
                string a1 = ((System.Net.IPEndPoint)tcpServer.Client.RemoteEndPoint).Address.ToString();
                string p1 = ((System.Net.IPEndPoint)tcpServer.Client.RemoteEndPoint).Port.ToString();
                msgBox.AppendText("[TCP Network] Connected : " + a1 + "," + p1 + "\r\n");

                server_ns = tcpServer.GetStream();
                msgBox.AppendText("Client Connected\r\n");
                while (NetAlive)
                {
                    string msg;
                    try
                    {
                        msg = ReadFrameText(server_ns);
                        if (msg == null) break; // 切断
                    }
                    catch (Exception ex)
                    {
                        msgBox.AppendText("Read error: " + ex.Message + "\r\n");
                        break;
                    }

                    if (show_recv_msg)
                    {
                        msgBox.AppendText("Received: " + msg + "\r\n");
                    }
                    ExecCmd(msg);
                }

                msgBox.AppendText("Exit tcp listener\r\n");
            }
            catch
            {
                msgBox.AppendText("[TCP Network] Break\r\n");
            }
            server_ns.Close();
            server_ns = null;
            tcpServer.Close();
            tcpL.Stop();
        }
        public void AmtNetWrite(string send_msg)
        {
            //クライアントにデータを送信する
            if (!tcp_listening) return;
            //文字列をByte型配列に変換
            byte[] sendBytes = Encoding.UTF8.GetBytes(send_msg);
            //データを送信する
            server_ns.Write(sendBytes, 0, sendBytes.Length);
        }
        private static bool ReadExact(NetworkStream ns, byte[] buf, int offset, int count)
        {
            int total = 0;
            while (total < count)
            {
                int n = ns.Read(buf, offset + total, count - total);
                if (n == 0) return false; // 切断
                total += n;
            }
            return true;
        }

        private string ReadFrameText(NetworkStream ns)
        {
            byte[] header = new byte[4];
            if (!ReadExact(ns, header, 0, 4)) return null;

            int len = BitConverter.ToInt32(header, 0);

            // 防御：異常サイズを弾く（DoS/バグ対策）
            if (len <= 0 || len > 1024 * 1024) throw new InvalidOperationException($"Invalid len={len}");

            byte[] body = new byte[len];
            if (!ReadExact(ns, body, 0, len)) return null;

            return Encoding.UTF8.GetString(body);
        }

    }
}
