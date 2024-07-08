using Domain.Constants;
using Domain.Entities;
using Domain.Enumerations;
using Infrastructure.DTOs.BatchDTOs;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Services
{
    public interface IGPTService
    {
        Task<Batch> UploadBatchAsync(enMotivationType type);
        Task<BatchResponse> CreateBatchAsync(string fileid);
        Task<BatchResponse> CheckBatchStatusAsync(string batchId);
        Task<string> GetBatchAsync(string fileId);
    }
    public class GPTService : IGPTService
    {
        private IHttpClientFactory httpClientFactory;
        private IConfiguration configuration;
        private BatchService _batchService;
        private MotivationService _motivationService;

        public GPTService(IConfiguration _configuration, IHttpClientFactory _httpClientFactory, BatchService batchService, MotivationService motivationService)
        {
            httpClientFactory = _httpClientFactory;
            configuration = _configuration;
            _batchService = batchService;
            _motivationService = motivationService;
        }
        public async Task<Batch> UploadBatchAsync(enMotivationType type)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("ChtpGPT");

                using var httpReq = new HttpRequestMessage(HttpMethod.Post, Endpoints.UPLOAD_BATCH);
                httpReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", configuration["OpenAIKey"]);

                if (type == enMotivationType.general)
                    httpReq.Content = _motivationService.General();
                else if(type == enMotivationType.morning)
                    httpReq.Content = _motivationService.Morning();
                else if(type == enMotivationType.evening)
                    httpReq.Content = _motivationService.Evening();

                using HttpResponseMessage httpResponse = await httpClient.SendAsync(httpReq);

                if (!httpResponse.IsSuccessStatusCode)
                    throw new HttpRequestException($"HTTP request failed with status code {httpResponse.StatusCode}");

                var uploadBatchResponse = JsonSerializer.Deserialize<BatchUploadModel>(await httpResponse.Content.ReadAsStringAsync());

                var createBatchResponse = await CreateBatchAsync(uploadBatchResponse.id);

                var batch = new Batch
                {
                    FileId = uploadBatchResponse.id,
                    BatchId = createBatchResponse.id,
                    IsProcessed = false
                };

                _batchService.Create(batch);

                return batch;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BatchResponse> CreateBatchAsync(string fileId)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("ChtpGPT");

                using var httpReq = new HttpRequestMessage(HttpMethod.Post, Endpoints.CREATE_BATCH);
                httpReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", configuration["OpenAIKey"]);
                string requestString = JsonSerializer.Serialize(new
                {
                    input_file_id = fileId,
                    endpoint = "/v1/chat/completions",
                    completion_window = "24h"
                });
                httpReq.Content = new StringContent(requestString, Encoding.UTF8, "application/json");

                using HttpResponseMessage httpResponse = await httpClient.SendAsync(httpReq);

                if (!httpResponse.IsSuccessStatusCode)
                    throw new HttpRequestException($"HTTP request failed with status code {httpResponse.StatusCode}");

                var response = await httpResponse.Content.ReadAsStringAsync();
                return httpResponse.IsSuccessStatusCode ? JsonSerializer.Deserialize<BatchResponse>(response) : null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BatchResponse> CheckBatchStatusAsync(string batchId)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("ChtpGPT");

                using var httpReq = new HttpRequestMessage(HttpMethod.Get, string.Format(Endpoints.CHECK_STATUS_BATCH, batchId));
                httpReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", configuration["OpenAIKey"]);

                using HttpResponseMessage httpResponse = await httpClient.SendAsync(httpReq);

                if (!httpResponse.IsSuccessStatusCode)
                    throw new HttpRequestException($"HTTP request failed with status code {httpResponse.StatusCode}");

                var response = await httpResponse.Content.ReadAsStringAsync();
                return httpResponse.IsSuccessStatusCode ? JsonSerializer.Deserialize<BatchResponse>(response) : null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> GetBatchAsync(string fileId)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("ChtpGPT");

                using var httpReq = new HttpRequestMessage(HttpMethod.Get, string.Format(Endpoints.GET_BATCH, fileId));
                httpReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", configuration["OpenAIKey"]);

                using HttpResponseMessage httpResponse = await httpClient.SendAsync(httpReq);

                if (!httpResponse.IsSuccessStatusCode)
                    throw new HttpRequestException($"HTTP request failed with status code {httpResponse.StatusCode}");

                using Stream contentStream = await httpResponse.Content.ReadAsStreamAsync();
                using StreamReader reader = new StreamReader(contentStream);
                return await reader.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

    }
}

