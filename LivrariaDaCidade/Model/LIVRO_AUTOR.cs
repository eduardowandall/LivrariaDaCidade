//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LivrariaDaCidade.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class LIVRO_AUTOR
    {
        public int ID { get; set; }
        public int ID_AUTOR { get; set; }
        public int ID_LIVRO { get; set; }
    
        public virtual AUTOR AUTOR { get; set; }
        public virtual LIVRO LIVRO { get; set; }
    }
}
