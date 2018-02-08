using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLab.Models
{
    /// <summary>
    /// Object model that represents a user's activity in One Drive.
    /// ---> Remark: Need ResourceOwnerId? ResourceId covers all that's needed already.
    /// </summary>
    public class ResourceActivity
    {
        /// <summary>
        /// Id of this activity assigned by the sql server.
        /// </summary>
        public Int64 Id { get; set; }

        /// <summary>
        /// Name of the app that this activity occurred in.
        /// </summary>
        [MinLength(1)]
        public string AppName { get; set; }

        /// <summary>
        /// The resource that this activity occurred to.
        /// </summary>
        [Required]
        [MinLength(1)]
        public string ResourceId { get; set; }

        /// <summary>
        /// Id of user that performed this activity.
        /// </summary>
        [Required]
        [MinLength(1)]
        public string ActionerId { get; set; }

        /// <summary>
        /// Id of the owner of the resource.
        /// </summary>
        [Required]
        [MinLength(1)]
        public string ResourceOwnerId { get; set; }

        /// <summary>
        /// Activity content's type/format.
        /// </summary>
        [Required]
        [MinLength(1)]
        public string ContentType { get; set; }

        /// <summary>
        /// Description of this activity.
        /// </summary>
        [Required]
        [MinLength(1)]
        public string Content { get; set; }

        /// <summary>
        /// Date and time that action occurred.
        /// </summary>
        public DateTime OccurenceTime { get; set; }
    }
}
