namespace NewUserHandler
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    class Program
    {
        static void Main(string[] args)
        {

            Tracker tracker = new Tracker();
            tracker.TrackMessages();

            Console.WriteLine("Çıkmak için q ya basınız");

            string input = null;

            do
            {
                input = Console.ReadLine();
            }
            while (input != "q");
        }
    }
}
