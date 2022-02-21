

namespace FrameworklessWebApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncServer server = new AsyncServer();
            server.RunServer();
        }
    }
}