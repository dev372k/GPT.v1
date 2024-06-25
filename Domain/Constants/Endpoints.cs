using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constants
{
    public class Endpoints
    {
        public const string UPLOAD_BATCH = "https://api.openai.com/v1/files";
        public const string CREATE_BATCH = "https://api.openai.com/v1/batches";
        public const string CHECK_STATUS_BATCH = "https://api.openai.com/v1/batches/{0}";
        public const string GET_BATCH = "https://api.openai.com/v1/files/{0}/content";
    }
}
