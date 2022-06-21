﻿using FluentValidation.Results;
using Newtonsoft.Json;

namespace PS.Core.Validations
{
    public class Validation
    {
        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }

        [JsonIgnore]
        public bool IsValid => ValidationResult.IsValid;
    }
}
