using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsVirusScanningSystem.Utilities
{
    public class SQLiteDQL
    {
        //查询病毒样本表格前一百条
        public const string VirusSampleData = $"Select * from {SQLiteGlobalName.TB_VirusSample} order by CreatedTime desc LIMIT 100";

        //通过MD5值查询病毒样本表格
        public const string ScanVirusDataByValue = $"SELECT * FROM {SQLiteGlobalName.TB_VirusSample} WHERE SampleName = ";

        //查询扫描病毒记录表格前十条
        public const string ScanVirusData = $"Select * from {SQLiteGlobalName.TB_ScanRecord} order by ScanTime desc LIMIT 10";

        //查询白名单表格
        public const string FileWhiteListData = $"Select * from {SQLiteGlobalName.TB_FileWhiteList}";

        //通过文件名称查询白名单表格
        public const string FileWhiteListDataByValue = $"SELECT * FROM {SQLiteGlobalName.TB_FileWhiteList} WHERE File_name = @value";
    }
}
