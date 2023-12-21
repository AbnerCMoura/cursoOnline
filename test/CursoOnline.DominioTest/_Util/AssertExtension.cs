using Xunit;

namespace CursoOnline.DominioTest._Util;

public static class AssertExtension
{
    public static bool ComMensagem(this ArgumentException exception, string mensagem)
    {
        return exception.Message == mensagem;
    }
}