using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Dapper;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;
using EtnaSoft.Dal.Services.Converter;

namespace EtnaSoft.Dal.Services.Database
{
    public class DatabaseCreationService : IDisposable
    {
        private IDbTransactions _transactions;
        private string creationString = $"CREATE DATABASE {EtnaSettings.DbName} ON PRIMARY " +
        $"(NAME = {EtnaSettings.DbName}, " +
        $"FILENAME = '{Directory.GetCurrentDirectory() + "\\" + EtnaSettings.DbName + "data.mdf"}', " +
        "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
        $"LOG ON (NAME ={EtnaSettings.DbName}_Log, " +
        $"FILENAME = '{Directory.GetCurrentDirectory() + "\\" + EtnaSettings.DbName + "data.ldf"}', " +
        "SIZE = 1MB, " +
        "MAXSIZE = 5MB, " +
        "FILEGROWTH = 10%)";
        private const string InitializeLabels = "INSERT INTO dbo.Labels (Caption, Color) VALUES (@Caption, @Color)";
        private const string InitializeRooms = "INSERT INTO dbo.Rooms (RoomNumber) Values (@RoomNumber)";
        private const string InitializeTypes = "INSERT INTO dbo.StayTypes (Title, Price) VALUES (@Title, @Price)";
        private List<CustomLabel> _labelList;
        private List<Room> _rooms;
        private List<StayType> _stayTypes;
        public DatabaseCreationService()
        {
           
            LabelList();
            RoomList();
            StayTypeList();
        }

        private void StayTypeList()
        {
            _stayTypes = new List<StayType>
            {
                new StayType(){Title = "PP_Standard", Price = 2800M},
                new StayType(){Title = "PP_Superior", Price = 3200M}
            };

        }
        private void LabelList()
        {
            _labelList = new List<CustomLabel>
            {
                new CustomLabel() {Caption = "Prijavljena", Color = "#489C9B"},

                new CustomLabel() {Caption = "Na dolasku", Color = "#FFA500"}
            };
        }

        private void RoomList()
        {
            _rooms = new List<Room>()
            {
                new Room(){RoomNumber = "8"},
                new Room(){RoomNumber = "9"},
                new Room(){RoomNumber = "10"},
                new Room(){RoomNumber = "11"},
                new Room(){RoomNumber = "12"},
                new Room(){RoomNumber = "101"},
                new Room(){RoomNumber = "102"},
                new Room(){RoomNumber = "103"},
                new Room(){RoomNumber = "104"},
                new Room(){RoomNumber = "105"},
                new Room(){RoomNumber = "106"},
                new Room(){RoomNumber = "107"},
                new Room(){RoomNumber = "108"},
                new Room(){RoomNumber = "109"},
                new Room(){RoomNumber = "110"},
                new Room(){RoomNumber = "111"},
                new Room(){RoomNumber = "112"},
                new Room(){RoomNumber = "113"},
                new Room(){RoomNumber = "114"},
                new Room(){RoomNumber = "115"}

            };
        }
        public bool CreateDatabase(string dbName, string directoryName, string fileName, FileConverter fc)
        {
            bool isSuccess = false;
            int dbCreationResult;
            using (_transactions = new DbTransactions())
            {


                try
                {
                    string dirName = directoryName;
                    string path = Directory.GetCurrentDirectory();
                    string scriptDir = path + dirName;
                    string dbFileName = fileName;
                    bool doesFolderExist = Directory.Exists(scriptDir);
                    if (!doesFolderExist)
                    {
                        throw new Exception("Folder sa scriptama za kreiranje baze ne postoji");
                    }
                    bool doesFileExist = File.Exists(scriptDir + dbFileName);
                    if (!doesFileExist)
                    {
                        throw new Exception("Fajl sa scriptama ne postoji");
                    }
                    // var dirName = Path.GetFileName()
                    string script = fc.Load(scriptDir + fileName);
                    string properScript = string.Empty;
                    
                    using (IDbConnection conn = new SqlConnection(EtnaSettings.ConnectionString))
                    {
                        dbCreationResult = conn.Execute(creationString, new { }, commandType: CommandType.Text);
                    }
                    
                    
                    _transactions.StartTransaction();
                    var sqlQueries = script.Split(new[] {"GO"}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var sqlQuery in sqlQueries)
                    {
                        _transactions.SaveDataTransaction(sqlQuery, new { });
                    }
                   
                    
                    
                    foreach (var label in _labelList)
                    {
                        _transactions.SaveDataTransaction(InitializeLabels, new {Caption = label.Caption, Color = label.Color});
                    }

                    foreach (var room in _rooms)
                    {
                        _transactions.SaveDataTransaction(InitializeRooms, new {RoomNumber = room.RoomNumber});
                    }

                    foreach (var stayType in _stayTypes)
                    {
                        _transactions.SaveDataTransaction(InitializeTypes,
                            new {Title = stayType.Title, Price = stayType.Price});
                    }
                    _transactions.CommitTransaction();
                    isSuccess = true;


                }
                catch
                {
                    _transactions.RollBackTransaction();
                    throw;
                }
            }
            return isSuccess;
        }

        public void Dispose()
        {
            _labelList.Clear();
            _labelList = null;
            _rooms.Clear();
            _rooms = null;
            _transactions?.Dispose();
        }
    }
}
