//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NotOtomasyonu
{
    using System;
    using System.Collections.Generic;
    
    public partial class OgrenciDb
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OgrenciDb()
        {
            this.NotDbs = new HashSet<NotDb>();
        }
    
        public string No { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NotDb> NotDbs { get; set; }
    }
}
