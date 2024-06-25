using Coravel.Invocable;
using Domain.Enumerations;
using Infrastructure.DTOs;
using Infrastructure.Services;


namespace API.Schedulers
{
    public class CreateBatchScheduler : IInvocable
    {
        private readonly IGPTService _gptService;
        private readonly ILogger _log;

        public CreateBatchScheduler(IGPTService gptService, ILogger<CheckStatusScheduler> log)
        {
            _gptService = gptService;
            _log = log;
        }

        public async Task Invoke()
        {
            await _gptService.UploadBatchAsync(enMotivationType.general);
        }
    }
}
