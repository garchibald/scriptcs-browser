using System.Linq;
using ScriptCs.Contracts;

namespace ScriptCs.Browser
{
    public class ScriptPack : IScriptPack
    {
        IScriptPackContext IScriptPack.GetContext()
        {
            return new Browser();
        }

        void IScriptPack.Initialize(IScriptPackSession session)
        {
            var namespaces = new string[0].ToList();

            namespaces.ForEach(session.ImportNamespace);
        }

        void IScriptPack.Terminate()
        {
        }
    }
}
