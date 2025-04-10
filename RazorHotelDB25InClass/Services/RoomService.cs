using Microsoft.Data.SqlClient;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;
using System.Data;

namespace RazorHotelDB25InClass.Services
{
    public class RoomService : Secret, IRoomService
    {
        private string connectionString = Secret.ConnectionString;
        private string roomSql = "Select Room_No, Hotel_No, Types, Price from Room where Hotel_No = @HotelNo";
        private string insertSql = "Insert into Room Values(@RoomNo, @HotelNo, @Types, @Price)";
        private string deleteSql = "Delete from Room where Hotel_No = @HotelNo and Room_No = @RoomNo";
        private string updateSql = "Update Room set Types = @Types, Price = @Price, Room_No = @RoomNo, Hotel_No = @HotelNo where Room_No = @RoomNo and Hotel_No = @HotelNo";

        public async Task<List<Room>> GetAllRoomAsync(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Room> rooms = new List<Room>();
                try
                {
                    SqlCommand command = new SqlCommand(roomSql, connection);
                    command.Parameters.AddWithValue("@HotelNo", hotelNr);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync()) // reads from data not from console
                    {
                        int roomNr = reader.GetInt32("Room_No");
                        char types = reader.GetString("Types")[0];
                        double price = reader.GetDouble("Price");
                        Room room = new Room(roomNr, types, price, hotelNr);
                        rooms.Add(room);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    throw ex;
                }
                finally { }
                return rooms;
            }
        }

        public async Task<Room?> GetRoomFromIdAsync(int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Room? room = null;
                try
                {
                    SqlCommand command = new SqlCommand(roomSql + " and Room_No = @RoomNo", connection);
                    command.Parameters.AddWithValue("@HotelNo", hotelNr);
                    command.Parameters.AddWithValue("@RoomNo", roomNr);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.Read()) // reads from data not from console
                    {
                        int rNr = reader.GetInt32("Room_No");
                        int hNr = reader.GetInt32("Hotel_No");
                        string types = reader.GetString("Types");
                        double price = reader.GetDouble("Price");
                        room = new Room(rNr, types[0], price, hNr);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    throw ex;
                }
                finally { }
                return room;
            }
        }

        public async Task<bool> CreateRoomAsync(int hotelNr, Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@RoomNo", room.RoomNr);
                    command.Parameters.AddWithValue("@HotelNo", room.HotelNr);
                    command.Parameters.AddWithValue("@Types", room.Types);
                    command.Parameters.AddWithValue("@Price", room.Pris);
                    await command.Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    throw ex;
                }
                finally { }
                return true;
            }
        }

        public async Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(updateSql, connection);
                    command.Parameters.AddWithValue("@RoomNo", room.RoomNr);
                    command.Parameters.AddWithValue("@HotelNo", room.HotelNr);
                    command.Parameters.AddWithValue("@Types", room.Types);
                    command.Parameters.AddWithValue("@Price", room.Pris);
                    await command.Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    throw ex;
                }
                finally { }
                return true;
            }
        }

        public async Task<Room?> DeleteRoomAsync(int roomNr, int hotelNr)
        {
            Room? room = GetRoomFromIdAsync(roomNr, hotelNr).Result;
            if (room == null) { return null; }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@RoomNo", roomNr);
                    command.Parameters.AddWithValue("@HotelNo", hotelNr);
                    await connection.OpenAsync();
                    int noOfRows = await command.ExecuteNonQueryAsync();
                    if (noOfRows == 0) { return null; }
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    throw ex;
                }
                finally { }
                return room;
            }
        }
    }
}