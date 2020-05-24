using Microsoft.AspNetCore.Mvc;
using QRCodeServices.Interfaces;

namespace QRCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        private readonly IQRService _qrService;
        public QRCodeController(IQRService qrService)
        {
            _qrService = qrService;
        }

        [HttpGet]
        [Route("GetQRCodeData")]
        public string GetQRCodeData(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return "FilePath cannot be null or empty";
            }
            return _qrService.ReadQRCode(filePath);
        }
    }
}