using System;
using System.Collections.Generic;
using System.Text;

namespace MSIS_HMS.Core.Entities.DTOs
{
    public class PatientQRCodeDTO
    {
        public int Id { get; set; }
        public byte[] BarCodeImg { get; set; }
        public byte[] QRCodeImg { get; set; }
        public string BarCode { get; set; }
        public string QRCode { get; set; }
    }
}
