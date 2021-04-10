using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace hospital_prj.Models
{
    public class user
    {
            public int userId { get; set; }
            public string userName { get; set; }
            public string userLname { get; set; }
            public string healthCN { get; set; }
                    }
        public class userDto
        {
            public int userId { get; set; }
            public string userName { get; set; }
            public string userLname { get; set; }
            public string healthCN { get; set; }

        }
    }
