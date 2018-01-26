using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace ParserAzure
{
    public static class Function1
    {
        [FunctionName("Function1")]
        [return: Queue("myqueue-output", Connection = "QueueOUT")]
        public static string Run([QueueTrigger("myqueue-items", Connection = "QueueIN")]string myQueueItem, TraceWriter log)
        {
            string[] names = myQueueItem.Split(' ');
            string fileDownload = names[0];
            string fileUpload = names[1];



            BlobHelper blobHelper = new BlobHelper(Environment.GetEnvironmentVariable("QueueIN"), "test");
            var content = blobHelper.Read(fileDownload);
            blobHelper.Write($"{fileUpload}", content);
            return ($"Blob done: file {fileUpload} was saved");
        }
    }
}