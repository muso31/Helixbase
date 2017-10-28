using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Fields;

namespace Helixbase.Feature.Hero.ViewModels
{
    public class HeroViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Image> HeroImages { get; set; }
        public bool IsExperienceEditor { get; set; }
    }
}