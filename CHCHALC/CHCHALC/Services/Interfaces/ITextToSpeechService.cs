using System.Threading.Tasks;

namespace CHCHALC.Services
{
    public interface ITextToSpeechService
    {
        Task SpeakAsync(string text);
    }
}
