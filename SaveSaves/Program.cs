Console.WriteLine("Hello, World!!!");
//string currentDirectories = Directory.GetCurrentDirectory();
string currentDirectories = $"D:\\Game";
string[] directories = Directory.GetDirectories(currentDirectories);
string[] names = new string[directories.Length];
for (int i = 0; i < directories.Length; i++)
{
    int index = directories[i].LastIndexOf('\\');
    names[i] = directories[i].Substring(index + 1);
}
for (int i = 0; i < names.Length; i++)
{
    int savesCopyCount = 0;
    int savesSkipCount = 0;

    string copySaves = Path.Combine("f:", "Users", "ilsch", "Documents", "saves", names[i], "game", "saves");
    string originalSaves = Path.Combine(directories[i], "game", "saves");
    if (!Directory.Exists(originalSaves))
    {
        continue;
    }
    if (!Directory.Exists(copySaves))
    {
        Directory.CreateDirectory(copySaves);
    }
    DirectoryInfo directoryInfo = new DirectoryInfo(originalSaves);
    FileInfo[] files = directoryInfo.GetFiles();
    foreach (FileInfo file in files)
    {
        string fileName = Path.Combine(copySaves, file.Name);
        if (File.Exists(fileName))
        {
            FileInfo fileInfo = new FileInfo(fileName);
            if (fileInfo.LastWriteTime == file.LastWriteTime)
            {
                savesSkipCount++;
                continue;
            }
        }
        File.Copy(file.FullName, fileName, true);
        savesCopyCount++;


    }
    Console.WriteLine($"Сохранения игры {names[i]}. Сохранений скопировано {savesCopyCount}, пропущено {savesSkipCount}");
}
Console.Read();