using System.Threading.Tasks;

namespace dytsenayasar.Util.RazorViewRenderer
{
    public interface IRazorViewRenderer
    {
         Task<string> RenderViewToString<TModel>(string viewName, TModel model);
    }
}