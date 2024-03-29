﻿namespace DeleteAuditLog
{
    using System;
    using Microsoft.Crm.Sdk.Messages;
   

    class DeleteLog
    {
        static void Main(string[] args)
        {
            try
            {
                //Sample code availbe on MS site
                //https://docs.microsoft.com/en-us/previous-versions/dynamicscrm-2016/developers-guide/gg327533(v=crm.8)


                var _service = CRMConnector.ConnectToCRM();

                /**/
                // Get the list of audit partitions.
                RetrieveAuditPartitionListResponse partitionRequest =
                    (RetrieveAuditPartitionListResponse)_service.Execute(new RetrieveAuditPartitionListRequest());
                AuditPartitionDetailCollection partitions = partitionRequest.AuditPartitionDetailCollection;

                // Create a delete request with an end date earlier than possible.
                DeleteAuditDataRequest deleteRequest = new DeleteAuditDataRequest();

                /*Can be used with custom(any) end date*/
                deleteRequest.EndDate = new DateTime(2019, 07, 31);

                /*Or can be used like below commnetd code*/
                //// Check if partitions are not supported as is the case with SQL Server Standard edition.
                //if (partitions.IsLogicalCollection)
                //{
                //    // Delete all audit records created up until now.
                //    deleteRequest.EndDate = DateTime.Now;
                //}
                //// Otherwise, delete all partitions that are older than the current partition.
                //// Hint: The partitions in the collection are returned in sorted order where the 
                //// partition with the oldest end date is at index 0.
                //else
                //{
                //    for (int n = partitions.Count - 1; n >= 0; --n)
                //    {
                //        if (partitions[n].EndDate <= DateTime.Now && partitions[n].EndDate > deleteRequest.EndDate)
                //        {
                //            deleteRequest.EndDate = (DateTime)partitions[n].EndDate;
                //            break;
                //        }
                //    }
                //}



                // Delete the audit records.
                if (deleteRequest.EndDate != new DateTime(2000, 1, 1))
                {
                    _service.Execute(deleteRequest);
                    Console.WriteLine("Audit records have been deleted.");
                }
                else
                {
                    Console.WriteLine("There were no audit records that could be deleted.");
                }
                /**/

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
    }
}
