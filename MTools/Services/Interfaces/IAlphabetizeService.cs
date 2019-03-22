using System.Collections.Generic;

namespace MTools.Services.Interfaces
{
    public interface IAlphabetizeService
    {
        string[] AlphabetizeArray(ref string[] array);

        IEnumerable<string> AlphabetizeArray(ref IEnumerable<string> array);
    }
}
