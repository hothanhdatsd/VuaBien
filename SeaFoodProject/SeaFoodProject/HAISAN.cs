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
    
    public partial class HAISAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HAISAN()
        {
            this.CHITIETDONDATs = new HashSet<CHITIETDONDAT>();
            this.CHITIETHOADONs = new HashSet<CHITIETHOADON>();
        }
    
        public string MSHAISAN { get; set; }
        public string MADVT { get; set; }
        public string MALOAI { get; set; }
        public string TENHAISAN { get; set; }
        public string CHITIETLOAI { get; set; }
        public Nullable<int> SOLUONG { get; set; }
        public Nullable<double> GIATIEN { get; set; }
        public string HINHANH { get; set; }
        public string MIEUTA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDONDAT> CHITIETDONDATs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETHOADON> CHITIETHOADONs { get; set; }
        public virtual DONVITINH DONVITINH { get; set; }
        public virtual LOAIHAISAN LOAIHAISAN { get; set; }
    }
}
