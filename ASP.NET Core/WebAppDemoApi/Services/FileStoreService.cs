namespace WebAppDemoApi.Services
{
    public class FileStoreService
    {
        public string ReadFile(string fileName)
        {
            try
            {
                return File.ReadAllText(Path.GetFullPath(fileName));
                //return File.ReadAllText($"{baseDirectory}{fileName}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> ReadFileAsync(string fileName)
        {
            try
            {
                return await File.ReadAllTextAsync(Path.GetFullPath(fileName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveFile(string fileName, string text)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath(fileName), text);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the file: {ex.Message}");
            }
        }
    }
}
