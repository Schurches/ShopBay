//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopBay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.Products = new HashSet<Products>();
            this.BidList = new HashSet<BidList>();
            this.ProductCommentary = new HashSet<ProductCommentary>();
            this.ProductsSold = new HashSet<ProductsSold>();
            this.ProductsSold1 = new HashSet<ProductsSold>();
            this.ProfileCommentary = new HashSet<ProfileCommentary>();
            this.ProfileCommentary1 = new HashSet<ProfileCommentary>();
        }

        public int UserID { get; set; }
        [Required(ErrorMessage = "Enter your username", AllowEmptyStrings = false)]
        [Display(Name = "Username")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username must NOT contain white spaces or @")]
        [StringLength(50, ErrorMessage = "The username must be at least {2} characters long.", MinimumLength = 3)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter your name", AllowEmptyStrings = false)]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "The name must be at least 50 characters characters long.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "name must only contain letters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter your password", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long. And must not be pure blanks", MinimumLength = 3)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Type { get; set; }
        [Required(ErrorMessage = "Enter your name", AllowEmptyStrings = false)]
        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "name must only contain letters")]
        public string Information { get; set; }
        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [Display(Name = "Telephone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string Telephone { get; set; }
        [Required(ErrorMessage = "Your Email address")]
        [Display(Name = "Mail")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Correct Email Address")]
        public string Mail { get; set; }
        public Nullable<double> Rate { get; set; }
        public Nullable<int> AccMoney { get; set; }
        public byte[] Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Products> Products { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BidList> BidList { get; set; }
        public virtual Movements Movements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductCommentary> ProductCommentary { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductsSold> ProductsSold { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductsSold> ProductsSold1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileCommentary> ProfileCommentary { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileCommentary> ProfileCommentary1 { get; set; }
    }
}

