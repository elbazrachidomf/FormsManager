//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Forms.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UNITE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UNITE()
        {
            this.ORGANISATION = new HashSet<ORGANISATION>();
        }
    
        public int ID { get; set; }
        public string LIBELLE { get; set; }
        public int idGroupe { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORGANISATION> ORGANISATION { get; set; }
    }
}