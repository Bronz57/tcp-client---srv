// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;

    Console.WriteLine("Creazione socket");
    const int PORT = 9000, BACKLOG = 128;
    IPEndPoint endPoint = new(IPAddress.Loopback, PORT);
    Socket socket = new(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
    socket.Bind(endPoint);
    socket.Listen(BACKLOG);

    Console.WriteLine("Sono in ascolto...");

    Socket clientSocket = socket.Accept();
    Console.WriteLine("Client connesso!");

    

    //dati client
    var endPointClient = clientSocket.RemoteEndPoint as IPEndPoint;
    string ipClient = endPointClient.Address.ToString();
    string portClient = endPointClient.Port.ToString();

    Console.WriteLine($"Client: {ipClient}:{portClient}");
    
    //dati server
    var endPointServer = socket.LocalEndPoint as IPEndPoint;
    string ipServer = endPointServer.Address.ToString();
    string portServer = endPointServer.Port.ToString();

    Console.WriteLine($"Server: {ipServer}:{portServer}");


    //cmd telnet invio poi telnet 127.0.0.1 9000 ed invio
     while(true){
        byte [] buffer = new byte[1024];
        int bufferDim = clientSocket.Receive(buffer);
        string msgClnt = System.Text.Encoding.UTF8.GetString(buffer);
        Console.WriteLine($"Messaggio del client: {msgClnt}");

        if(bufferDim==0){
            clientSocket.Close();
            break;
        }

        clientSocket.Send(buffer);
    }

    socket.Close();
        
    
    
    