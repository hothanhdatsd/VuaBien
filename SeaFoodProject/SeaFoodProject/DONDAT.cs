//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SeaFoodProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class DONDAT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DONDAT()
        {
            this.CHITIETDONDATs = new HashSet<CHITIETDONDAT>();
        }
    
        public int MADON { get; set; }
        public string MAKH { get; set; }
        public string MATT { get; set; }
        public Nullable<System.DateTime> THOIGIAN { get; set; }
        public Nullable<double> TONGTIEN { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDONDAT> CHITIETDONDATs { get; set; }
        public virtual KHACHHANG KHACHHANG { get; set; }
        public virtual TRANGTHAI TRANGTHAI { get; set; }
    }
}