using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using A4.Empower.Helpers;
using DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{
    [Produces("application/json")]
    [Route("api/FileUpload")]
    public class FileUploadController : Controller
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        readonly IEmailer _emailer;
        IWebHostEnvironment _hostingEnvironment;

        public FileUploadController(IUnitOfWork unitOfWork, ILogger<FileUploadController> logger, IEmailer emailer, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailer = emailer;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("create")]
        public IActionResult Upload(int typeId, string userId)
        {
            if (typeId == 1)
            {
                var status = new List<MyReponse>();
                if (Request.Form.Files.Count > 0)
                {
                    foreach (var item in Request.Form.Files)
                    {
                        var fileBinary = item.GetDownloadBits();
                        var qqFileNameParameter = "qqfilename";
                        var fileName = item.FileName;
                        if (string.IsNullOrEmpty(fileName) && Request.Form.ContainsKey(qqFileNameParameter))
                            fileName = Request.Form[qqFileNameParameter].ToString();

                        fileName = Path.GetFileName(fileName);
                        var contentType = item.ContentType;
                        var fileExtension = Path.GetExtension(fileName);
                        if (!string.IsNullOrEmpty(fileExtension))
                            fileExtension = fileExtension.ToLowerInvariant();


                        var picture = _unitOfWork.FileUpload.InsertPicture(fileBinary, fileExtension, null, fileName);
                        status.Add(new MyReponse { success = true, pictureId = picture.Id.ToString(), imageUrl = _unitOfWork.FileUpload.GetDocumentUrl(picture, _hostingEnvironment.WebRootPath) });
                    }
                }
                return Ok(status);
            }
            else
            {
                var status = new MyReponse();
                var httpPostedFile = Request.Form.Files.FirstOrDefault();
                var fileBinary = httpPostedFile.GetDownloadBits();
                var qqFileNameParameter = "qqfilename";
                var fileName = httpPostedFile.FileName;
                if (string.IsNullOrEmpty(fileName) && Request.Form.ContainsKey(qqFileNameParameter))
                    fileName = Request.Form[qqFileNameParameter].ToString();
                fileName = Path.GetFileName(fileName);
                var contentType = httpPostedFile.ContentType;
                var fileExtension = Path.GetExtension(fileName);
                if (typeId == 2)
                {
                    var picture = _unitOfWork.FileUpload.InsertPicture(fileBinary, fileExtension, null, fileName);
                    status.success = true;
                    if (picture.Id != 0)
                    {
                        status.pictureId = picture.Id.ToString();
                        _unitOfWork.Profile.UpdateProfilePicDetail(status.pictureId = picture.Id.ToString(), userId);
                    }
                    
                    status.imageUrl = _unitOfWork.FileUpload.GetPictureUrl(picture, _hostingEnvironment.WebRootPath);

                }
                else if (typeId == 201)
                {
                    var picture = _unitOfWork.FileUpload.InsertPicture(fileBinary, fileExtension, null);
                    status.success = true;
                    if (picture.Id != 0)
                    {
                        status.pictureId = picture.Id.ToString();
                    }
                    status.imageUrl = _unitOfWork.FileUpload.GetSalesDocumentUrl(picture, _hostingEnvironment.WebRootPath);

                }
                else
                {
                    if (!string.IsNullOrEmpty(fileExtension))
                        fileExtension = fileExtension.ToLowerInvariant();


                    switch (fileExtension)
                    {
                        case ".doc":
                            contentType = ".doc";
                            break;
                        case ".docx":
                            contentType = ".docx";
                            break;
                        case ".pdf":
                            contentType = MimeTypes.ApplicationPdf;
                            break;
                        default:
                            break;
                    }

                    var candidateProfile = _unitOfWork.JobCandidateProfile.Find(m => m.UserId == userId).FirstOrDefault();
                    if (candidateProfile != null)
                    {
                        var picture = _unitOfWork.FileUpload.InsertPicture(fileBinary, contentType, null, fileName);
                        if (picture.Id != 0)
                        {
                            if (candidateProfile != null)
                            {
                                candidateProfile.ResumeId = picture.Id;
                                _unitOfWork.JobCandidateProfile.Update(candidateProfile);
                                _unitOfWork.SaveChanges();
                            }
                            status.success = true;
                        }
                        status.imageUrl = _unitOfWork.FileUpload.GetResumeUrl(picture, _hostingEnvironment.WebRootPath);
                    }
                }
                return Ok(status);
            }
        }

        public class MyReponse
        {
            public Boolean success { get; set; }
            public String pictureId { get; set; }
            public string imageUrl { get; set; }

            public MyReponse()
            {
                this.success = false;
            }
        }

    }
}
