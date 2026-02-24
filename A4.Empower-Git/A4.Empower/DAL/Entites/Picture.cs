using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
    public class Picture
    {
        #region Properties

        public int Id { get; set; }

        public byte[] PictureBinary { get; set; }

        public string MimeType { get; set; }

        public string AltAttribute { get; set; }

        public string TitleAttribute { get; set; }

        public bool IsNew { get; set; }
        #endregion
    }
}
