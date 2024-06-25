using Coravel.Invocable;
using Domain.Entities;
using Domain.Helpers;
using Infrastructure.DTOs.BatchDTOs;
using Infrastructure.Services;

namespace API.Schedulers
{
    public class CheckStatusScheduler : IInvocable
    {
        private readonly IGPTService _gptService;
        private readonly BatchService _batchService;
        private readonly ContentService _contentService;
        private readonly ILogger _log;

        public CheckStatusScheduler(IGPTService gptService, ILogger<CheckStatusScheduler> log, 
            BatchService batchService, ContentService contentService)
        {
            _gptService = gptService;
            _log = log;
            _batchService = batchService;
            _contentService = contentService;
        }

        public async Task Invoke()
        {
            var batches = _batchService.Get();
            foreach (var batch in batches)
            {
                var response = await _gptService.CheckBatchStatusAsync(batch.BatchId);
                if (response != null)
                {
                    if (response.status == BatchStatus.Completed.ToString().ToLower())
                    {
                        var content = await _gptService.GetBatchAsync(response.output_file_id);
                        if (!string.IsNullOrEmpty(content))
                        {
                            var results = await JSONHelper.Read(content);
                            foreach (var result in results)
                            {
                                _contentService.Create(new Content
                                {
                                    request_id = result.request_id,
                                    content = result.content,
                                    custom_id = result.custom_id,
                                    response_id = result.id
                                });
                            }

                            _batchService.Update(batch.Id, new Batch
                            {
                                Id = batch.Id,
                                BatchId = batch.BatchId,
                                FileId = batch.FileId,
                                OutputFileId = response.output_file_id,
                                IsProcessed = true,
                            });
                        }
                    }
                    _log.LogInformation($"Datetime: {DateTime.Now} - Batch {batch.BatchId} process has been {response.status}");
                }
            }
        }
    }
}
