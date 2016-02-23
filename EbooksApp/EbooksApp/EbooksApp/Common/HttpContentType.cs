using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbooksApp.Common
{
    public enum HttpContentType
    {
        Json,
        FormEncoded
    }

    public enum AuthorizationType
    {
        Basic,
        Bearer
    }
}
