namespace MultiShopMvc.Helpers
{
    public static class FileManagerw
    {
        public static string SaveFile(this IFormFile formFile, string rootpath, string folderpath)
        {
            string fileName = formFile.FileName;
            fileName = fileName.Length > 64 ? fileName.Substring(fileName.Length - 15, 15) : fileName;
            fileName = Guid.NewGuid().ToString() + fileName;

            string path = Path.Combine(rootpath, folderpath, fileName);
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(fs);
            };
            return fileName;
        }

        public static bool CheckFileLength(this IFormFile formFile, double length)
        {
            if (formFile.Length > length) return false;
            return true;
        }
        public static bool CheckFileType(this IFormFile formFile)
        {
            if (formFile.ContentType != "image/jpeg" && formFile.ContentType != "image/png") return false;
            return true;
        }
    }
}
