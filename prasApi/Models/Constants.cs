using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;

namespace prasApi.Models
{
    //[JsonConverter(typeof(StringEnumConverter))]
    public enum Gender
    {
        Male,
        Female
    }

    //[JsonConverter(typeof(StringEnumConverter))]
    public enum Priority
    {
        Low,
        Medium,
        High,
        Critical
    }

    //[JsonConverter(typeof(StringEnumConverter))]
    public enum Status
    {
        Open,
        InProgress,
        Completed
    }

    public class TemplateStructure : Dictionary<string, string>
    {
    }

    public class FieldValue : Dictionary<string, string>
    {
    }
}