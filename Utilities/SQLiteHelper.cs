using System;
using System.Data.SQLite;
using System.Data;
using System.Reflection;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using WindowsVirusScanningSystem.ViewModel;

namespace WindowsVirusScanningSystem.Utilities
{
    public class SQLiteHelper
    {
        //连接对象
        private SQLiteConnection SQLiteConn = null;

        private string FilePath = string.Empty;

        //连接字符串
        private string SQLiteConnString = string.Empty;

        //单例化
        private static SQLiteHelper instance = null;
        public static SQLiteHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SQLiteHelper();
                }
                return instance;
            }
        }

        public SQLiteHelper()
        {

            SQLiteConnString = $"Data Source={SQLiteGlobalName.DbPath}\\{SQLiteGlobalName.DbName}";

            FilePath = $"{SQLiteGlobalName.DbPath}\\{SQLiteGlobalName.DbName}";

            //初始化数据库
            InitializedDB();
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        private void InitializedDB()
        {
            try
            {
                // 创建SQLite连接对象
                using (SQLiteConn = new SQLiteConnection(SQLiteConnString))
                {
                    SQLiteConn.Open();

                    //操作
                    using (SQLiteCommand command = new SQLiteCommand(SQLiteConn))
                    {
                        //假若数据库不存在，创建数据库
                        if (!File.Exists(FilePath))
                        {
                            CreateDB();
                        }

                        //扫描记录表是否存在
                        command.CommandText = SQLiteDDL.CT_ScanRecord;

                        // 执行
                        command.ExecuteNonQuery();

                        // 病毒库表是否存在
                        command.CommandText = SQLiteDDL.CT_VirusSample;

                        // 执行
                        command.ExecuteNonQuery();

                        //白名单表是否存在
                        command.CommandText = SQLiteDDL.CT_FileWhiteList;

                        // 执行
                        command.ExecuteNonQuery();
                    }

                    SQLiteConn.Close();
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"初始化数据库失败:{ex.Message}");
            }
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void CreateDB()
        {
            try
            {
                SQLiteConnection.CreateFile(FilePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"创建数据库失败:{ex.Message}");
            }
        }

        /// <summary>
        /// 查询病毒扫描记录表数据
        /// </summary>
        public DataTable GetScanVirusData()
        {
            try
            {
                using (SQLiteConn = new SQLiteConnection(SQLiteConnString))
                {
                    SQLiteConn.Open();

                    DataTable tb = new DataTable();

                    using (SQLiteCommand cmd = new SQLiteCommand(SQLiteConn))
                    {
                        cmd.CommandText = SQLiteDQL.ScanVirusData;

                        using (SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd))
                        {
                            ad.Fill(tb);
                        }
                    }

                    SQLiteConn.Close();

                    return tb;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"查询病毒扫描记录表数据失败:{ex.Message}");

            }
        }

        /// <summary>
        /// 查询病毒样本表数据
        /// </summary>
        public DataTable GetVirusSampleData()
        {
            try
            {
                using (SQLiteConn = new SQLiteConnection(SQLiteConnString))
                {
                    SQLiteConn.Open();

                    DataTable tb = new DataTable();

                    using (SQLiteCommand cmd = new SQLiteCommand(SQLiteConn))
                    {
                        cmd.CommandText = SQLiteDQL.VirusSampleData;

                        using (SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd))
                        {
                            ad.Fill(tb);
                        }
                    }

                    SQLiteConn.Close();

                    return tb;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"查询病毒样本表数据失败:{ex.Message}");

            }
        }

        /// <summary>
        /// 按MD5值查询病毒样本表数据,是否存在
        /// </summary>
        public int GetVirusSampleDataByValue(string md5)
        {
            try
            {
                using (SQLiteConn = new SQLiteConnection(SQLiteConnString))
                {
                    SQLiteConn.Open();

                    int result = 0;

                    using (SQLiteCommand cmd = new SQLiteCommand(SQLiteConn))
                    {
                        cmd.CommandText = $"{SQLiteDQL.ScanVirusDataByValue}'{md5}';";

                        result = cmd.ExecuteNonQuery();
                    }

                    SQLiteConn.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"查询病毒样本表数据失败:{ex.Message}");

            }
        }

        /// <summary>
        /// 查询白名单表数据
        /// </summary>
        public DataTable GetFileWhiteListData()
        {
            try
            {
                using (SQLiteConn = new SQLiteConnection(SQLiteConnString))
                {
                    SQLiteConn.Open();

                    DataTable tb = new DataTable();

                    using (SQLiteCommand cmd = new SQLiteCommand(SQLiteConn))
                    {
                        cmd.CommandText = SQLiteDQL.FileWhiteListData;

                        using (SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd))
                        {
                            ad.Fill(tb);
                        }
                    }

                    SQLiteConn.Close();

                    return tb;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"查询白名单表数据失败:{ex.Message}");

            }
        }

        /// <summary>
        /// 按文件路径查询白名单表数据,是否存在
        /// </summary>
        public int GetFileWhiteListDataByValue(string filePath)
        {
            try
            {
                using (SQLiteConn = new SQLiteConnection(SQLiteConnString))
                {
                    SQLiteConn.Open();

                    DataTable tb = new DataTable();

                    int result = 0;

                    using (SQLiteCommand cmd = new SQLiteCommand(SQLiteConn))
                    {
                        cmd.CommandText = $"{SQLiteDQL.FileWhiteListData};";

                        cmd.Parameters.AddWithValue("@value", filePath);

                        using (SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd))
                        {
                            ad.Fill(tb);

                            for(int i = 0; i < tb.Rows.Count; i++)
                            {
                                if(filePath == tb.Rows[i][1].ToString())
                                {
                                    result = 1;
                                    break;
                                }
                            }
                        }
                    }

                    SQLiteConn.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"查询病毒样本表数据失败:{ex.Message}");

            }
        }

        /// <summary>
        /// 插入病毒样本数据
        /// </summary>
        /// <param name="sampleId"></param>
        /// <param name="sampleName"></param>
        /// <param name="sampleHash"></param>
        /// <param name="createdTime"></param>
        /// <exception cref="Exception"></exception>
        public void InsertVirusSampleData(string sampleId, string sampleName, string sampleHash, string createdTime)
        {
            try
            {
                using (SQLiteConn = new SQLiteConnection(SQLiteConnString))
                {
                    SQLiteConn.Open();

                    using (SQLiteCommand command = new SQLiteCommand(SQLiteConn))
                    {
                        command.CommandText = SQLiteDML.InsertVirusSampleData;

                        // 设置参数值，避免SQL注入
                        command.Parameters.AddWithValue("@SampleId", sampleId);
                        command.Parameters.AddWithValue("@SampleName", sampleName);
                        command.Parameters.AddWithValue("@SampleHash", sampleHash);
                        command.Parameters.AddWithValue("@CreatedTime", createdTime);

                        command.ExecuteNonQuery(); // 执行插入数据的SQL语句
                    }

                    SQLiteConn.Close();
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"插入病毒样本数据失败:{ex.Message}");
            }
        }

        /// <summary>
        /// 插入扫描病毒搜索记录
        /// </summary>
        /// <param name="time"></param>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="virusCount"></param>
        /// <param name="type"></param>
        /// <exception cref="Exception"></exception>
        public void InsertScanVirusData(string recordId, string scanPath, string fileCount, string folderCount, string virusCount, string scanTime)
        {
            try
            {
                using (SQLiteConn = new SQLiteConnection(SQLiteConnString))
                {
                    SQLiteConn.Open();

                    using (SQLiteCommand command = new SQLiteCommand(SQLiteConn))
                    {
                        command.CommandText = SQLiteDML.InsertScanVirusData;

                        // 设置参数值，避免SQL注入
                        command.Parameters.AddWithValue("@Id", recordId);
                        command.Parameters.AddWithValue("@Path", scanPath);
                        command.Parameters.AddWithValue("@FiCou", fileCount);
                        command.Parameters.AddWithValue("@FoCou", folderCount);
                        command.Parameters.AddWithValue("@ViCou", virusCount);
                        command.Parameters.AddWithValue("@Time", scanTime);

                        command.ExecuteNonQuery(); // 执行插入数据的SQL语句
                    }

                    SQLiteConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"插入病毒样本数据失败:{ex.Message}");
            }
        }

        /// <summary>
        /// 插入白名单
        /// </summary>
        /// <param name="time"></param>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="virusCount"></param>
        /// <param name="type"></param>
        /// <exception cref="Exception"></exception>
        public void InsertFileWhiteListData(string File_id, string File_name, string File_hash, string CreatedTime)
        {
            try
            {
                using (SQLiteConn = new SQLiteConnection(SQLiteConnString))
                {
                    SQLiteConn.Open();

                    using (SQLiteCommand command = new SQLiteCommand(SQLiteConn))
                    {
                        command.CommandText = SQLiteDML.InsertFileWhiteListData;

                        // 设置参数值，避免SQL注入
                        command.Parameters.AddWithValue("@File_id", File_id);
                        command.Parameters.AddWithValue("@File_name", File_name);
                        command.Parameters.AddWithValue("@File_hash", File_hash);
                        command.Parameters.AddWithValue("@CreatedTime", CreatedTime);

                        command.ExecuteNonQuery(); // 执行插入数据的SQL语句
                    }

                    SQLiteConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"插入病毒样本数据失败:{ex.Message}");
            }
        }

        public bool DeleteVirusSample(string SampleId)
        {
            try
            {
                using (SQLiteConn = new SQLiteConnection(SQLiteConnString))
                {
                    SQLiteConn.Open();

                    using (SQLiteCommand command = new SQLiteCommand(SQLiteConn))
                    {
                        command.CommandText = SQLiteDML.DeleteVirusSampleData;

                        command.Parameters.AddWithValue("@SampleId", SampleId);

                        int rows = command.ExecuteNonQuery();

                        SQLiteConn.Close();

                        if (rows > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"插入病毒样本数据失败:{ex.Message}");
            }
        }

        public bool DeleteWhiteFile(string whiteFileId)
        {
            using (SQLiteConn = new SQLiteConnection(SQLiteConnString))
            {
                SQLiteConn.Open();

                using (SQLiteCommand command = new SQLiteCommand(SQLiteConn))
                {
                    command.CommandText = SQLiteDML.DeleteFileWhiteData;

                    command.Parameters.AddWithValue("@File_id", whiteFileId);

                    int rows = command.ExecuteNonQuery();

                    SQLiteConn.Close();

                    if (rows > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
