using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCappCoreWeb.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using MVCappCoreWeb.Areas.Identity.Data;

namespace MVCappCoreWeb.DBLayer
{
        public class EventsManager
    {
        public static bool InsertEvent(MessageViewModel msg)
        {
            bool resp = false;
            try
            {
                using (SqlConnection connection = ConnectionManager.GetConnectionEvents())
                {
                    SqlCommand com = new SqlCommand("proc_Message_Insert", connection);
                    com.CommandType = CommandType.StoredProcedure;

                    com.Parameters.AddWithValue("@Sender", SqlDbType.NVarChar).Value = msg.Sender;
                    com.Parameters.AddWithValue("@Receivers", SqlDbType.NVarChar).Value = msg.Receivers;
                    com.Parameters.AddWithValue("@CCReceivers", SqlDbType.NVarChar).Value = msg.CCReceivers;
                    com.Parameters.AddWithValue("@BCCReceivers", SqlDbType.NVarChar).Value = msg.BCCReceivers;
                    com.Parameters.AddWithValue("@Subject", SqlDbType.NVarChar).Value = msg.Subject;
                    com.Parameters.AddWithValue("@Body", SqlDbType.NVarChar).Value = msg.Body;
                    com.Parameters.AddWithValue("@ProcessResultMessage", SqlDbType.NVarChar).Value = msg.ProcessResultMessage;
                    com.Parameters.AddWithValue("@DateTimeOfCreation", SqlDbType.DateTime).Value = msg.DateTimeOfCreation;
                    com.Parameters.AddWithValue("@DateTimeOfUpdate", SqlDbType.DateTime).Value = msg.DateTimeOfUpdate;
                    com.Parameters.AddWithValue("@NumberOfFailures", SqlDbType.Int).Value = msg.NumberOfFailures;
                    com.Parameters.AddWithValue("@MessageStatus_Id", SqlDbType.Int).Value = msg.MessageStatus_Id;
                    com.Parameters.AddWithValue("@MessageType_Id", SqlDbType.Int).Value = msg.MessageType_Id;
                    com.Parameters.AddWithValue("@MessageEventType_Id", SqlDbType.Int).Value = msg.MessageEventType_Id;
                    com.Parameters.AddWithValue("@IdEvent", SqlDbType.Int).Value = msg.intIdEvent;
                    com.ExecuteNonQuery();
                }
                resp = true;
            }
            catch (Exception ex)
            {
                resp = false;
            }
            return resp;
        }

        public static MessageViewModel getMessageViewModel(WebUser user, string ProcessResultMessage,int idEvent,int MessageEventType_Id, int MessageType_Id)
        {
            MessageViewModel newEvent = new MessageViewModel();
            try
            {
                newEvent.Subject = ProcessResultMessage;
                newEvent.Sender = user.Email;
                newEvent.Receivers = user.Email;
                newEvent.Body = "Text";
                newEvent.BCCReceivers = "12312";
                newEvent.CCReceivers = null;
                newEvent.DateTimeOfCreation = DateTime.Now;
                newEvent.DateTimeOfUpdate = DateTime.Now;
                newEvent.MessageEventType_Id = MessageEventType_Id;
                newEvent.MessageStatus_Id = 1;
                newEvent.MessageType_Id = MessageType_Id;
                newEvent.ProcessResultMessage = ProcessResultMessage;
                newEvent.NumberOfFailures = 0;
                newEvent.intIdEvent = idEvent;
            }
            catch (Exception ex)
            {
                // log;
            }
            return newEvent;
        }

    }


        public class EventViewModel
        {
            public string EventId { get; set; }
            public IList<MessageViewModel> ListOfMessages { get; set; }

       
    }
        public class MessageViewModel
        {
            public string Sender { get; set; }
            public string Receivers { get; set; }
            public string CCReceivers { get; set; }
            public string BCCReceivers { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public string ProcessResultMessage { get; set; }
            public DateTime DateTimeOfCreation { get; set; }
            public DateTime DateTimeOfUpdate { get; set; }
            public int NumberOfFailures { get; set; }
            public int MessageStatus_Id { get; set; }
            public int MessageType_Id { get; set; }
            public int MessageEventType_Id { get; set; }
            public int intIdEvent { get; set; }
        }
    
        


}
