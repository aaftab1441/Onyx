﻿using System.IO;
using Abp.Auditing;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Sixoclock.Onyx.Dto;

namespace Sixoclock.Onyx.Web.Controllers
{
    public class FileController : OnyxControllerBase
    {
        private readonly IAppFolders _appFolders;

        public FileController(IAppFolders appFolders)
        {
            _appFolders = appFolders;
        }

        [DisableAuditing]
        public ActionResult DownloadTempFile(FileDto file)
        {
            var filePath = Path.Combine(_appFolders.TempFileDownloadFolder, file.FileToken);
            if (!System.IO.File.Exists(filePath))
            {
                throw new UserFriendlyException(L("RequestedFileDoesNotExists"));
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            System.IO.File.Delete(filePath);
            return File(fileBytes, file.FileType, file.FileName);
        }
    }
}