namespace hrhub.Utilities.Extentions;

public static class FileExtention
{
    public static string CreateFile(this IFormFile file, string webRoot, string folderName)
    {
        if(!IsValidFile(file)) return string.Empty;
        string fileName = Guid.NewGuid().ToString() + file.FileName;
        string filePath = Path.Combine(webRoot, folderName);
        using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
        {
            file.CopyTo(fileStream);
        }
        return fileName;
    }

    public static string UpdateFile(this IFormFile file, string webRootPath, string folderName, string oldUrl)
    {
        if (!IsValidFile(file)) return String.Empty;
        RemoveFile(Path.Combine(webRootPath, folderName, oldUrl));
        return file.CreateFile(webRootPath, folderName);
    }

    public static void RemoveFile(string path)
    {
        System.IO.File.Delete(path);
    }

    public static bool IsValidFile(IFormFile file)
    {
        if(!file.ContentType.Contains("image")) return false;
        if(file.Length == 0) return false;
        if(file.FileName.Length == 0 && file.FileName.Length > 2000000) return false;
        if(file is null) return false;
        return true;
    }
}
