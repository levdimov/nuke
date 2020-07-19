// Copyright 2019 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace Nuke.CodeGeneration.Model
{
    [UsedImplicitly(ImplicitUseKindFlags.Assign, ImplicitUseTargetFlags.WithMembers)]
    public class DataClass : IDeprecatable
    {
        [JsonIgnore]
        [YamlIgnore]
        public Tool Tool { get; set; }

        [JsonIgnore]
        [YamlIgnore]
        public virtual bool IsToolSettingsClass => false;

        [NotNull]
        [JsonIgnore]
        [YamlIgnore]
        public virtual IDeprecatable Parent => Tool;

        [JsonProperty(Required = Required.Always)]
        [RegularExpression(RegexPatterns.Name)]
        [Description("Name of the data class.")]
        public virtual string Name { get; set; }

        [Description("The base class to inherit from.")]
        public string BaseClass { get; set; }

        [Description("Enables generation of extension methods for modification.")]
        public bool ExtensionMethods { get; set; }

        [Description("Omits generation of the data class.")]
        public bool OmitDataClass { get; set; }

        [Description("Properties of the data class.")]
        public List<Property> Properties { get; set; } = new List<Property>();

        [Description("Obsolete message. DataClass is marked as obsolete when specified.")]
        public string DeprecationMessage { get; set; }
    }

    [UsedImplicitly]
    public class SettingsClass : DataClass
    {
        public SettingsClass()
        {
            ExtensionMethods = true;
        }

        public override bool IsToolSettingsClass => true;

        [JsonIgnore]
        [YamlIgnore]
        public Task Task { get; set; }

        [NotNull]
        [JsonIgnore]
        [YamlIgnore]
        public override IDeprecatable Parent => Task;

        [JsonProperty(Required = Required.Default)]
        public override string Name => $"{Tool.Name}{Task.Postfix}Settings";
    }
}
