using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiLab.Models
{
    /// <summary>
    /// Represents a message sent from a user to another user.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// The Id of the message. It is the primary key.
        /// </summary>
        public Int64 Id { get; set; }

        /// <summary>
        /// The name of the application that sends the message
        /// </summary>
        [MinLength(1)]
        public string AppName { get; set; }

        /// <summary>
        /// The identity string of the sender. E.g. email
        /// </summary>
        [Required]
        [MinLength(1)]
        public string SenderId { get; set; }

        /// <summary>
        /// The identity string of the receipent, e.g. email
        /// </summary>
        [MinLength(1)]
        public string ReceiverId { get; set; }

        /// <summary>
        /// The content type of the message. This is defined by the developer of the app that sends the messages.
        /// </summary>
        [Required]
        [MinLength(1)]
        public string MessageContentType { get; set; }

        /// <summary>
        /// The message body. The app developer defines its app message format. 
        /// </summary>
        [Required]
        [MinLength(1)]
        public string MessageBody { get; set; }

        /// <summary>
        /// The timestamp when the message is received by the ApiLab service. 
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime PostedTime { get; set; }
    }
}
