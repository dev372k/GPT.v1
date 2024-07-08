using Coravel.Invocable;
using Domain.Enumerations;
using Infrastructure.Services;

namespace API.Schedulers
{
    public class EveningScheduler : IInvocable
    {
        private readonly IGPTService _gptService;
        private readonly ILogger _log;

        public EveningScheduler(IGPTService gptService, ILogger<CheckStatusScheduler> log)
        {
            _gptService = gptService;
            _log = log;
        }

        public async Task Invoke()
        {
            await _gptService.UploadBatchAsync(enMotivationType.evening);
        }
    }
}
