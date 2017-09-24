namespace NewUserHandler
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class Tracker
    {
        private readonly string localDbCon;

        public Tracker()
        {
            this.localDbCon = ConfigurationManager.ConnectionStrings["LocalDb"].ConnectionString;

            SqlDependency.Stop(this.localDbCon);
            SqlDependency.Start(this.localDbCon);
        }

        public void TrackMessages()
        {

            using (SqlConnection connection = new SqlConnection(this.localDbCon))
            {
                SqlCommand command = new SqlCommand("SELECT Name, Surname FROM dbo.Users", connection);

                SqlDependency dependency = new SqlDependency(command);

                dependency.OnChange += this.OnDependencyChange;

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Process the DataReader.
                }
            }
        }

        private void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            Console.WriteLine(e.Info);
            Console.WriteLine(e.Source);
            Console.WriteLine(e.Type);

            this.TrackMessages();
        }

        ~Tracker()
        {
            SqlDependency.Stop(this.localDbCon);
        }
    }
}
