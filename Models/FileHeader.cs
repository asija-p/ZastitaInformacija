using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZastitaInformacija_19322.Models
{
    public class FileHeader
    {
        public string FileName { get; set; }         // original filename
        public long FileSize { get; set; }           // in bytes
        public DateTime CreatedAt { get; set; }      // creation time
        public string EncryptionAlgorithm { get; set; }  // e.g., "RC6", "Playfair"
        public string HashAlgorithm { get; set; }        // e.g., "SHA-1" or null
        public string HashValue { get; set; }
    }
}
