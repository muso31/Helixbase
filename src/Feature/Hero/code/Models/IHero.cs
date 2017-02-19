﻿using Glass.Mapper.Sc.Fields;
using Helixbase.Foundation.Content.Models;
using System.Collections.Generic;

namespace Helixbase.Feature.Hero.Models
{
    public interface IHero : ISitecoreItem
    {
        IEnumerable<Image> HeroImages { get; set; }
    }
}
