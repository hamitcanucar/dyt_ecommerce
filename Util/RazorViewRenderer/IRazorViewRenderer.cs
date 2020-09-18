using System.Threading.Tasks;

namespace dyt_ecommerce.Util.RazorViewRenderer
{
    public interface IRazorViewRenderer
    {
         Task<string> RenderViewToString<TModel>(string viewName, TModel model);
    }
}