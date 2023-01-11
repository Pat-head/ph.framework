using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using PatHead.Framework.Uow.Entity;

namespace WebApplicationTest.Domain.Entities
{
    public class DemoEntity : BaseIntegerKeyEntity
    {
        public string Name { get; set; }
    }
}