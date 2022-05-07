using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Catalogs.CatalogItems.UriComposer
{
    public interface IUriComposerServie
    {
        string ComposerImageUri(string src);
    }

    public class UriComposerServie : IUriComposerServie
    {
        public string ComposerImageUri(string src)
        {
            return "https://localhost:7196/" + src.Replace('\\', '/' ); 
        }
    }
}
 