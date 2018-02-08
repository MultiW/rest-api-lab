namespace ApiLab.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents an app that uses the ApiLab service.
    /// </summary>
    public class App
    {
        /// <summary>
        /// The app name. This is the primary key of the Apps table.
        /// </summary>
        [Key]
        public string Name { get; set; }

        /// <summary>
        /// App description.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// The permission token for the app to access the ApiLab service.
        /// </summary>
        public string ApiToken { get; set; }
       
        /// <summary>
        /// When the app is created.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
    }
}
