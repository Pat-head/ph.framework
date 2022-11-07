using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace WebApplicationTest.Domain.Entities
{
    [Table("demo", Schema = "public")]
    public class DemoEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column("id"), Key]
        public int Id { get; set; }


        /// <summary>
        /// 主键
        /// </summary>
        [Column("name")]
        public string Name { get; set; }
    }
}