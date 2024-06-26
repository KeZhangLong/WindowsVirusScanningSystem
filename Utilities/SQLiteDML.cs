﻿using System;
using System.Xml.Linq;

namespace WindowsVirusScanningSystem.Utilities
{
    public class SQLiteDML
    {
        public const string InsertVirusSampleData = $"INSERT OR IGNORE INTO VirusSample (SampleId,SampleName,SampleHash,CreatedTime) " +
            $"VALUES (@SampleId,@SampleName,@SampleHash,@CreatedTime)";

        public const string InsertScanVirusData = $"INSERT OR IGNORE INTO ScanRecord (RecordId,ScanPath,FileCount,FolderCount,VirusCount,ScanTime) " +
           $"VALUES (@Id,@Path,@FiCou,@FoCou,@ViCou,@Time)";

        public const string InsertFileWhiteListData = $"INSERT OR IGNORE INTO FileWhiteList (File_id,File_name,File_hash,CreatedTime) " +
           $"VALUES (@File_id,@File_name,@File_hash,@CreatedTime)";

        public const string DeleteVirusSampleData = "delete from VirusSample where SampleId=@SampleId";

        public const string DeleteFileWhiteData = "delete from FileWhiteList where File_id=@File_id";
    }
}
