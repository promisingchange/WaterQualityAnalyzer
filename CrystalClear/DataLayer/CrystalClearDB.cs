using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace CrystalClear.DL
{
    using SQLite;

    using CrystalClear.DL.Models;

    public class CrystalClearDB
    {
        private string dbPath = "";

        public static void InitializeDatabase()
        {
            var dbPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\crystalclear.db";

            if (!File.Exists(dbPath))
            {
                // Database file does not exist.
                // Create and Initialize the database.
                CreateDatabase(dbPath);
                Seed(dbPath);
            }
            else
            {
                // Database file exists.
                // Update the database, if any.
                UpdateData(dbPath);
            }
        }
        private static void CreateDatabase(string pathToDB)
        {
            using (var db = new SQLite.SQLiteConnection(pathToDB))
            {
                db.CreateTable<EnvironmentalStandard>();
                db.CreateTable<OxygenSolubility>();
                db.CreateTable<OxygenDO>();
                db.CreateTable<BaseIndex>();
                db.CreateTable<Reservoir>();
                db.CreateTable<Record>();
            }
        }
        private static void Seed(string pathToDB)
        {
            var sqlPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\predefineddata.sql";
            if (File.Exists(sqlPath))
            {
                // Predefined Data exists.
                // Read it and Save predefined data.
                var commands = File.ReadAllText(sqlPath);
                var statements = commands.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                using (var db = new SQLite.SQLiteConnection(pathToDB))
                {
                    try
                    {

                        foreach (var statement in statements)
                        {
                            //Console.WriteLine(statement);
                            db.Execute(statement);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception: " + ex.Message);
                    }
                }
            }
            else
            {
                // No Predefined Data exists.
                // Generate base data and Store it.
                var environmentalStandards = CrystalClearDBUtil.GenerateBaseDataForEnvironmentalStandard();
                var oxygenSolubilities = CrystalClearDBUtil.GenerateBaseDataForOxygenSolubility();
                var oxygenDOs = CrystalClearDBUtil.GenerateBaseDataForOxygenDO();
                var indices = CrystalClearDBUtil.GenerateBaseDataForBaseIndex();

                try
                {
                    using (var db = new SQLite.SQLiteConnection(pathToDB))
                    {
                        db.InsertAll(environmentalStandards);
                        db.InsertAll(oxygenSolubilities);
                        db.InsertAll(oxygenDOs);
                        db.InsertAll(indices);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }
        }

        private static void UpdateData(string pathToDB)
        {
            var sqlPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\updateddata.sql";
            if (File.Exists(sqlPath))
            {
                // Updated Data exists.
                // Clean existing base data first.
                CleanBaseData(pathToDB);

                // Update the database with the provided data.
                try
                {
                    var commands = File.ReadAllText(sqlPath);
                    var statements = commands.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    using (var db = new SQLite.SQLiteConnection(pathToDB))
                    {
                        foreach (var statement in statements)
                        {
                            db.Execute(statement);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }
        }
        private static void CleanBaseData(string pathToDB)
        {
            try
            {
                using (var db = new SQLite.SQLiteConnection(pathToDB))
                {
                    db.Execute("DELETE FROM EnvironmentalStandard");
                    db.Execute("DELETE FROM OxygenSolubility");
                    db.Execute("DELETE FROM OxygenDO");
                    db.Execute("DELETE FROM Index");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
        public CrystalClearDB()
        {

        }

        public static CrystalClearDB GetInstance()
        {
            var db = new CrystalClearDB();

            var dbPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\crystalclear.db";
            // Check if the database file exists.
            if (File.Exists(dbPath))
            {
                // Database file exists.
                db.dbPath = dbPath;

                return db;
            }

            // Database does not exist, although it's been created and initialized when the app started.
            // Something went wrong. It might be deleted or whatever.
            return null;
        }

        public List<EnvironmentalStandard> GetEnvironmentalStandards()
        {
            var list = new List<EnvironmentalStandard>();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<EnvironmentalStandard>("SELECT * FROM EnvironmentalStandard ORDER BY Level");
            }

            return list;
        }
        public EnvironmentalStandard GetAverageEnvironmentalStandard()
        {
            var standard = new EnvironmentalStandard();

            try
            {
                using (var db = new SQLite.SQLiteConnection(dbPath))
                {
                    var list = db.Query<EnvironmentalStandard>("SELECT * FROM EnvironmentalStandard WHERE Level=3");
                    if (list.Count > 0)
                    {
                        standard = list[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetAverageEnvironmentalStandard: " + ex.Message);
            }

            return standard;
        }
        public List<OxygenSolubility> GetOxygenSolubilities()
        {
            var list = new List<OxygenSolubility>();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<OxygenSolubility>("SELECT * FROM OxygenSolubility ORDER BY Id");
            }

            return list;
        }
        public List<OxygenDO> GetOxygenDOs()
        {
            var list = new List<OxygenDO>();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<OxygenDO>("SELECT * FROM OxygenDO ORDER BY Id");
            }

            return list;
        }
        public OxygenDO GetOxygenDOForTemperature(double Temperature)
        {
            var oxygenDO = new OxygenDO();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                var list = db.Query<OxygenDO>("SELECT * FROM OxygenDO WHERE Temperature = ?", new object[] { Temperature });
                if (list.Count > 0)
                {
                    oxygenDO = list[0];
                }
            }

            return oxygenDO;
        }
        public List<BaseIndex> GetIndividualIndicesFor10Celsius()
        {
            var list = new List<BaseIndex>();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<BaseIndex>("SELECT * FROM BaseIndex WHERE Category='II10' ORDER BY Level");
            }

            return list;
        }
        public List<BaseIndex> GetIndividualIndicesFor20Celsius()
        {
            var list = new List<BaseIndex>();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<BaseIndex>("SELECT * FROM BaseIndex WHERE Category='II20' ORDER BY Level");
            }

            return list;
        }
        public List<BaseIndex> GetIndividualIndicesFor30Celsius()
        {
            var list = new List<BaseIndex>();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<BaseIndex>("SELECT * FROM BaseIndex WHERE Category='II30' ORDER BY Level");
            }

            return list;
        }
        public List<BaseIndex> GetIndividualIndicesForTemperature(double Temperature)
        {
            var category = "II" + Temperature.ToString("F0");

            var list = new List<BaseIndex>();
            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<BaseIndex>("SELECT * FROM BaseIndex WHERE Category = ? ORDER BY Level", new object[] { category });
            }

            return list;
        }
        public List<BaseIndex> GetWeightCoefficientsFor10Celsius()
        {
            var list = new List<BaseIndex>();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<BaseIndex>("SELECT * FROM BaseIndex WHERE Category='WK10' ORDER BY Level");
            }

            return list;
        }
        public List<BaseIndex> GetWeightCoefficientsFor20Celsius()
        {
            var list = new List<BaseIndex>();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<BaseIndex>("SELECT * FROM BaseIndex WHERE Category='WK20' ORDER BY Level");
            }

            return list;
        }
        public List<BaseIndex> GetWeightCoefficientsFor30Celsius()
        {
            var list = new List<BaseIndex>();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<BaseIndex>("SELECT * FROM BaseIndex WHERE Category='WK30' ORDER BY Level");
            }

            return list;
        }
        public List<BaseIndex> GetWeightCoefficientsForTemperature(double Temperature)
        {
            var category = "WK" + Temperature.ToString("F0");

            var list = new List<BaseIndex>();
            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<BaseIndex>("SELECT * FROM BaseIndex WHERE Category = ? ORDER BY Level", new object[] { category });
            }

            return list;
        }
        public List<BaseIndex> GetWeightIndicesFor10Celsius()
        {
            var list = new List<BaseIndex>();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<BaseIndex>("SELECT * FROM BaseIndex WHERE Category='WI10' ORDER BY Level");
            }

            return list;
        }
        public List<BaseIndex> GetWeightIndicesFor20Celsius()
        {
            var list = new List<BaseIndex>();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<BaseIndex>("SELECT * FROM BaseIndex WHERE Category='WI20' ORDER BY Level");
            }

            return list;
        }
        public List<BaseIndex> GetWeightIndicesFor30Celsius()
        {
            var list = new List<BaseIndex>();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<BaseIndex>("SELECT * FROM BaseIndex WHERE Category='WI30' ORDER BY Level");
            }

            return list;
        }
        public List<BaseIndex> GetWeightIndicesForTemperature(double Temperature)
        {
            var category = "WI" + Temperature.ToString("F0");

            var list = new List<BaseIndex>();
            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<BaseIndex>("SELECT * FROM BaseIndex WHERE Category = ? ORDER BY Level", new object[] { category });
            }

            return list;
        }
        public List<Reservoir> GetAllReservoirs()
        {
            var list = new List<Reservoir>();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<Reservoir>("SELECT * FROM Reservoir WHERE Deleted=false ORDER BY Id");
            }

            return list;
        }
        public int AddReservoir(string newName)
        {
            int newId = -1;
            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                newId = db.Insert(new Reservoir { Name = newName });
                Console.WriteLine("new Id: " + newId.ToString());
            }
            return newId;
        }
        public Reservoir GetReservoirById(int Id)
        {
            var reservoir = new Reservoir();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                var list = db.Query<Reservoir>("SELECT * FROM Reservoir WHERE Id = " + Id.ToString());
                if (list.Count > 0)
                {
                    reservoir = list[0];
                }
                else
                {
                    reservoir = null;
                }
            }

            return reservoir;
        }
        public Reservoir GetLastReservoir()
        {
            var reservoir = new Reservoir();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                var list = db.Query<Reservoir>("SELECT * FROM Reservoir WHERE Deleted=false Order By Id Desc Limit 1");
                if (list.Count > 0)
                {
                    reservoir = list[0];
                }
                else
                {
                    reservoir = null;
                }
            }

            return reservoir;
        }
        public int UpdateReservoir(Reservoir reservoir)
        {
            int rowAffected = 0;
            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                rowAffected = db.Update(reservoir);
                Console.WriteLine("Row affected: " + rowAffected.ToString());
            }
            return rowAffected;
        }
        public int DeleteReservoir(Reservoir reservoir)
        {
            int rowAffected = 0;
            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                reservoir.Deleted = true;
                rowAffected = db.Update(reservoir);
                Console.WriteLine("Row affected: " + rowAffected.ToString());
            }
            return rowAffected;
        }
        public List<RecordExtended> GetAllRecords()
        {
            var list = new List<RecordExtended>();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<RecordExtended>("select Record.*, t.Name as ReservoirName FROM Record JOIN (SELECT Id, Name from Reservoir) AS t ON Record.ReservoirId=t.Id ORDER BY ReservoirName, DateTime");
            }

            return list;
        }
        public List<RecordExtended> GetRecordsForReservoir(int Id)
        {
            var list = new List<RecordExtended>();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                list = db.Query<RecordExtended>("select Record.*, t.Name as ReservoirName FROM Record JOIN (SELECT Id, Name from Reservoir) AS t ON Record.ReservoirId=t.Id AND Record.ReservoirId=" + Id.ToString("D") + " ORDER BY ReservoirName, DateTime");
            }

            return list;
        }
        public int AddRecord(Record record)
        {
            int newId = -1;
            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                newId = db.Insert(record);
                Console.WriteLine("new Id: " + newId.ToString());
            }
            return newId;
        }
        public Record GetLastRecord()
        {
            var record = new Record();

            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                var list = db.Query<Record>("SELECT * FROM Record Order By Id Desc Limit 1");
                if (list.Count > 0)
                {
                    record = list[0];
                }
                else
                {
                    record = null;
                }
            }

            return record;
        }
        public int UpdateRecord(Record record)
        {
            int rowAffected = 0;
            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                rowAffected = db.Update(record);
                Console.WriteLine("Row affected: " + rowAffected.ToString());
            }
            return rowAffected;
        }
        public int DeleteRecord(Record record)
        {
            int rowAffected = 0;
            using (var db = new SQLite.SQLiteConnection(dbPath))
            {
                rowAffected = db.Delete(record);
                Console.WriteLine("Row affected: " + rowAffected.ToString());
            }
            return rowAffected;
        }
    }
}
