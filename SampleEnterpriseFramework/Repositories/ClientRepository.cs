using Microsoft.Data.SqlClient;
using SampleEnterpriseFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEnterpriseFramework.Repositories
{
    public class ClientRepository
    {
        private readonly string connectionSting = "Data Source=HAICHANNGUYEN\\BI2425;Initial Catalog=winformdb;Integrated Security=True;Trust Server Certificate=True";

        public List<Client> GetClients()
        {
            var clients = new List<Client>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionSting))
                {
                    connection.Open();

                    string sql = "SELECT * FROM clients ORDER BY id DESC";
                    using SqlCommand command = new SqlCommand(sql, connection);
                    {
                        using SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Client client = new Client();
                            client.id = reader.GetInt32(0);
                            client.firstName = reader.GetString(1);
                            client.lastName = reader.GetString(2);
                            client.email = reader.GetString(3);
                            client.address = reader.GetString(4);
                            client.phone = reader.GetString(5);
                            client.createdAt = reader.GetDateTime(6).ToString();

                            clients.Add(client);
                        }
                    }

                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

            return clients;
        }


        public Client? GetClientById(int id) {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionSting))
                {
                    connection.Open();

                    string sql = "SELECT * FROM clients WHERE id=@id";
                    using SqlCommand command = new SqlCommand(sql, connection);
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            Client client = new Client();
                            client.id = reader.GetInt32(0);
                            client.firstName = reader.GetString(1);
                            client.lastName = reader.GetString(2);
                            client.email = reader.GetString(3);
                            client.phone = reader.GetString(4);
                            client.address = reader.GetString(5);
                            client.createdAt = reader.GetDateTime(6).ToString();

                            return client;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            return null;
        }


        public void CreateClient(Client client) 
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionSting))
                {
                    connection.Open();

                    string sql = "INSERT INTO clients " +
                        "(firstname, lastname, email, address, phone) VALUES " +
                        "(@firstname, @lastname, @email, @address, @phone);";
                    using SqlCommand command = new SqlCommand(sql, connection);
                    {
                        command.Parameters.AddWithValue("@firstname", client.firstName);
                        command.Parameters.AddWithValue("@lastname", client.lastName);
                        command.Parameters.AddWithValue("@email", client.email);
                        command.Parameters.AddWithValue("@phone", client.phone);
                        command.Parameters.AddWithValue("@address", client.address);

                        
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        public void UpdateClient(Client client)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionSting))
                {
                    connection.Open();

                    string sql = "UPDATE clients " +
                        "SET firstname=@firstname, lastname=@lastname, " +
                        "email=@email, address=@address, phone=@phone " +
                        "WHERE id=@id";
                    using SqlCommand command = new SqlCommand(sql, connection);
                    {
                        command.Parameters.AddWithValue("@firstname", client.firstName);
                        command.Parameters.AddWithValue("@lastname", client.lastName);
                        command.Parameters.AddWithValue("@email", client.email);
                        command.Parameters.AddWithValue("@phone", client.phone);
                        command.Parameters.AddWithValue("@address", client.address);
                        command.Parameters.AddWithValue("@id", client.id);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        public void DeleteClient(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionSting))
                {
                    connection.Open();

                    string sql = "DELETE FROM clients WHERE id=@id";
                    using SqlCommand command = new SqlCommand(sql, connection);
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using SqlDataReader reader = command.ExecuteReader();

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
}
