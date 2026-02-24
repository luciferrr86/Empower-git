using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
    public interface IFileUploadRepository : IRepository<Picture>
    {
        Picture InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename,

        string altAttribute = null, string titleAttribute = null,

        bool isNew = true, bool validateBinary = true);

        string GetPictureUrl(Picture picture, string filePath, int targetSize = 0);

        string GetResumeUrl(Picture picture, string filePath, int targetSize = 0);

        string GetDocumentUrl(Picture picture, string filePath, int targetSize = 0);

        string GetSalesDocumentUrl(Picture picture, string filePath, int targetSize = 0);

        
        Picture GetPictureById(int pictureId);
    }
}
