namespace WindowsVirusScanningSystem.ViewModel
{
    public class SearchRecordItem
    {
        public SearchRecordItem(string time, string fileName, string filePath)
        {
            Time = time;
            FileName = fileName;
            FilePath = filePath;
        }

        public string Time { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

    }
}
