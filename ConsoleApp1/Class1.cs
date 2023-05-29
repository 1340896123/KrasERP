using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

class Class1
{
   public static void Start()  
    {
        // 设置本地监听端口
        int localPort = 8888;
        // 设置目标服务器IP和端口
        string remoteHost = "chat.forefront.ai";
        int remotePort = 443;

        // 加载证书
        X509Certificate2 serverCertificate = new X509Certificate2("server.pfx", "111111");

        // 创建本地监听Socket
        TcpListener listener = new TcpListener(IPAddress.Any, localPort);
        listener.Start();

        Console.WriteLine("Listening on port " + localPort);

        while (true)
        {
            // 接受本地客户端连接
            TcpClient client = listener.AcceptTcpClient();

            // 连接目标服务器
            TcpClient server = new TcpClient(remoteHost, remotePort);

            // 创建SSL流
            SslStream clientStream = new SslStream(client.GetStream(), false);
            SslStream serverStream = new SslStream(server.GetStream(), false);

            // 进行SSL握手
            clientStream.AuthenticateAsServer(serverCertificate, false, SslProtocols.Tls, true);
            serverStream.AuthenticateAsClient(remoteHost);

            // 开启两个线程，分别进行数据转发
            Task.Run(() => ForwardData(clientStream, serverStream));
            Task.Run(() => ForwardData(serverStream, clientStream));
        }
    }

    static void ForwardData(SslStream source, SslStream destination)
    {
        byte[] buffer = new byte[1024];
        int bytesRead;

        try
        {
            while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
            {
                destination.Write(buffer, 0, bytesRead);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            source.Close();
            destination.Close();
        }
    }
}
