using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WindowsVirusScanningSystem.Utilities
{
    public class SQLiteGlobalName
    {
        //表名（作用：解耦）
        public const string TB_VirusSample = "VirusSample";

        public const string TB_ScanRecord = "ScanRecord";

        public const string TB_FileWhiteList = "FileWhiteList";

        //数据库所在路径，readonly动态生成
        public static readonly string DbPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).ToString();
        //数据库名称
        public const string DbName = "Data.db";
    }
}
