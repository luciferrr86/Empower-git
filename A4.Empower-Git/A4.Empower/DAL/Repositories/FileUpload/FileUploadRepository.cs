using A4.DAL.Entites;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace A4.DAL.Repositories
{
   public class FileUploadRepository : Repository<Picture>, IFileUploadRepository
    {
        public FileUploadRepository(DbContext context) : base(context)
        {
         
        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public virtual Picture InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename,

        string altAttribute = null, string titleAttribute = null,

        bool isNew = true, bool validateBinary = true)
        {

            var picture = new Picture
            {
                PictureBinary = pictureBinary,
                MimeType = mimeType,
                AltAttribute = altAttribute,
                TitleAttribute = titleAttribute,
                IsNew = isNew,
            };
            _appContext.Picture.Add(picture);
            _appContext.SaveChanges();

            return picture;
        }

        public string GetPictureUrl(Picture picture, string filePath, int targetSize = 0)
        {
            var url = string.Empty;
            byte[] pictureBinary = null;
            if (picture != null)
                pictureBinary = picture.PictureBinary;
            if (picture == null || pictureBinary == null || pictureBinary.Length == 0)
            {                
                return url = "assets/images/default-profile.png";
            }

            if (picture.IsNew)
            {
                DeletePictureThumbs(picture, filePath);
                picture = UpdatePicture(picture.Id, pictureBinary, picture.MimeType);
            }

            var lastPart = GetFileExtensionFromMimeType(picture.MimeType);
            string thumbFileName;
            if (targetSize == 0)
            {
                thumbFileName = $"{picture.Id:0000000}.{lastPart}";
            }
            else
            {
                thumbFileName = $"{picture.Id:0000000}_{targetSize}.{lastPart}";
            }
            var thumbFilePath = GetThumbLocalPath(thumbFileName, filePath);
            using (var mutex = new Mutex(false, thumbFileName))
            {
                if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
                {
                    mutex.WaitOne();

                    if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
                    {
                        byte[] pictureBinaryResized;                      
                        pictureBinaryResized = pictureBinary.ToArray();
                        SaveThumb(thumbFilePath, thumbFileName, picture.MimeType, pictureBinaryResized,filePath);
                    }

                    mutex.ReleaseMutex();
                }

            }
            url = GetThumbUrl(thumbFileName, null);
            return url;
        }

        public string GetResumeUrl(Picture picture, string filePath, int targetSize = 0)
        {
            var url = string.Empty;
            byte[] pictureBinary = null;
            if (picture != null)
                pictureBinary = picture.PictureBinary;
            if (picture == null || pictureBinary == null || pictureBinary.Length == 0)
            {
                return url = "assets/images/default-resume.png";
            }

            if (picture.IsNew)
            {
                DeletePictureThumbs(picture, filePath);
                picture = UpdatePicture(picture.Id, pictureBinary, picture.MimeType);
            }

            var lastPart = GetFileExtensionFromMimeType(picture.MimeType);
            string thumbFileName;
            if (targetSize == 0)
            {
                thumbFileName = $"{picture.Id:0000000}.{lastPart}";
            }
            else
            {
                thumbFileName = $"{picture.Id:0000000}_{targetSize}.{lastPart}";
            }
            var thumbFilePath = GetResumeLocalPath(thumbFileName, filePath);
            using (var mutex = new Mutex(false, thumbFileName))
            {
                if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
                {
                    mutex.WaitOne();

                    if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
                    {
                        byte[] pictureBinaryResized;
                        pictureBinaryResized = pictureBinary.ToArray();
                        SaveThumb(thumbFilePath, thumbFileName, picture.MimeType, pictureBinaryResized, filePath);
                    }

                    mutex.ReleaseMutex();
                }

            }
            url = GetResumeThumbUrl(thumbFileName, null);
            return url;
        }

        public string GetDocumentUrl(Picture picture, string filePath, int targetSize = 0)
        {
            var url = string.Empty;
            byte[] pictureBinary = null;
            if (picture != null)
                pictureBinary = picture.PictureBinary;
            if (picture == null || pictureBinary == null || pictureBinary.Length == 0)
            {
                return url = "assets/images/default-resume.png";
            }

            if (picture.IsNew)
            {
                DeletePictureThumbs(picture, filePath);
                picture = UpdatePicture(picture.Id, pictureBinary, picture.MimeType);
            }

            var lastPart = GetFileExtensionFromMimeType(picture.MimeType);
            string thumbFileName;
            if (targetSize == 0)
            {
                thumbFileName = $"{picture.Id:0000000}.{lastPart}";
            }
            else
            {
                thumbFileName = $"{picture.Id:0000000}_{targetSize}.{lastPart}";
            }
            var thumbFilePath = GetDocumentLocalPath(thumbFileName, filePath);
            using (var mutex = new Mutex(false, thumbFileName))
            {
                if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
                {
                    mutex.WaitOne();

                    if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
                    {
                        byte[] pictureBinaryResized;
                        pictureBinaryResized = pictureBinary.ToArray();
                        SaveThumb(thumbFilePath, thumbFileName, picture.MimeType, pictureBinaryResized, filePath);
                    }

                    mutex.ReleaseMutex();
                }

            }
            url = GetDocumentThumbUrl(thumbFileName, null);
            return url;
        }

        public string GetSalesDocumentUrl(Picture picture, string filePath, int targetSize = 0)
        {
            var url = string.Empty;
            byte[] pictureBinary = null;
            if (picture != null)
                pictureBinary = picture.PictureBinary;
            if (picture == null || pictureBinary == null || pictureBinary.Length == 0)
            {
                return url = "assets/images/default-resume.png";
            }

            if (picture.IsNew)
            {
                DeletePictureThumbs(picture, filePath);
                picture = UpdatePicture(picture.Id, pictureBinary, picture.MimeType);
            }

            var lastPart = GetFileExtensionFromMimeType(picture.MimeType);
            string thumbFileName;
            if (targetSize == 0)
            {
                thumbFileName = $"{picture.Id:0000000}.{lastPart}";
            }
            else
            {
                thumbFileName = $"{picture.Id:0000000}_{targetSize}.{lastPart}";
            }
            var thumbFilePath = GetSalesDocumentLocalPath(thumbFileName, filePath);
            using (var mutex = new Mutex(false, thumbFileName))
            {
                if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
                {
                    mutex.WaitOne();

                    if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
                    {
                        byte[] pictureBinaryResized;
                        pictureBinaryResized = pictureBinary.ToArray();
                        SaveThumb(thumbFilePath, thumbFileName, picture.MimeType, pictureBinaryResized, filePath);
                    }

                    mutex.ReleaseMutex();
                }

            }
            url = GetSalesDocumentThumbUrl(thumbFileName, null);
            return url;
        }


        protected virtual string GetFileExtensionFromMimeType(string mimeType)
        {
            if (mimeType == null)
                return null;


            var parts = mimeType.Split('/');
            var lastPart = parts[parts.Length - 1];
            switch (lastPart)
            {
                case "pjpeg":
                    lastPart = "jpg";
                    break;
                case "x-png":
                    lastPart = "png";
                    break;
                case "x-icon":
                    lastPart = "ico";
                    break;
            }
            return lastPart;
        }

        protected virtual bool GeneratedThumbExists(string thumbFilePath, string thumbFileName)
        {
            return File.Exists(thumbFilePath);
        }

        protected virtual void DeletePictureThumbs(Picture picture,string filePath)
        {
            var filter = $"{picture.Id:0000000}*.*";
            //var thumbDirectoryPath = Path.Combine(filePath, "Profile");
            var currentFiles = System.IO.Directory.GetFiles(filePath, filter, SearchOption.AllDirectories);
            foreach (var currentFileName in currentFiles)
            {
                var thumbFilePath = GetThumbLocalPath(currentFileName, filePath);
                File.Delete(thumbFilePath);
            }
        }

        protected virtual string GetThumbLocalPath(string thumbFileName,string filePath)
        {
           var thumbsDirectoryPath = Path.Combine(filePath, "Profile");
            var thumbFilePath = Path.Combine(thumbsDirectoryPath, thumbFileName);
            return thumbFilePath;
        }
        protected virtual string GetResumeLocalPath(string thumbFileName, string filePath)
        {
            var thumbsDirectoryPath = Path.Combine(filePath, "Resume");
            var thumbFilePath = Path.Combine(thumbsDirectoryPath, thumbFileName);
            return thumbFilePath;
        }

        protected virtual string GetDocumentLocalPath(string thumbFileName, string filePath)
        {
            var thumbsDirectoryPath = Path.Combine(filePath, "ExpenseDocument");
            var thumbFilePath = Path.Combine(thumbsDirectoryPath, thumbFileName);
            return thumbFilePath;
        }
        protected virtual string GetSalesDocumentLocalPath(string thumbFileName, string filePath)
        {
            var thumbsDirectoryPath = Path.Combine(filePath, "SalesDocument");
            var thumbFilePath = Path.Combine(thumbsDirectoryPath, thumbFileName);
            return thumbFilePath;
        }

        protected virtual string GetThumbUrl(string thumbFileName, string storeLocation = null)
        {
            var url = "Profile/";

            url = url + thumbFileName;
            return url;
        }

        protected virtual string GetResumeThumbUrl(string thumbFileName, string storeLocation = null)
        {
            var url = "Resume/";

            url = url + thumbFileName;
            return url;
        }

        protected virtual string GetDocumentThumbUrl(string thumbFileName, string storeLocation = null)
        {
            var url = "ExpenseDocument/";

            url = url + thumbFileName;
            return url;
        }
        protected virtual string GetSalesDocumentThumbUrl(string thumbFileName, string storeLocation = null)
        {
            var url = "SalesDocument/";

            url = url + thumbFileName;
            return url;
        }

        protected virtual void SaveThumb(string thumbFilePath, string thumbFileName, string mimeType, byte[] binary,string filePath)
        {
            var thumbsDirectoryPath = Path.Combine(filePath, "Profile");
            if (!System.IO.Directory.Exists(thumbsDirectoryPath))
                System.IO.Directory.CreateDirectory(thumbsDirectoryPath);

            File.WriteAllBytes(thumbFilePath, binary);
        }

        public virtual Picture UpdatePicture(int pictureId, byte[] pictureBinary, string mimeType, bool isNew = true)
        {
            var picture = GetPictureById(pictureId);
            if (picture == null)
                return null;
            picture.PictureBinary = pictureBinary;
            picture.MimeType = mimeType;
            picture.AltAttribute = picture.AltAttribute;
            picture.TitleAttribute = picture.TitleAttribute;
            picture.IsNew = isNew;
            _appContext.SaveChanges();
            return picture;
        }

        public Picture GetPictureById(int pictureId)
        {
            if (pictureId == 0)
                return null;

            return _appContext.Picture.Where(x => x.Id == pictureId).FirstOrDefault();
        }

    }
}
