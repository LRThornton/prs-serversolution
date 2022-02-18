using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace prs_server.Models {

    public class Product {

        public int Id { get; set; }
        [Required, StringLength(30)]
        public string PartNbr { get; set; }
        [Required, StringLength(30)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(11,2)")]
        public decimal Price { get; set; }
        [Required, StringLength(30)]
        public string Unit { get; set; }
        [StringLength(255)]
        public string Photopath { get; set; }

        public int VendorId { get; set; }
        //virtual instance required for EF to recognize Fk, virtual means it wont be in the database, just in the class
        public virtual Vendor Vendor { get; set; }

        public Product() { }

    }
}
