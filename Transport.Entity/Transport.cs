//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Transport.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transport()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int TransportId { get; set; }
        public string CarNumber { get; set; }
        public string Dimensions { get; set; }
        public Nullable<double> CarryingCapacity { get; set; }
        public Nullable<double> FuelConsumption { get; set; }
        public int DriverId { get; set; }
        public int TransportTypeId { get; set; }
        public string Name { get; set; }
        public Nullable<int> MaxSpeed { get; set; }
    
        public virtual Driver Driver { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        public virtual TransportType TransportType { get; set; }
    }
}
