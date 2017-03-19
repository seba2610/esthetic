using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esthetic
{
    public class EnumConst
    {
        public enum DataAccessProvider { SqlServer }

        public enum PropertyType { Int, String, Bool, RentSaleEnum, CurrencyEnum, Unkown }

        public const string ImagesPath = @"{0}/images/";

        public const string ThumbnailImagesPath = @"{0}/thumbnail/";

        public const string AbsoluteFileName = "{0}/{1}";

        public const string ThumbnailDir = "thumbnail";

        public const int ThumbnailDefaultSize = 120;

        public const int ImageDefaultWidth = 120;

        public const int ImageDefaultHeight = 120;
    }
}
