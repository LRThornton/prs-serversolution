﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace prs_server.Models {

    public class Requestline {

        public int Id { get; set; }
        public int Quantity { get; set; }

        public int RequestId { get; set; }
        [JsonIgnore]
        public virtual Request Request { get; set; }

        public int ProductId { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }

        public Requestline() { }

    }
}
