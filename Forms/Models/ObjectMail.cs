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
    
    public partial class ObjectMail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ObjectMail()
        {
            this.SendedMail = new HashSet<SendedMail>();
        }
    
        public int Id { get; set; }
        public string Libelle { get; set; }
        public Nullable<int> IdModelCRM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SendedMail> SendedMail { get; set; }
    }
}
