using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospitalPrj.Models
{
    public class client
    {
        public int clientId { get; set; }
        public string clientName { get; set; }
        public string clientLname { get; set; }
        public string healthCN { get; set; }

       
    }
    public class clientDto
    {
        public int clientId { get; set; }
        public string clientName { get; set; }
        public string clientLname { get; set; }
        public string healthCN { get; set; }

    }
}