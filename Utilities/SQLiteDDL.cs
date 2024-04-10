using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsVirusScanningSystem.Utilities
{
    public class SQLiteDDL
    {
        //Create Table（CT）

        //如果不存在，创建表格VirusSample
        public const string CT_VirusSample = $"CREATE TABLE IF NOT EXISTS {SQLiteGlobalName.TB_VirusSample}" +
            "('SampleId' INT AUTO_INCREMENT," +
            "'SampleName' VARCHAR(255)," +
            "'SampleHash' VARCHAR(255) NOT NULL, " +
            "'CreatedTime' DATETIME NOT NULL," +
            "PRIMARY KEY (SampleId));";

        //如果不存在，创建表格ScanRecord
        public const string CT_ScanRecord = $"CREATE TABLE IF NOT EXISTS {SQLiteGlobalName.TB_ScanRecord}" +
            "('RecordId' INT AUTO_INCREMENT," +
            "'ScanPath' VARCHAR(255) NOT NULL," +
            "'FileCount' INT NOT NULL," +
            "'FolderCount' INT NOT NULL," +
            "'VirusCount' INT NOT NULL," +
            "'ScanTime' DATETIME NOT NULL," +
            " PRIMARY KEY (RecordId));";

        //如果不存在，创建表格FileWhiteList
        public const string CT_FileWhiteList = $"CREATE TABLE IF NOT EXISTS {SQLiteGlobalName.TB_FileWhiteList}" +
            "('File_id' INT AUTO_INCREMENT," +
            "'File_name' VARCHAR(255) NOT NULL," +
            "'File_hash' VARCHAR(255) NOT NULL," +
            "'CreatedTime' DATETIME NOT NULL," +
            "PRIMARY KEY (File_id));";
    }
}
